using Godot;
using System;

public partial class MovementModule : Module
{
	[Export]
	private Node2D _canonRotator;

	private Vector2 _velocity;
	private Action<double> _addMovement;
	public Vector2 Velocity => _velocity;

	[Signal]
	private delegate void WalkingEventHandler();
	[Signal]
	private delegate void IddleEventHandler();
	[Signal]
	private delegate void FlipEventHandler(bool isFlipped);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_addMovement = d => { };
	}

	[Export] public float Speed = 5.0f, SpeedFactor=1;
	public override void _PhysicsProcess(double delta)
	{
		MoveLogic();
		Rotation();
		_addMovement.Invoke(delta);
		_owner.MoveAndSlide();
	}

	public void AddMovement(Action<double> movementToAdd)
	{
		_addMovement += movementToAdd;
	}

	public void RemoveMovement(Action<double> movementToRem)
	{
		_addMovement -= movementToRem;
	}
	private void MoveLogic()
	{
		if (_owner.Pause)
		{
			_velocity = Vector2.Zero;
			_owner.Velocity = Vector2.Zero;
			EmitSignalIddle();
			return;
		}
		 _velocity = _owner.Velocity;

		Vector2 inputDir = _owner.MovAxis; 
		Vector2 direction = (new Vector2(inputDir.X, inputDir.Y)).Normalized();
		if (direction != Vector2.Zero)
		{
			_velocity.X = direction.X * Speed*SpeedFactor;
			_velocity.Y = direction.Y * Speed*SpeedFactor;
		}
		else
		{
			_velocity.X = Mathf.MoveToward(_owner.Velocity.X, 0, Speed);
			_velocity.Y = Mathf.MoveToward(_owner.Velocity.Y, 0, Speed);
		}

		if (_velocity.Length() >= 0.02f)
		{
			EmitSignalWalking();
			EmitSignalFlip(_velocity.X<=0);
		}
		else
		{
			EmitSignalIddle();
		}
		_owner.Velocity = _velocity;
	}

	private void Rotation()
	{
		Vector2 inputDir = _owner.LookAxis;
		var angle =  Mathf.Atan2(inputDir.X, inputDir.Y);
		_canonRotator.Rotation = angle;
	}
	
}

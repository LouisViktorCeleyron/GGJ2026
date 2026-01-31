using Godot;
using System;

public partial class MovementModule : Module
{
	[Export]
	private Node2D _canonRotator;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
	[Export]
	public float Speed = 5.0f;
	public override void _PhysicsProcess(double delta)
	{
		MoveLogic();
		Rotation();
		_owner.MoveAndSlide();
	}

	private void MoveLogic()
	{
		Vector2 velocity = _owner.Velocity;

		Vector2 inputDir = _owner.MovAxis; 
		Vector2 direction = (new Vector2(inputDir.X, inputDir.Y)).Normalized();
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(_owner.Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(_owner.Velocity.Y, 0, Speed);
		}

		_owner.Velocity = velocity;
	}

	private void Rotation()
	{
		Vector2 inputDir = _owner.LookAxis;
		var angle =  Mathf.Atan2(inputDir.X, inputDir.Y);
		_canonRotator.Rotation = angle;
	}
	
	public override void _Process(double delta)
	{
	}
}

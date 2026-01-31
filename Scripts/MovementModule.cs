using Godot;
using System;

public partial class MovementModule : Module
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
	public const float Speed = 5.0f;
	public override void _PhysicsProcess(double delta)
	{
		MoveLogic();
		Rotation();
		_owner.MoveAndSlide();
	}

	private void MoveLogic()
	{
		Vector3 velocity = _owner.Velocity;

		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(_owner.Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(_owner.Velocity.Z, 0, Speed);
		}

		_owner.Velocity = velocity;
	}

	private void Rotation()
	{
		Vector2 inputDir = Input.GetVector("ui_lookRight", "ui_lookLeft", "ui_lookDown", "ui_lookUp");
		var angle =  Mathf.Atan2(inputDir.X, inputDir.Y);
		_owner.Rotation = Vector3.Up*angle;
	}
	
	public override void _Process(double delta)
	{
	}
}

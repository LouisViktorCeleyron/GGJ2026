using Godot;
using System;

public partial class jexistepourmourir : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			GD.Print("coucou");
		}
	}
	
}

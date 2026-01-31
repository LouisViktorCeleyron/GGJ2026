using Godot;
using System;

[GlobalClass]
public partial class BulletBody : RigidBody2D
{
	[Export] private float _bulletSpeed = 150f;

	private PlayerRef _owner;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void Launch(PlayerRef owner)
	{
		_owner = owner;
		LinearVelocity = -Transform.Y*_bulletSpeed;
	}

	public void Collide(Node2D body)
	{
		if (body as PlayerRef is { } prBody)
		{
			if (prBody != _owner)
			{
				prBody.GetModule<DeathModule>().Die(_owner);
			}
		}
	}
	
	
}

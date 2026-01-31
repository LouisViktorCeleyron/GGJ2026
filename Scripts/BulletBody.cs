using Godot;
using System;

[GlobalClass]
public partial class BulletBody : RigidBody2D
{
	[Export] private float _bulletSpeed = 150f;

	private string _ownerID;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void Launch(string ownerId)
	{
		_ownerID = ownerId;
		LinearVelocity = -Transform.Y*_bulletSpeed;
	}

	public void Collide(Node2D body)
	{
		if (body as PlayerRef is { } prBody)
		{
			if (prBody.GetID() != _ownerID)
			{
				prBody.GetModule<DeathModule>().Die(_ownerID);
			}
		}
	}
	
	
}

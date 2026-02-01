using Godot;
using System;

[GlobalClass]
public partial class BulletBody : RigidBody2D
{
	[Export] private float _bulletSpeed = 150f;

	protected PlayerRef _owner;


	private bool _shouldCollide=true;

	public void SetShouldCollide(bool b)
	{
		_shouldCollide = b;
	}

	public virtual void Launch(PlayerRef owner)
	{
		_owner = owner;
		LinearVelocity = Transform.Y*_bulletSpeed;
	}

	public void Collide(Node2D body)
	{
		if (!_shouldCollide)
		{
			return;
		}
		if (body as PlayerRef is { } prBody)
		{
			if (prBody != _owner)
			{
				prBody.GetModule<DeathModule>().Die(_owner);
			}
			else
			{
				prBody.GetModule<DeathModule>().Die(GameManager.Instance.FindRandomPlayer(_owner));
			}
		}

		if (!body.IsInGroup("Bullet"))
		{
			QueueFree();
		}
	}
	
	
}

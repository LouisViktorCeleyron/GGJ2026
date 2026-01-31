using Godot;
using System;

public partial class ShooterModule : Module
{
	
	[Export]
	private Node2D _canon;
	[Export] private PackedScene _spawnedBulletBody;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_owner.Shoot)
		{
			if (_spawnedBulletBody.Instantiate() is BulletBody spawnedBullet)
			{
				AddChild(spawnedBullet);
				spawnedBullet.Position = _canon.GlobalPosition;
				spawnedBullet.Rotation = _canon.GlobalRotation;
				spawnedBullet.Launch(_owner.GetID());
			}
		}
	}
}

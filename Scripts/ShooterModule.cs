using Godot;
using System;

public partial class ShooterModule : Module
{
	
	[Export] private PackedScene _spawnedBulletBody;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_accept"))
		{
			if (_spawnedBulletBody.Instantiate() is BulletBody spawnedBullet)
			{
				spawnedBullet.Prout();
				spawnedBullet.Position = _owner.Position;
			}
		}
	}
}

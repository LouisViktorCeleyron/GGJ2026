using Godot;
using System;

public partial class ShooterModule : Module
{

	[Export] private Timer _shootTimer;
	[Export]
	private Node2D _canon;
	[Export] private PackedScene _spawnedBulletBody;

	private bool _canShoot=true;

	public void ResetShoot()
	{
		_canShoot = true;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_owner.Shoot)
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		if (!_canShoot)
		{
			return;
		}
		if (_spawnedBulletBody.Instantiate() is BulletBody spawnedBullet)
		{
			_canShoot = false;
			AddChild(spawnedBullet);
			spawnedBullet.Position = _canon.GlobalPosition;
			spawnedBullet.Rotation = _canon.GlobalRotation;
			spawnedBullet.Launch(_owner);
			_shootTimer.Start();
		}
	}
}

using Godot;
using System;
using System.Collections.Generic;

public partial class ShooterModule : Module
{

	[Export] private Timer _shootTimer;
	[Export]
	private Node2D _canon;

	private bool _canShoot=true;
	private System.Func<bool> _additionalCheck;
	[Signal]
	private delegate void OnShootEventHandler();
	public void ResetShoot()
	{
		_canShoot = true;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_owner.Shoot)
		{
			var shouldShoot = true;
			if (_additionalCheck is not null && _additionalCheck.GetInvocationList().Length > 0)
			{
				shouldShoot = _additionalCheck.Invoke();
			}

			if (shouldShoot)
			{
				Shoot();
			}
		}
	}

	public void AddCondition(Func<bool> condition)
	{
		_additionalCheck += condition;
	}
	private void Shoot()
	{
		if (!_canShoot)
		{
			return;
		}
		EmitSignalOnShoot();
		
	}

	private void CreateBullet()
	{
		var botMask = _owner.GetModule<BotMask>();
		if (botMask.BulletToSpawn.Instantiate() is BulletBody spawnedBullet)
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

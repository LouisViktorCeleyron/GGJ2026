using Godot;
using System;

public partial class DeathModule : Module
{
	[Signal]
	public delegate void OnDeathEventHandler();

	private bool _isInvincible,_isRed;
	private double _timer = 0f, _invTimer =0;
	[Export]
	private float _invincibilityTime = 2f;

	public void Die(PlayerRef killer)
	{
		if (_isInvincible)
		{
			return;
		}
		var isLastHit =
		killer.GetModule<ScoreModule>().AddScore();
		if (isLastHit)
		{
			_isInvincible = false;
			_owner.Modulate = Colors.White;
		}
		else
		{
			StartInvincibility();
			GameManager.Instance.SpawnPlayerAtPoint(_owner);
		}
	}

	public void Defeat()
	{
		EmitSignalOnDeath();
	}

	private void StartInvincibility()
	{
		_timer = 0;
		_invTimer = _invincibilityTime;
		_isInvincible = true;
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (_isInvincible)
		{
			_timer += delta;
			_invTimer -= delta;
			if (_timer >= 0.2f)
			{
				_timer = 0;
				_isRed = !_isRed;
				_owner.Modulate = _isRed ? Colors.Red : Colors.White;
			}

			if (_invTimer <= 0)
			{
				_owner.Modulate = Colors.White;
				_isInvincible = false;
			}
		}
	}
}

using Godot;
using System;

public partial class DeathModule : Module
{
	[Signal]
	public delegate void OnDeathEventHandler();
	public void Die(PlayerRef killer)
	{
		killer.GetModule<ScoreModule>().AddScore();
		GameManager.Instance.SpawnPlayerAtPoint(_owner);
	}

	public void Defeat()
	{
		EmitSignalOnDeath();
	}

}

using Godot;
using System;

public partial class DeathModule : Module
{
	[Signal]
	public delegate void OnDeathEventHandler();
	public void Die(PlayerRef killer)
	{
		GD.Print($"Ono Je sui le J{_owner.PlayerIndex} suis mort et j'ai été tué par le J{killer.PlayerIndex}");	
		killer.GetModule<ScoreModule>().AddScore();
		GameManager.Instance.SpawnPlayerAtPoint(_owner);
	}

	public void Defeat()
	{
		
	}

}

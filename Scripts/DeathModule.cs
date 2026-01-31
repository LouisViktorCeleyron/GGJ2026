using Godot;
using System;

public partial class DeathModule : Module
{
	
	public void Die(PlayerRef killer)
	{
		GD.Print($"Ono Je sui le J{_owner.PlayerIndex} suis mort et j'ai été tué par le J{killer.PlayerIndex}");	
		killer.GetModule<ScoreModule>().AddScore();
		GameManager.Instance.SpawnPlayerAtPoint(_owner);
	}


}

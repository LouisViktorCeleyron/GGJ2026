using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Array = Godot.Collections.Array;

[GlobalClass]
public partial class GameManager : Node
{

	public static GameManager Instance;
	private List<PlayerRef> _score;

	[Export] private Array<SpawnPoint> _spawnPoints;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GameManager.Instance is null)
		{
			GameManager.Instance = this;
		}
		else
		{
			Free();
		}
	}

	public PlayerRef FindRandomPlayer(PlayerRef exclude = null)
	{
		PlayerRef ret = null;
		while (ret is null || ret != exclude)
		{
			
		}
	}

	public void SpawnPlayerAtPoint(PlayerRef player)
	{
		player.Position = _spawnPoints.PickRandom().Position;
	}

}

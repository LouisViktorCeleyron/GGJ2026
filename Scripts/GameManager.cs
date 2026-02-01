using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Array = Godot.Collections.Array;

[GlobalClass]
public partial class GameManager : Node
{

	public static GameManager Instance;
	private Array<PlayerRef> _players;

	[Export] private Array<SpawnPoint> _spawnPoints;
	[Signal]
	public delegate void EndGameSignalEventHandler(PlayerRef winner);

	public int BestOf = 3;

	private void StartGame()
	{
		GD.Print(_players);
		foreach (var player in _players)
		{
			var pos = _spawnPoints.PickRandom();
			while (pos.IsOccupied)
			{
				pos = _spawnPoints.PickRandom();
			}

			player.Position = pos.Position;
			pos.IsOccupied = true;
		}
	}
	
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

		CallDeferred("StartGame");
	}

	public void EndGame(PlayerRef winner)
	{
		foreach (var player in _players)
		{
			player.SetPause(true);
			if (player == winner)
			{
				continue;
			}
			player.GetModule<DeathModule>().Defeat();
		}
		EmitSignalEndGameSignal(winner);
	}
	public void AddPlayerRef(PlayerRef pr)
	{
		if (_players is null)
		{
			_players = new Array<PlayerRef>();
		}
		_players.Add(pr);
	}

	public PlayerRef FindRandomPlayer(PlayerRef exclude = null)
	{
		PlayerRef ret = null;
		while (ret is null || ret == exclude)
		{
			ret = _players.PickRandom();
		}
		return ret;
	}

	public void SpawnPlayerAtPoint(PlayerRef player)
	{
		player.Position = _spawnPoints.PickRandom().Position;
	}

}

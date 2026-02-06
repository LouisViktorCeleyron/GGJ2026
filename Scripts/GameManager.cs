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
	public delegate void LoadScreenSignalEventHandler();
	[Signal]
	public delegate void StartGameSignalEventHandler();
	[Signal]
	public delegate void EndGameSignalEventHandler(PlayerRef winner);

	[Export] private AnimationPlayer _animPlayer;
	public int BestOf = 3;
	private bool _gameStarted,_canGameStart;
	[Export] private PackedScene _mainScene;

	[Export] private SettingManager _settings;
	
	public void AddSpawnPoint(SpawnPoint newSP)
	{
		_spawnPoints.Add(newSP);
	}

	public void LoadStartScreen()
	{
		EmitSignalLoadScreenSignal();
	}

	private void ManageAnimation(StringName animName)
	{
		switch (animName)
		{
			case "In":
				_animPlayer.Play("Out");
				if (_canGameStart)
				{
					PreStartGame();
				}
				break;
			case "Out":
				if(!_canGameStart)
				{
					_canGameStart = true;
				}
				break;;
		}
	}
	public void StartGame()
	{
		foreach (var player in _players)
		{
			var pos = _spawnPoints.PickRandom();
			while (pos.IsOccupied)
			{
				pos = _spawnPoints.PickRandom();
			}
			player.GetModule<MovementModule>().InitMovement();
			player.Position = pos.Position;
			pos.IsOccupied = true;
		}
		
		EmitSignalStartGameSignal();
		foreach (var player in _players)
		{
			_gameStarted = true;
			player.GetModule<MaskManagerModule>().GenerateMasks();
			player.SetPause(false);
		}
	}
	
	public void GameOver()
	{
		GetTree().Quit();
	}

	private void PreStartGame()
	{
		GetTree().ChangeSceneToPacked(_mainScene);
	}

	public static SettingManager GetSettings()
	{
		return Instance._settings;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GameManager.Instance is null)
		{
			GameManager.Instance = this;
			_gameStarted = false;
			_spawnPoints = new Array<SpawnPoint>();
		}
		else
		{
			Free();
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!_gameStarted && _canGameStart)
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				_animPlayer.Play("In");
			}
		}
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

using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Array = Godot.Collections.Array;

[GlobalClass]
public partial class GameManager : Node
{
	public static GameManager Instance;
	[Export] private SettingManager _settings;
	public static SettingManager GetSettings()
	{
		return Instance._settings;
	}
	public override void _Ready()
	{
		if (GameManager.Instance is null)
		{
			GameManager.Instance = this;
			_gameStarted = false;
			SetUpGame();
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
				LaunchAnim("In");
			}
		}
		else
		{
			if (Input.IsActionPressed("ui_accept"))
			{
				ProcessSkip(_skipProcess+(float)delta);
			}

			if (Input.IsActionJustReleased("ui_accept"))
			{
				ProcessSkip(0f);
			}
		}
	}

	#region SkipManagement
		[Export] private float _skipAmount =1;
		private float _skipProcess;
		public Action<float> OnSkipUpdate;
		public Action OnSkipFinished;
		private void ProcessSkip(float skipValue)
		{
			_skipProcess = skipValue;
			OnSkipUpdate?.Invoke(skipValue);
			if (_skipProcess >= _skipAmount)
			{
				OnSkipFinished?.Invoke();
				OnSkipFinished = null;
				OnSkipUpdate = null;
			}
		}
	#endregion
	#region GameManagement
		private bool _gameStarted,_canGameStart;
		[Export] private PackedScene _mainScene;
		[Signal] public delegate void StartGameSignalEventHandler();
		[Signal] public delegate void EndGameSignalEventHandler(PlayerRef winner);
		private void SetUpGame()
		{
			_spawnPoints = new Array<SpawnPoint>();
			Players = new Array<PlayerRef>();
			_gameStarted = false;
			_canGameStart = false;
		}
		public void StartGame()
		{
			foreach (var player in Players)
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
			foreach (var player in Players)
			{
				_gameStarted = true;
				player.GetModule<MaskManagerModule>().GenerateMasks();
				player.SetPause(false);
			}
		}
		private void PreStartGame()
		{
			GetTree().ChangeSceneToPacked(_mainScene);
		}
		public void EndGame(PlayerRef winner)
		{
			foreach (var player in Players)
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
		public bool IsInGame()
		{
			return _gameStarted;
		}
	#endregion
	#region Player
		[Export] private Array<SpawnPoint> _spawnPoints;
		public Array<PlayerRef> Players { get; private set; }
		public void AddPlayerRef(PlayerRef pr)
		{
			if (Players is null)
			{
				Players = new Array<PlayerRef>();
			}
			Players.Add(pr);
		}
		public PlayerRef FindRandomPlayer(PlayerRef exclude = null)
		{
			PlayerRef ret = null;
			while (ret is null || ret == exclude)
			{
				ret = Players.PickRandom();
			}
			return ret;
		}
		public void SpawnPlayerAtPoint(PlayerRef player)
		{
			player.Position = _spawnPoints.PickRandom().Position;
		}
		public void AddSpawnPoint(SpawnPoint newSP)
		{
			_spawnPoints.Add(newSP);
		}
	#endregion
	#region Animations
		[Export] private AnimationPlayer _animPlayer;

		[Export] private CanvasLayer _mainMenu;
		public void LaunchAnim(string animName)
		{
			_animPlayer.Play(animName);
		}
		public void ManageAnimation(StringName animName)
		{
			switch (animName)
			{
				case "In":
					LaunchAnim("Out");
					if (_canGameStart)
					{
						PreStartGame();
					}
					else
					{
						_mainMenu.Visible = true;
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
	#endregion
}

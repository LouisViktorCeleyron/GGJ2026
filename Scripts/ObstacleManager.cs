using Godot;
using Godot.Collections;
using System;

public partial class ObstacleManager : Node
{
	[Export]
	private Array<PackedScene> _obstacles;
	private Array<Node2D> _spawned;
	private Array<SpawnPoint> _spawnPoints;

	public override void _Ready()
	{
		base._Ready();
		var container = GetChild(0);
		_spawnPoints = new Array<SpawnPoint>();
		_spawned = new Array<Node2D>();
		foreach (var sp in container.GetChildren())
		{
			_spawnPoints.Add(sp as SpawnPoint);
		}
		container.GetChildren();
		
	}

	private int GetObstacleAmount()
	{
		return GameManager.GetSettings().GetSettingValue("Obstacles");
	}

	public void GenerateObstacles()
	{
		var amount = GetObstacleAmount();
		if (amount <= 0)
		{
			return;
		}
		
		var selectedSp = _spawnPoints.PickRandom();
		for (int i = 0; i < amount; i++)
		{
			while (selectedSp.IsOccupied)
			{
				selectedSp = _spawnPoints.PickRandom();
			}

			if (_obstacles.PickRandom().Instantiate() is Node2D tempObs)
			{
				GetTree().CurrentScene.AddChild(tempObs);
				tempObs.Position = selectedSp.Position;
				selectedSp.IsOccupied = true;
				_spawned.Add(tempObs);
			}
		}
	}

	public void ResetSpawnPoints()
	{
		foreach (var VARIABLE in _spawnPoints)
		{
			VARIABLE.IsOccupied = false;
		}

		foreach (var VARIABLE in _spawned)
		{
			VARIABLE.QueueFree();
		}
	}
	
}

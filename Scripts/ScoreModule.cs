using Godot;
using System;

public partial class ScoreModule : Module
{
	private int _score;
	[Signal]
	public delegate void OnChangeScoreEventHandler(string score);
	[Signal]
	public delegate void OnVictoryEventHandler();

	private int _bestOf;
	public override void _Ready()
	{
		base._Ready();
		_bestOf = GameManager.GetSettings().GetSettingValue("Points");
	}
	public bool AddScore()
	{
		return ChangeScore(_score+1);
	}

	private bool ChangeScore(int newScore)
	{
		_score = newScore;
		EmitSignalOnChangeScore(_score.ToString("00"));
		if (_score >= _bestOf)
		{
			Victory();
			GameManager.Instance.EndGame(_owner);
			return true;
		}

		return false;
	}
	

	private void Victory()
	{
		EmitSignalOnVictory();
	}
	
	
}

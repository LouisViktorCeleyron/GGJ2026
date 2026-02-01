using Godot;
using System;

public partial class ScoreModule : Module
{
	private int _score;
	[Signal]
	public delegate void ChangeScoreEventHandler(string score);
	[Signal]
	public delegate void OnVictoryEventHandler();

	public void AddScore()
	{
		_score += 1;
		EmitSignalChangeScore(_score.ToString());
		_owner.Bridge(_score);
		if (_score >= GameManager.Instance.BestOf)
		{
			Victory();
			GameManager.Instance.EndGame(_owner);
		}
	}


	private void Victory()
	{
		EmitSignalOnVictory();
	}
	
	
}

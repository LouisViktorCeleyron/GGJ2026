using Godot;
using System;

public partial class ScoreModule : Module
{
	private int _score;
	[Signal]
	public delegate void ChangeScoreEventHandler(string score);
	public void AddScore()
	{
		_score += 1;
		EmitSignalChangeScore(_score.ToString());
	}
	
}

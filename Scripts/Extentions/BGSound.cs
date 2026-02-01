using Godot;
using System;

public partial class BGSound : AudioStreamPlayer2D
{

	[Export]
	private AudioStream _partOne,_partTwo;

	public override void _Process(double delta)
	{
		if (Stream == _partOne)
		{
			if (GetPlaybackPosition() >= 11.075f)
			{
				Stream = _partTwo;
				Play(0);
			}
		}
	}

	public void PlaySound()
	{
		Stream = _partTwo;
		Play();
	}
	
}

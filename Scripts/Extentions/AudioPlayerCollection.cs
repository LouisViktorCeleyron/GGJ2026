using Godot;
using Godot.Collections;
using System;

public partial class AudioPlayerCollection : AudioStreamPlayer2D
{
	[Export]
	private Array<AudioStream> _audioList;
	[Export] private float _pitchRandom = 0f;

	private void PlayRandom()
	{
		if (IsPlaying())
		{
			return;
		}

		Stream = _audioList.PickRandom();
		if (_pitchRandom != 0f)
		{
			PitchScale = 1+(float)GD.RandRange(-0.5, _pitchRandom);
		}
		Play();
	}
	
}

using Godot;
using System;

public partial class BGSound : AudioStreamPlayer2D
{

	[Export] private AudioStream _musicInGame;
	[Export] private AudioStream _menuMusic;
	private void LaunchGameMusic()
	{
		var t = GetPlaybackPosition();
		Stream = _musicInGame;
		Play(t);
	}

	private void LaunchMenuMusic()
	{
		Stream = _menuMusic;
		Play(0);		
	}
}

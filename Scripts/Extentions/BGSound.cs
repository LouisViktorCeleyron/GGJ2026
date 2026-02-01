using Godot;
using System;

public partial class BGSound : AudioStreamPlayer2D
{

	[Export] private AudioStream _musicInGame;
	private bool _doOnce;
	[Signal]
	public delegate void OnChangeMusicEventHandler();
	public override void _Process(double delta)
	{
		if (_doOnce)
		{
			return;
		}
		if (GetPlaybackPosition() >= 11.075f)
		{
			_doOnce = true;
			EmitSignalOnChangeMusic();
		}
	}

	public void LaunchGameMusic()
	{
		var t = GetPlaybackPosition();
		Stream = _musicInGame;
		Play(t);
	}
}

using Godot;
using System;

public partial class BGSound : AudioStreamPlayer2D
{

	public void PlaySound()
	{
		if (!IsPlaying())
		{
			Play();
		}
	}
	
}

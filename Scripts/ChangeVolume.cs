using Godot;
using System;

[GlobalClass]
public partial class ChangeVolume : Node
{
	[Export]
	private AudioStreamPlayer _parent;
	private float _baseDecibel;
	private SettingManager _settings;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_baseDecibel = _parent.VolumeDb;
	}

	public void ChangeVolumeFunction(int newVolume, int max)
	{
		_parent.VolumeDb = ((float)newVolume/(float)max)*_baseDecibel;
	}
}

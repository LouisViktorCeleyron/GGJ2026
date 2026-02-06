using Godot;
using System;

[GlobalClass]
public partial class ChangeVolume : Node
{
	[Export]
	private AudioStreamPlayer2D _parent;
	private float _baseLinear;
	private SettingManager _settings;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CallDeferred("SetUp");
	}

	private void SetUp()
	{
		_parent = GetParent<AudioStreamPlayer2D>();
		_baseLinear = _parent.VolumeLinear;
		GD.Print(GameManager.GetSettings());
		var volumeSetting = GameManager.GetSettings().GetSetting("Volume"); 
		volumeSetting.SubscribeToSetting(ChangeVolumeFunction);
		ChangeVolumeFunction(volumeSetting.Value,volumeSetting.Max);
		
	}

	private void ChangeVolumeFunction(int newVolume, int max)
	{
		_parent.VolumeLinear = ((float)newVolume/(float)max)*_baseLinear;
	}
}

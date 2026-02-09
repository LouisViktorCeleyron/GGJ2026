using Godot;
using System;

public partial class ConnectAutoload : Node
{
	[Export] private ProgressBar _progressBar;

	[Export]
	private CanvasItem _layer;
	[Export]
	private AnimationPlayer _animPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void LoadGameStart()
	{
		if (_animPlayer.IsPlaying())
		{
			_animPlayer.Stop();
		}
		GD.Print(_layer);
		_layer.Visible = false;
		GameManager.Instance.LaunchAnim("Out");
	}

	public void PrepareSkip()
	{
		GameManager.Instance.OnSkipFinished += LoadGameStart;
		GameManager.Instance.OnSkipUpdate += SetSkipBar;
	}

	private void SetSkipBar(float value)
	{
		_progressBar.SetValue(value);
		_progressBar.Visible = value is > 0 and < 0.99f;
	}

	public void LoadMainScene()
	{
		CallDeferred("Bridge");

	}

	private void Bridge()
	{
		GameManager.Instance.StartGame();
	}
}

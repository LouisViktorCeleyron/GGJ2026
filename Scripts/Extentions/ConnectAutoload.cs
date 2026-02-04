using Godot;
using System;

public partial class ConnectAutoload : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void LoadGameStart()
	{
		GameManager.Instance.LoadStartScreen();
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

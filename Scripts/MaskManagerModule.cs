using Godot;
using System;
using Godot.Collections;

public partial class MaskManagerModule : Module
{
	[Export]
	private Array<PackedScene> _topMasks, _botMasks;
	[Export]
	private Node2D _topPos,_botPos;


	public override void PostReady()
	{
		base.PostReady();
		//GenerateMasks();
	}

	public void GenerateMasks()
	{
		var topMask = _topMasks.PickRandom();
		var spawnedTop = topMask.Instantiate();
		_topPos.AddChild(spawnedTop);
		//CallDeferred("AddChildToOwner",spawnedTop,_topPos);

		var botMask = _botMasks.PickRandom();
		var spawnedBot = botMask.Instantiate();
		_botPos.AddChild(spawnedBot);
		//CallDeferred("AddChildToOwner",spawnedBot,_botPos);
		

	}
}

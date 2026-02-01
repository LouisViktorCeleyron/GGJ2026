using Godot;
using System;
using Godot.Collections;

public partial class MaskManagerModule : Module
{
	[Export]
	private Array<PackedScene> _topMasks, _botMasks;
	[Export]
	private Node2D _topPos,_botPos;
	private Vector2 _baseTopPos, _baseBotPos;
	public override void PostReady()
	{
		base.PostReady();
		_baseTopPos = _topPos.Position;
		_baseBotPos = _botPos.Position;
		//GenerateMasks();
	}

	public void MaskFlip(bool flip)
	{
		
		 _topPos.Position =new Vector2(_baseTopPos.X*(flip?-1:1),_topPos.Position.Y);
		 _topPos.Scale = Vector2.Right * (flip ? -1 : 1) + Vector2.Down;
		 _topPos.Scale *= 1.5f;
		 _botPos.Position = new Vector2(_baseBotPos.X*(flip?-1:1),_botPos.Position.Y);
		 _botPos.Scale = Vector2.Right * (flip ? -1 : 1)+ Vector2.Down;
		 _botPos.Scale *= 1.5f;
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

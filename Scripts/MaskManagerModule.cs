using Godot;
using System;
using Godot.Collections;

public partial class MaskManagerModule : Module
{
	[Export]
	private Array<PackedScene> _topMasks, _botMasks;
	[Export]
	private Node2D _topPos,_botPos;

	private Sprite2D _topSprite,_botSprite;

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
		 _topPos.Scale = Vector2.Right * (flip ? -1 : 1);
		 _botPos.Position = new Vector2(_baseBotPos.X*(flip?-1:1),_botPos.Position.Y);
		 _botPos.Scale = Vector2.Right * (flip ? -1 : 1);
	}
	public void GenerateMasks()
	{
		var topMask = _topMasks.PickRandom();
		var spawnedTop = topMask.Instantiate();
		_topSprite = spawnedTop.GetChild<Sprite2D>(0);
		_topPos.AddChild(spawnedTop);
		//CallDeferred("AddChildToOwner",spawnedTop,_topPos);

		var botMask = _botMasks.PickRandom();
		var spawnedBot = botMask.Instantiate();
		_botSprite= spawnedBot.GetChild<Sprite2D>(0);
		_botPos.AddChild(spawnedBot);
		//CallDeferred("AddChildToOwner",spawnedBot,_botPos);
		

	}
}

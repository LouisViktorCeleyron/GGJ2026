using Godot;
using System;

public partial class UiScore : HBoxContainer
{
	[Export]
	private Node jpp;
	public void SetScore(int i)
	{
		var c = jpp.GetChildren();
		var index = 0;
		foreach (var n in c)
		{
			index++;
			var ci = n as CanvasItem;
			ci.SetVisible(i>=index);
		}
	}

}

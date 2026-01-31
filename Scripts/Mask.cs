using Godot;
using System;

[GlobalClass]
public partial class Mask : Node2D
{
	private PlayerRef _owner;
	public override void _Ready()
	{
		_owner = GetParent<PlayerRef>();
	}
	
	public override void _Process(double delta)
	{
	}
}

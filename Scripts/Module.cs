using Godot;
using System;

public partial class Module : Node
{
	protected PlayerRef _owner;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_owner = GetParent<PlayerRef>();
		_owner.AddModule(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

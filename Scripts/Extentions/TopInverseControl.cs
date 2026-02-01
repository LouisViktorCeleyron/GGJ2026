using Godot;
using System;

public partial class TopInverseControl : TopMask
{
	public override void _Ready()
	{
		base._Ready();
		var movementModule = _owner.GetModule<MovementModule>();
		movementModule.SpeedFactor *= -1;
	}

}

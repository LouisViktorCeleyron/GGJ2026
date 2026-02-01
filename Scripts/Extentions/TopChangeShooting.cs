using Godot;
using System;

public partial class TopChangeShooting : TopMask
{
	private MovementModule _movementModule;
	public override void _Ready()
	{
		base._Ready();
		var shootModule = _owner.GetModule<ShooterModule>();
		_movementModule = _owner.GetModule<MovementModule>();
		shootModule.AddCondition(ShouldShoot);
	}

	private bool ShouldShoot()
	{
		return _movementModule.Velocity.Length()<0.0001f;
	}
}

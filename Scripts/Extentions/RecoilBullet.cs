using Godot;
using System;

public partial class RecoilBullet : BulletBody
{
	private double _t;
	private Vector2 _impulse;
	[Export]
	private float _impulseForce=500f;

	private MovementModule _movement;
	public override void Launch(PlayerRef owner)
	{
		base.Launch(owner);
		_t = 0.2;
		_movement = _owner.GetModule<MovementModule>();
		_movement.AddMovement(Impulse);
	}
	
	private void Impulse(double delta)
	{
		_impulse = Vector2.Zero;
		base._PhysicsProcess(delta);
		if (_t >= 0f)
		{
			_t -= delta;
			var impulseDir = -Transform.Y.Normalized()*_impulseForce;
			_impulse.X = Mathf.MoveToward(impulseDir.X,0,0.2f);
			_impulse.Y = Mathf.MoveToward(impulseDir.Y,0,0.2f);
			_owner.Velocity = _impulse;
		}
		else
		{
			_movement.RemoveMovement(Impulse);
		}
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		_movement.RemoveMovement(Impulse);
	}
}

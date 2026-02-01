using Godot;
using System;

public partial class TopMaskSizeManagement : TopMask
{
    private MovementModule _movement;
    [Export]
    private double _scaleSpeed = 0.000001;
    public override void _Ready()
    {
        base._Ready();
        _movement = _owner.GetModule<MovementModule>();
        _movement.AddMovement(ChangeSizeByMove);
    }

    private void ChangeSizeByMove(double delta)
    {
        var velo = _movement.Velocity.Normalized();
        if (velo.Length() <= 0.02f)
        {
            return;
        }
        var newSize = 1.0;
        var target = Mathf.Abs(velo.X) > Mathf.Abs(velo.Y) ? 0.01 : 10;
        newSize = Mathf.MoveToward(_owner.GetScaling(), target, _scaleSpeed); 
        _owner.Scaling((float)newSize);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _movement.RemoveMovement(ChangeSizeByMove);
    }
}

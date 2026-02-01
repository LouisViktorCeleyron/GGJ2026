using Godot;
using System;

[GlobalClass]
public partial class Mask : Module
{
    public override void _Ready()
    {
        _owner = GetParent().GetParent().GetParent<PlayerRef>();
        _owner.AddModule(this);
    }
}

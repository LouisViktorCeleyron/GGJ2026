using Godot;
using System;

public partial class DeathModule : Module
{
	
	public void Die(string ID = "")
	{
		GD.Print($"Ono Je {_owner.GetID()} suis mort et j'ai été tué par {ID}");	
	}


}

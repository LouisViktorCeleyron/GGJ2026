using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerRef : CharacterBody2D
{
	private string _id;
	private List<Module> _modules;
	
	[Export]
	private int _deviceId;

	private Vector2 _lookAxis, _movAxis;
	private bool _shoot;
	public Vector2 LookAxis => _lookAxis;
	public Vector2 MovAxis => _movAxis;
	public bool Shoot => _shoot;
	
	public string GetID()
	{
		return _id;
	}
	private void GenerateID()
	{
		_id = "";
		for (int i = 0; i < 4; i++)
		{
			_id += GD.Randi() % 10;
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		GenerateID();
	}
	public void AddModule(Module module)
	{
		if (_modules == null)
		{
			_modules = new List<Module>();
		}
		_modules.Add(module);
	}
	public T GetModule<T>() where T : Module
	{
		foreach (var module in _modules)
		{
			if (module.GetType() == typeof(T))
			{
				return module as T;
			}
		}

		return null;
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.Device != _deviceId)
		{
			return;
		}
		
		_movAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		_lookAxis = Input.GetVector("ui_lookLeft", "ui_lookRight", "ui_lookDown", "ui_lookUp");
		
		_shoot = @event.IsActionPressed("ui_accept");
		if (_shoot)
		{
			GD.Print(@event.Device);	
		}

	}
}

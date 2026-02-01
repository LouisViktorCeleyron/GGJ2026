using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class PlayerRef : CharacterBody2D
{
	private List<Module> _modules;
	
	[Export]
	private int _playerIndex;
	public int PlayerIndex => _playerIndex;

	private Vector2 _lookAxis, _movAxis;
	public Vector2 LookAxis => _lookAxis;
	public Vector2 MovAxis => _movAxis;
	
	private bool _shoot;
	public bool Shoot => _shoot;
	
	[Export]
	private Array<Node2D> _scallable;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		GameManager.Instance.AddPlayerRef(this);
		CallDeferred("CallPostReady");
	}

	public float GetScaling()
	{
		return _scallable[0].Scale.X;
	}
	public void Scaling(float scale)
	{
		foreach (var scalable in _scallable)
		{
			scalable.Scale = Vector2.One * scale;
		}
	}

	private void CallPostReady()
	{
		foreach (var module in _modules)
		{
			module.PostReady();
		}
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
		_movAxis = Input.GetVector(
			"ui_left_"+_playerIndex, 
			"ui_right_"+_playerIndex,
			"ui_up"+_playerIndex,
			"ui_down"+_playerIndex);
		_lookAxis = Input.GetVector(
			"ui_lookLeft"+_playerIndex,
			"ui_lookRight"+_playerIndex,
			"ui_lookDown"+_playerIndex,
			"ui_lookUp"+_playerIndex);
		_shoot = Input.IsActionPressed("ui_shoot"+_playerIndex);
	}

}

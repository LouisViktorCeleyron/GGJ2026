using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class PlayerRef : CharacterBody2D
{
	private List<Module> _modules;
	[Signal]
	private delegate void OnScoreChangedEventHandler(int score);
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

	[Export] private SpriteFrames _frames;
	[Export] private AnimatedSprite2D _as;

	private bool _pause = true;
	public bool Pause => _pause;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_as.SpriteFrames = _frames;
		_as.Play();
		GameManager.Instance.AddPlayerRef(this);
		CallDeferred("CallPostReady");
	}

	public void Bridge(int score)
	{
		EmitSignalOnScoreChanged(score);
	}

	public void SetPause(bool status)
	{
		_pause = status;
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
		if (_pause)
		{
			_movAxis = Vector2.Zero;
			_lookAxis= Vector2.Zero;
			_shoot = false;
			return;
		}
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

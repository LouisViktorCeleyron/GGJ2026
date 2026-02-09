using Godot;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class PlayerRef : CharacterBody2D
{
	private List<Module> _modules;
	[Signal]
	private delegate void OnScoreChangedEventHandler(int score);

	[Export]
	public int PlayerIndex { get; private set; }

	private Vector2 _lookAxis, _movAxis;
	public Vector2 LookAxis => _lookAxis;
	public Vector2 MovAxis => _movAxis;

	public bool Shoot { get; private set; }

	[Export]
	private Array<Node2D> _scalable;

	[Export] private SpriteFrames _frames;
	[Export] private AnimatedSprite2D _as;

	public bool Pause { get; private set; } = true;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		var numberOfPlayer = GameManager.GetSettings().GetSettingValue("Joueurs");
		if (numberOfPlayer <= PlayerIndex)
		{
			QueueFree();
			return;
		}
		
		GameManager.Instance.AddPlayerRef(this);
		_as.SpriteFrames = _frames;
		_as.Play();
		CallDeferred("CallPostReady");
	}

	public void SetPause(bool status)
	{
		Pause = status;
	}
	public float GetScaling()
	{
		return _scalable[0].Scale.X;
	}
	public void Scaling(float scale)
	{
		foreach (var scalable in _scalable)
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
		_modules ??= new List<Module>();
		_modules.Add(module);
	}
	public T GetModule<T>() where T : Module
	{
		return (from module in _modules where module.GetType() == typeof(T) select module as T).FirstOrDefault();
	}

	public override void _Input(InputEvent @event)
	{
		if (Pause)
		{
			_movAxis = Vector2.Zero;
			_lookAxis= Vector2.Zero;
			Shoot = false;
			return;
		}
		_movAxis = Input.GetVector(
			"ui_left_"+PlayerIndex, 
			"ui_right_"+PlayerIndex,
			"ui_up"+PlayerIndex,
			"ui_down"+PlayerIndex);
		_lookAxis = Input.GetVector(
			"ui_lookLeft"+PlayerIndex,
			"ui_lookRight"+PlayerIndex,
			"ui_lookDown"+PlayerIndex,
			"ui_lookUp"+PlayerIndex);
		Shoot = _lookAxis.Length() > 0.02f;
		// _shoot = Input.IsActionPressed("ui_shoot"+_playerIndex);
	}

}

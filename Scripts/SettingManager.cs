using Godot;
using System;
using Godot.Collections;

public partial class SettingManager : Node
{
	[Export]
	private Array<SettingElement> _settingsElements;

	private int _current;
	private GameManager _gameManager;
	public override void _Ready()
	{
		base._Ready();
		var children = GetChildren();
		_settingsElements = new Array<SettingElement>();
		foreach (var child in children)
		{
			if (child is SettingElement castedChild)
			{
				_settingsElements.Add(castedChild);
				castedChild.Initialize(this);
			}
		}
		CallDeferred("SetUp");
	}

	private void SetUp()
	{
		Select(0);
		_gameManager = GameManager.Instance;
	}
	private void Select(int index)
	{
		
		var currentIndex = Mathf.Clamp(index, 0, _settingsElements.Count - 1);
		foreach (var element in _settingsElements)
		{
			element.Select(false);
		}
		_settingsElements[currentIndex].Select(true);
		_current = currentIndex;
	}

	public int GetSettingValue(string elementName)
	{
		var element = GetSetting(elementName);
		return element?.Value ?? -1;
	}

	public void SubscribeToSetting(string elementName, SettingElement.OnElementNumberChangeEventHandler action)
	{
		GetSetting(elementName).SubscribeToSetting(action);
	}

	public SettingElement GetSetting(string elementName)
	{
		foreach (var element in _settingsElements)
		{
			if (element.Name == elementName)
			{
				return element;
			}
		}

		return default;
	}

	private bool IsInMenu()
	{
		if (_gameManager is null)
		{
			return false;
		}

		return !_gameManager.IsInGame();
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!IsInMenu())
		{
			return;
		}
		if (Input.IsActionJustPressed("ui_left_0"))
		{
			_settingsElements[_current].AddNumber(-1);
		}
		if (Input.IsActionJustPressed("ui_right_0"))
		{
			_settingsElements[_current].AddNumber(+1);
		}
		if (Input.IsActionJustPressed("ui_up0"))
		{
			Select(_current-1);
		}
		if (Input.IsActionJustPressed("ui_down0"))
		{
			Select(_current+1);
		}
	}
}

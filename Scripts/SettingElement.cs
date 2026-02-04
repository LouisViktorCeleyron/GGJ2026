using Godot;
using System;

public partial class SettingElement : Node
{
	[Export]
	private int _minValue=0,_maxValue=4,_defaultValue=1;

	private int _currentValue;
	[Signal]
	public delegate void OnSelectionEventHandler(bool isSelected);
	[Signal]
	public delegate void OnInitializationEventHandler(string name);
	[Signal]
	public delegate void OnElementNumberChangeEventHandler(int newNumber, int maxNumber);

	[Export] private UiElementNumber _uiFeedback;
	public int Value => _currentValue;
	private SettingManager _manager;
	
	public void Initialize(SettingManager manager)
	{
		_manager = manager;
		OnSelection += _uiFeedback.Selection;
		OnInitialization += _uiFeedback.SetLabelName;
		OnElementNumberChange += _uiFeedback.NumberChange;
		ChangeNumber(_defaultValue);
		EmitSignalOnInitialization(Name);
	}

	public void AddNumber(int toAdd)
	{
		ChangeNumber(_currentValue+toAdd);
	}

	public void ChangeNumber(int newNumber)
	{
		_currentValue = Mathf.Clamp(newNumber, _minValue, _maxValue);
		EmitSignalOnElementNumberChange(_currentValue,_maxValue);
	}

	public void Select(bool isSelected)
	{
		EmitSignalOnSelection(isSelected);
	}

	
}

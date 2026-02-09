using Godot;
using System;
using Godot.Collections;

public partial class UiElementNumber : Control
{
	[Export] private Color _activeColor, _inactiveColor;
	[Export]
	private Array<TextureRect> _numberTexture;
	
	[Export] private Label _label,_numberLabel;

	public void SetLabelName(string name)
	{
		_label.SetText(name);
	}

	public void Selection(bool isSelected)
	{
		Modulate = isSelected? _activeColor:_inactiveColor;
	}

	public void NumberChange(int elementValue, int max)
	{
		if (_numberTexture.Count <= 0)
		{
			ChangeLabel(elementValue);			
		}
		else
		{
			BarFeedback(elementValue,max);
		}
	}

	private void ChangeLabel(int settingValue)
	{
		_numberLabel.SetText(settingValue.ToString());
	}
	private void BarFeedback(int elementValue, int elementMax)
	{
		for (int i = 0; i < elementMax; i++)
		{
			_numberTexture[i].Modulate = i < elementValue ? _activeColor : Colors.Black;
		}
	}
}

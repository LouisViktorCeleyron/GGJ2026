using Godot;

public partial class UiScore : HBoxContainer
{
	[Export] private int _player;
	[Export]
	private Label _scoreLabel,_playerLabel;

	[Export]
	private Separator _separator;

	[Export] private TextureRect _textureRectFlag;
	[Export] private Color _flagColor;
	public override void _Ready()
	{
		base._Ready();
		CallDeferred("DeferredReady");
	}

	private void DeferredReady()
	{
		GameManager.Instance.StartGameSignal += SubscribedAtStart;
	}
	private void SubscribedAtStart()
	{
		var players = GameManager.Instance.Players;
		var visible = false;
		_textureRectFlag.SelfModulate = _flagColor;
		_scoreLabel.Text = "00";
		foreach (var p in players)
		{
			if (p.PlayerIndex != _player) continue;
			p.GetModule<ScoreModule>().OnChangeScore += SetScore;
			visible = true;
			_playerLabel?.SetText( $"P{_player+1}:");
			break;
		}
		SetVisible(visible);
	}

	private void SetScore(string i)
	{
		_scoreLabel.Text = i;
	}

	public void SetVisibility(bool newVisibility)
	{
		//check pk ça marche ap
		Visible = newVisibility;
		_separator?.SetVisible(newVisibility);
	}
	
}

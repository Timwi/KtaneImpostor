using System.Text;
using UnityEngine;

public abstract class WordModule : MonoBehaviour
{
	public TextMesh PuzzleDisplay;
	public TextMesh AnswerDisplay;
	public KMSelectable[] Buttons;
	public KMSelectable DeleteButton;
	public KMSelectable EnterButton;
	public KMAudio KMAudio;

	protected string Answer;
	protected string Puzzle;

	protected void SetUpButtons()
	{
		for (var i = 0; i < Buttons.Length; i++)
		{
			var buttonText = Buttons[i].GetComponentInChildren<TextMesh>();
			buttonText.text = Puzzle[i].ToString();
			Buttons[i].OnInteract += delegate
			{
                KMAudio.HandlePlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
				Answer += buttonText.text;
				SetAnswerDisplay(Answer);
				return false;
			};
		}

		DeleteButton.OnInteract += delegate
		{
			KMAudio.HandlePlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Answer = string.Empty;
			SetAnswerDisplay(Answer);
			return false;
		};

		EnterButton.OnInteract += delegate
		{
			KMAudio.HandlePlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			if (Solve())
				GetComponent<KMBombModule>().HandlePass();
			else
				GetComponent<KMBombModule>().HandleStrike();

			Answer = string.Empty;
			SetAnswerDisplay(Answer);
			return false;
		};
	}

	protected abstract bool Solve();

	protected void SetPuzzleDisplay(string text)
	{
		PuzzleDisplay.text = text;
	}

	protected void SetAnswerDisplay(string text)
	{
		AnswerDisplay.text = text;
	}

	protected string ScrambleWord(string solution)
	{
		var scrambled = string.Empty;
		var builder = new StringBuilder(string.Copy(solution));

		while (builder.Length > 0)
		{
			var randomChar = Random.Range(0, builder.Length);

			scrambled += builder[randomChar];
			builder.Remove(randomChar, 1);
		}

		return scrambled;
	}
}
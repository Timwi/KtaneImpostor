using System.Collections.Generic;
using UnityEngine;

public class AnagramsModule : WordModule
{
	private static readonly IList<IList<string>> Words = new List<IList<string>>()
	{
		new List<string> {"stream", "master", "tamers"},
		new List<string> {"looped", "poodle", "pooled"},
		new List<string> {"cellar", "caller", "recall"},
		new List<string> {"seated", "sedate", "teased"},
		new List<string> {"rescue", "secure", "recuse"},
		new List<string> {"rashes", "shears", "shares"},
		new List<string> {"barely", "barley", "bleary"},
		new List<string> {"duster", "rusted", "rudest"}
	};

	private IList<string> _solution;

	void Start()
	{
		Init();
	}

	private void Init()
	{
		SetUpPuzzle();
		SetAnswerDisplay(string.Empty);
		SetPuzzleDisplay(Puzzle);
		SetUpButtons();
	}

	private void SetUpPuzzle()
	{
		var puzzleKey = Random.Range(0, Words.Count);
		var wordList = Words[puzzleKey];
		var puzzleChoice = Random.Range(0, wordList.Count);

		Puzzle = wordList[puzzleChoice];
		_solution = new List<string>();

		for (var i = 0; i < wordList.Count; i++)
		{
			if (i != puzzleChoice)
				_solution.Add(wordList[i]);
		}
	}

	protected override bool Solve()
	{
		return _solution.Contains(Answer);
	}
}
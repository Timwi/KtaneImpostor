using System.Collections.Generic;
using UnityEngine;

public class WordScrambleModule : WordModule {
	private string _solution;

	private static readonly IList<string> Words = new List<string>()
	{
		"module",
		"ottawa",
		"banana",
		"kaboom",
		"letter",
		"widget",
		"person",
		"sapper",
		"wiring",
		"archer",
		"device",
		"rocket",
		"damage",
		"defuse",
		"flames",
		"semtex",
		"cannon",
		"blasts",
		"attack",
		"weapon",
		"charge",
		"napalm",
		"mortar",
		"bursts",
		"casing",
		"disarm",
		"keypad",
		"button",
		"robots",
		"kevlar"
	};

	void Start () {
		Init();
	}

	void Init()
	{
		var wordIndex = Random.Range(0, Words.Count);
		_solution = Words[wordIndex];
		Answer = string.Empty;

		Puzzle = ScrambleWord(_solution);
		SetAnswerDisplay(string.Empty);
		SetPuzzleDisplay(Puzzle);
		SetUpButtons();
	}

	protected override bool Solve()
	{
		return Answer.Equals(_solution);
	}
}

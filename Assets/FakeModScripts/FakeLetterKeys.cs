using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

public class FakeLetterKeys : ImposterMod
{

	[SerializeField]
	private TextMesh[] texts;
	[SerializeField]
	private TextMesh number;

	private string[] ordinals = { "1st", "2nd", "3rd", "4th" };
	private int chosenCase;
	private int changedKey;

    void Start()
	{
		string num = Rnd.Range(0, 100).ToString();
		string[] letters = new string[] { "A", "B", "C", "D" }.Shuffle();
		chosenCase = Rnd.Range(0, 2);
		if (chosenCase == 0)
        {
			changedKey = Rnd.Range(0, 4);
			flickerObjs.Add(texts[changedKey].gameObject);
			switch (Rnd.Range(0, 3))
			{
				case 0:
					letters[changedKey] = Rnd.Range(0, 10).ToString();
					Log(string.Format("The {0} key is changed to a number!", ordinals[changedKey]));
					break;
				case 1:
					letters[changedKey] = letters.Where(x => x != letters[changedKey]).PickRandom();
					Log(string.Format("There is a duplicate {0}!", letters[changedKey]));
					break;
				case 2:
					letters[changedKey] = "EFGHIJKLMNOPQRSTUVWXYZ".PickRandom().ToString();
					Log(string.Format("The {0} key is changed to letter {1}!", ordinals[changedKey], letters[changedKey]));
					break;
			}
        }
        else
        {
			flickerObjs.Add(number.gameObject);
			num = Rnd.Range(0, 9).ToString() + letters.PickRandom();
			if (Rnd.Range(0, 2) == 0)
				num = num.Reverse().Join("");
			Log(string.Format("The display doesn't have a number, it contains the letter {0}!", num.First(x => letters.Contains(x.ToString()))));
        }
		for (int i = 0; i < 4; i++)
			texts[i].text = letters[i];
		number.text = num;

		

	}
}

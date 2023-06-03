using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

public class FakeLetterKeys : ImpostorMod
{
	public override string ModAbbreviation { get { return "Lk"; } }
	public TextMesh[] texts;
	public TextMesh number;

	private int changedKey;

    void Start()
	{
		string num = Rnd.Range(0, 100).ToString();
		string[] letters = new string[] { "A", "B", "C", "D" }.Shuffle();
		if (Ut.RandBool())
        {
			changedKey = Rnd.Range(0, 4);
			AddFlicker(texts[changedKey]);
			switch (Rnd.Range(0, 3))
			{
				case 0:
					letters[changedKey] = Rnd.Range(0, 10).ToString();
					LogQuirk("the {0} key is changed to a number ({1})", Ut.Ordinal(changedKey + 1), texts[changedKey].text);
					break;
				case 1:
					letters[changedKey] = letters.Where(x => x != letters[changedKey]).PickRandom();
					LogQuirk("there is a duplicate {0}", letters[changedKey]);
					break;
				case 2:
					letters[changedKey] = "EFGHIJKLMNOPQRSTUVWXYZ".PickRandom().ToString();
					LogQuirk("the {0} key is changed to letter {1}", Ut.Ordinal(changedKey + 1), letters[changedKey]);
					break;
			}
        }
        else
        {
			AddFlicker(number);
			num = Rnd.Range(0, 9).ToString() + letters.PickRandom();
			if (Ut.RandBool())
				num = num.Reverse().Join("");
			LogQuirk("the display says {0}", num);
        }
		for (int i = 0; i < 4; i++)
			texts[i].text = letters[i];
		number.text = num;

		

	}
}

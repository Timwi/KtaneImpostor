using UnityEngine;
using System.Linq;
using Rnd = UnityEngine.Random;

public class FakeGamepad : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Gp"; } }
    public TextMesh[] arrowButtonTexts, letterButtonTexts, displayTexts;
    private int Case;

    void Start()
    {
        displayTexts[0].text = Rnd.Range(0, 99).ToString("00");
        displayTexts[1].text = Rnd.Range(0, 99).ToString("00");

        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0:
                var arrowTexts = new string[] { "▶", "▲", "◀", "▼" };
                for (int i = 0; i < 4; i++)
                {
                    arrowButtonTexts[i].text = arrowTexts[(i + 2) % 4];
                    flickerObjs.Add(arrowButtonTexts[i].gameObject);
                }
                LogQuirk("the arrow buttons point in the opposite directions");
                break;
            case 1:
                var allowedLetters = "ABCDEFHJLPUY";
                var deaf = Rnd.Range(0, 5) == 0;
                if (deaf)
                {
                    displayTexts[0].text = "DE";
                    displayTexts[1].text = "AF";
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                        displayTexts[i].text = allowedLetters.PickRandom().ToString() + allowedLetters.PickRandom().ToString();
                }
                flickerObjs.AddRange(displayTexts.Select(x => x.gameObject));
                LogQuirk("the display texts show letters");
                break;
            case 2:
                var AB = "AB";
                var rndLet = Rnd.Range(0, 2);
                letterButtonTexts[rndLet].text = AB[(rndLet + 1) % 2].ToString();
                flickerObjs.Add(letterButtonTexts[rndLet].gameObject);
                LogQuirk("There are two buttons with {0} written on them", AB[rndLet]);
                break;
        }
    }
}

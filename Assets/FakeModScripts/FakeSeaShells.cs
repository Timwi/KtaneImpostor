using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeSeaShells : ImpostorMod
{
    public override string ModAbbreviation { get { return "Seas"; } }
    public TextMesh DisplayText;
    public TextMesh[] BtnTexts;
    private static readonly string[][] _screenPhrases = new string[][] {
        new string[] { "she sells ", "she shells ", "sea shells ", "sea sells " },
        new string[] { "sea\nshells ", "she\nshells ", "sea\nsells ", "she\nsells " },
        new string[] { "the\nsea shore", "the\nshe sore", "the\nshe sure", "the\nseesaw" }
    };
    private static readonly string[][] _btnTextsShort = new string[][] {
        new string[] { "she", "sit", "sushi", "shoe" },
        new string[] { "tutu", "can", "cancan", "2" },
        new string[] { "stitch", "twitch", "itch", "witch" },
        new string[] { "burger", "llama", "armour", "bulgaria" }
    };
    private static readonly string[] _btnTextsLong = new string[] { "shih tzu", "toucan", "switch", "burglar alarm" };
    private static readonly string[][] _fakeScreenPhrases = new string[][]
    {
        new string[] {"she smells ", "she spells ", "she swells ", "sea smells ", "sea spells ", "sea swells "},
        new string[] {"she\nsmells ", "she\nspells ", "she\nswells ", "sea\nsmells ", "sea\nspells ", "sea\nswells "},
        new string[] {"the\ndark web", "the\nblack market", "\ncraigslist", "\nebay", "\ngumtree","\ndeez nuts"}
    };
    private static readonly string[] _fakeBtnTexts = new string[] { "sussy", "cannot", "bitch", "boomer" };
    private string _dispText;
    private int Case;

    void Start()
    {
        _dispText += _screenPhrases[0][Rnd.Range(0, _screenPhrases[0].Length)];
        _dispText += _screenPhrases[1][Rnd.Range(0, _screenPhrases[1].Length)];
        _dispText += "on ";
        _dispText += _screenPhrases[2][Rnd.Range(0, _screenPhrases[2].Length)];
        DisplayText.text = _dispText;
        var chosenWordIx = Rnd.Range(0, _btnTextsShort.Length);
        var chosenWordList = _btnTextsShort[chosenWordIx].Shuffle().ToList();
        chosenWordList.Add(_btnTextsLong[chosenWordIx]);
        for (int i = 0; i < BtnTexts.Length - 1; i++)
            BtnTexts[i].text = chosenWordList[i];
        BtnTexts[4].text = chosenWordList[4];

        Case = Rnd.Range(0, 5); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                _dispText = "";
                _dispText += _fakeScreenPhrases[0][Rnd.Range(0, _fakeScreenPhrases[0].Length)];
                _dispText += _screenPhrases[1][Rnd.Range(0, _screenPhrases[1].Length)];
                _dispText += "on ";
                _dispText += _screenPhrases[2][Rnd.Range(0, _screenPhrases[2].Length)];
                DisplayText.text = _dispText;
                flickerObjs.Add(DisplayText.gameObject);
                LogQuirk("the display reads {0}", _dispText);
                break;
            case 1:
                _dispText = "";
                _dispText += _screenPhrases[0][Rnd.Range(0, _screenPhrases[0].Length)];
                _dispText += _screenPhrases[1][Rnd.Range(0, _screenPhrases[1].Length)];
                _dispText += "on ";
                _dispText += _fakeScreenPhrases[2][Rnd.Range(0, _fakeScreenPhrases[2].Length)];
                DisplayText.text = _dispText;
                flickerObjs.Add(DisplayText.gameObject);
                LogQuirk("the display reads {0}", _dispText.ToUpperInvariant());
                break;
            case 2:
                _dispText = "";
                _dispText += _fakeScreenPhrases[0][Rnd.Range(0, _fakeScreenPhrases[0].Length)];
                _dispText += _fakeScreenPhrases[1][Rnd.Range(0, _fakeScreenPhrases[1].Length)];
                _dispText += "on ";
                _dispText += _screenPhrases[2][Rnd.Range(0, _screenPhrases[2].Length)];
                DisplayText.text = _dispText;
                flickerObjs.Add(DisplayText.gameObject);
                LogQuirk("the display reads {0}", _dispText);
                break;
            case 3:
                var rndBtn = Rnd.Range(0, 4);
                BtnTexts[rndBtn].text = _fakeBtnTexts[chosenWordIx];
                flickerObjs.Add(BtnTexts[rndBtn].gameObject);
                LogQuirk("one of the buttons says {0}", _fakeBtnTexts[chosenWordIx].ToUpperInvariant());
                break;
            case 4:
                var rndBtn1 = Rnd.Range(0, 4);
                newBtn:
                var rndBtn2 = Rnd.Range(0, 4);
                if (rndBtn2 == rndBtn1)
                    goto newBtn;
                BtnTexts[rndBtn1].text = BtnTexts[rndBtn2].text;
                flickerObjs.Add(BtnTexts[rndBtn1].gameObject);
                flickerObjs.Add(BtnTexts[rndBtn2].gameObject);
                LogQuirk("two buttons have the same text {0}", _fakeBtnTexts[chosenWordIx].ToUpperInvariant());
                break;
        }
    }
}

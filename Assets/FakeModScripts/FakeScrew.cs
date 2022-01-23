using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeScrew : ImpostorMod
{
    [SerializeField]
    private GameObject[] screwHoles;
    [SerializeField]
    private Texture[] screwTextures;
    [SerializeField]
    private TextMesh[] buttonTexts;
    [SerializeField]
    private TextMesh screenText;
    [SerializeField]
    private Texture cyanTexture;

    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 5); //However many cases you want there to be.
        var shuff = Enumerable.Range(0, 6).ToArray().Shuffle();
        for (int i = 1; i < screwHoles.Length; i++)
            screwHoles[i].GetComponent<MeshRenderer>().material.mainTexture = screwTextures[shuff[i]];
        var btnLetters = Enumerable.Range(0, 4).ToArray().Shuffle();
        for (int i = 0; i < buttonTexts.Length; i++)
            buttonTexts[i].text = "ABCD"[btnLetters[i]].ToString();
        screenText.text = "1";
        switch (Case)
        {
            case 0:
                dupeHoles:
                var rnd1 = Rnd.Range(1, 6);
                var rnd2 = Rnd.Range(1, 6);
                if (rnd1 == rnd2)
                    goto dupeHoles;
                var dupeShuff = Enumerable.Range(0, 6).ToArray().Shuffle();
                dupeShuff[rnd1] = dupeShuff[rnd2];
                flickerObjs.Add(screwHoles[rnd1]);
                flickerObjs.Add(screwHoles[rnd2]);
                for (int i = 0; i < screwHoles.Length; i++)
                    screwHoles[i].GetComponent<MeshRenderer>().material.mainTexture = screwTextures[dupeShuff[i]];
                LogQuirk("there was a duplicate colored hole, at holes {0} and {1}", rnd1 + 1, rnd2 + 1);
                break;
            case 1:
                var numShuff = Enumerable.Range(0, 4).ToArray().Shuffle();
                for (int i = 0; i < buttonTexts.Length; i++)
                {
                    buttonTexts[i].text = (numShuff[i] + 1).ToString();
                    flickerObjs.Add(buttonTexts[i].gameObject);
                }
                LogQuirk("the buttons have numbers instead of letters");
                break;
            case 2:
                fakeLetter:
                var newLetters = Enumerable.Range(0, 4).ToArray();
                var btnRand = Rnd.Range(0, 4);
                var letterRand = Rnd.Range(4, 26);
                if (btnRand == letterRand || letterRand == 8) // I may look too much like 1
                    goto fakeLetter;
                newLetters[btnRand] = letterRand;
                newLetters.Shuffle();
                flickerObjs.Add(buttonTexts[Array.IndexOf(newLetters, letterRand)].gameObject);
                for (int i = 0; i < 4; i++)
                    buttonTexts[i].text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[newLetters[i]].ToString();
                LogQuirk("one of the buttons had the letter {0}", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[letterRand]);
                break;
            case 3:
                var cyanHole = Rnd.Range(1, 6);
                screwHoles[cyanHole].GetComponent<MeshRenderer>().material.mainTexture = cyanTexture;
                flickerObjs.Add(screwHoles[cyanHole]);
                LogQuirk("there was a cyan colored hole");
                break;
            case 4:
                screenText.text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)].ToString();
                flickerObjs.Add(screenText.gameObject);
                LogQuirk("the screen had a letter instead of a number");
                break;
        }
        // LogQuirk("Test message 2+2={0}", 2 + 2);
    }
}

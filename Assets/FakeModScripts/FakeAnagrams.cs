using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeAnagrams : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] buttonTexts;
    [SerializeField]
    private TextMesh topDisp, botDisp;
    public override SLPositions SLPos  
    { get { return SLPositions.TL; } } 
    private int Case;

    private static readonly string[] anagrams = { "STREAM", "MASTER", "TAMERS", "LOOPED", "POODLE", "POOLED", "CELLAR", "CALLER", "RECALL", "SEATED", "SEDATE", "TEASED", "RESCUE", "SECURE", "RECUSE", "RASHES", "SHEARS", "SHARES", "BARELY", "BARLEY", "BLEARY", "DUSTER", "RUSTED", "RUDEST" };
    private string chosenWord;

    void Start()
    {
        Case = Rnd.Range(0, 2);
        if (Ut.RandBool())
        {
                chosenWord = anagrams.PickRandom();
                LogQuirk("the anagram is on the bottom screen");
                botDisp.text = chosenWord;
                flickerObjs.Add(botDisp.gameObject);
                for (int i = 0; i < 6; i++)
                    buttonTexts[i].text = chosenWord[i].ToString();
        }
        else
        {
            chosenWord = anagrams.PickRandom();
            LogQuirk("DEL and OK are on the left");
            topDisp.text = chosenWord;
            for (int i = 0; i < 8; i++)
                flickerObjs.Add(buttonTexts[i].gameObject);
            buttonTexts[0].text = "DEL";
            buttonTexts[3].text = "OK";
            int[] order = { 1, 2, 6, 4, 5, 7 };
            for (int i = 0; i < 6; i++)
                buttonTexts[order[i]].text = chosenWord[i].ToString();

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeAnagrams : ImpostorMod
{
    public override string ModAbbreviation { get { return "Ag"; } }
    public TextMesh[] buttonTexts;
    public TextMesh topDisp, botDisp;
    public override SLPositions SLPos
    { get { return SLPositions.TL; } }

    private static readonly string[] anagrams = { "STREAM", "MASTER", "TAMERS", "LOOPED", "POODLE", "POOLED", "CELLAR", "CALLER", "RECALL", "SEATED", "SEDATE", "TEASED", "RESCUE", "SECURE", "RECUSE", "RASHES", "SHEARS", "SHARES", "BARELY", "BARLEY", "BLEARY", "DUSTER", "RUSTED", "RUDEST" };
    private string chosenWord;
    private static readonly string consonants = "BCDFGHJKLMNPQRSTVWXZ";

    void Start()
    {
        if (Ut.RandBool())
        {
            chosenWord = anagrams.PickRandom();
            LogQuirk("the anagram is on the bottom screen");
            botDisp.text = chosenWord;
            AddFlicker(botDisp);
        }
        else
        {
            for (int i = 0; i < 6; i++)
                chosenWord += consonants.PickRandom();
            LogQuirk("the \"anagram\" consists of only consonants");
            topDisp.text = chosenWord;
            AddFlicker(topDisp);
        }
        for (int i = 0; i < 6; i++)
            buttonTexts[i].text = chosenWord[i].ToString();
    }
}

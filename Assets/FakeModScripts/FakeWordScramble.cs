using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeWordScramble : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Ws"; } }
    public TextMesh[] buttonTexts;
    public TextMesh topDisp, botDisp;

    private static readonly string[] wordscramble = { "ARCHER", "ATTACK", "BANANA", "BLASTS", "BURSTS", "BUTTON", "CANNON", "CASING", "CHARGE", "DAMAGE", "DEFUSE", "DEVICE", "DISARM", "FLAMES", "KABOOM", "KEVLAR", "KEYPAD", "LETTER", "MODULE", "MORTAR", "NAPALM", "OTTAWA", "PERSON", "ROBOTS", "ROCKET", "SAPPER", "SEMTEX", "WEAPON", "WIDGET", "WIRING" };
    private string chosenWord;
    private static readonly string consonants = "BCDFGHJKLMNPQRSTVWXZ";

    void Start()
    {
        if (Ut.RandBool())
        {
            chosenWord = wordscramble.PickRandom().ToCharArray().Shuffle().Join("");
            LogQuirk("the word is on the bottom screen");
            botDisp.text = chosenWord;
            AddFlicker(botDisp);
        }
        else
        {
            for (int i = 0; i < 6; i++)
                chosenWord += consonants.PickRandom();
            LogQuirk("the scrambled \"word\" consists of only consonants");
            topDisp.text = chosenWord;
            AddFlicker(topDisp);
        }
        for (int i = 0; i < 6; i++)
            buttonTexts[i].text = chosenWord[i].ToString();
    }
}

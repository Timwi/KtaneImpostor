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

    void Start()
    {
        if (Ut.RandBool())
        {
            chosenWord = wordscramble.PickRandom().ToCharArray().Shuffle().Join("");
            LogQuirk("the word is on the bottom screen");
            botDisp.text = chosenWord;
            flickerObjs.Add(botDisp.gameObject);
            for (int i = 0; i < 6; i++)
                buttonTexts[i].text = chosenWord[i].ToString();
        }
        else
        {
            chosenWord = wordscramble.PickRandom().ToCharArray().Shuffle().Join("");
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

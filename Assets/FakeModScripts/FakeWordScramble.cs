using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeWordScramble : ImposterMod 
{
    [SerializeField]
    private TextMesh[] buttonTexts;
    [SerializeField]
    private TextMesh topDisp, botDisp;
    private int Case;

    private static readonly string[] anagrams = { "STREAM", "MASTER", "TAMERS", "LOOPED", "POODLE", "POOLED", "CELLAR", "CALLER", "RECALL", "SEATED", "SEDATE", "TEASED", "RESCUE", "SECURE", "RECUSE", "RASHES", "SHEARS", "SHARES", "BARELY", "BARLEY", "BLEARY", "DUSTER", "RUSTED", "RUDEST" };
    private static readonly string[] wordscramble = { "ARCHER", "ATTACK", "BANANA", "BLASTS", "BURSTS", "BUTTON", "CANNON", "CASING", "CHARGE", "DAMAGE", "DEFUSE", "DEVICE", "DISARM", "FLAMES", "KABOOM", "KEVLAR", "KEYPAD", "LETTER", "MODULE", "MORTAR", "NAPALM", "OTTAWA", "PERSON", "ROBOTS", "ROCKET", "SAPPER", "SEMTEX", "WEAPON", "WIDGET", "WIRING" };
    private string chosenWord;

    void Start()
    {
        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0:
                chosenWord = anagrams.PickRandom();
                Log("...there's an anagram on the screen, that doesn't seem normal");
                topDisp.text = chosenWord;
                flickerObjs.Add(topDisp.gameObject);
                for (int i = 0; i < 6; i++)
                    buttonTexts[i].text = chosenWord[i].ToString();
                break;
            case 1:
                chosenWord = wordscramble.PickRandom().ToCharArray().Shuffle().Join("");
                Log("...the word is on the bottom screen, that doesn't seem normal.");
                botDisp.text = chosenWord;
                flickerObjs.Add(botDisp.gameObject);
                for (int i = 0; i < 6; i++)
                    buttonTexts[i].text = chosenWord[i].ToString();
                break;
            case 2:
                chosenWord = wordscramble.PickRandom().ToCharArray().Shuffle().Join("");
                Log("...DEL and OK are on the left, that doesn't seem normal.");
                topDisp.text = chosenWord;
                for (int i = 0; i < 8; i++)
                    flickerObjs.Add(buttonTexts[i].gameObject);
                buttonTexts[0].text = "DEL";
                buttonTexts[3].text = "OK";
                int[] order = { 1, 2, 6, 4, 5, 7 };
                for (int i = 0; i < 6; i++)
                    buttonTexts[order[i]].text = chosenWord[i].ToString();
                break;
        }
    }
}

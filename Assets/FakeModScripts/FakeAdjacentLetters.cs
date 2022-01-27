using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeAdjacentLetters : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Adj"; } }
    public TextMesh[] labels;
    private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    void Start()
    {
        char[] letterOrder = alphabet.ToArray().Shuffle();
        for (int i = 0; i < 12; i++)
            labels[i].text = letterOrder[i].ToString();
        if (Ut.RandBool())
        {
            int changedPos = Rnd.Range(0, 12);
            labels[changedPos].text = Rnd.Range(0, 9).ToString();
            flickerObjs.Add(labels[changedPos].gameObject);
            LogQuirk("the {0} button has a {1} on it", Ut.Ordinal(changedPos + 1), labels[changedPos].text);
        }
        else
        {
            string dupeLetter = alphabet.PickRandom().ToString();
            int[] positions = Enumerable.Range(0, 12).ToArray().Shuffle().Take(Rnd.Range(2, 5)).ToArray();
            foreach (int pos in positions)
            {
                flickerObjs.Add(labels[pos].gameObject);
                labels[pos].text = dupeLetter;
            }
            LogQuirk("there are duplicates of the letter {0}", dupeLetter);
        }
    }
}

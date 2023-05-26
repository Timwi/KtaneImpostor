using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeConnectionCheck : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Cck"; } }

    public GameObject[] redLeds, greenLeds;
    public TextMesh[] texts;
    public Material[] susmats;
    public GameObject[] ccwtf;
    private static readonly string[] fakeWords = {"CHEGG", "CHAIR", "CHORTLE", "CHALK", "CHAT", "CHEAP", "CHEAT", "CHIP", "CHILL", "CHIME", "CHOP", "CHUNK", "CHEW", "CHUCK", "CHAIN", "CHEESE", "CHOOSE", "CHARGE", "CHOW"};

    private int Case;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            (Ut.RandBool() ? redLeds[i] : greenLeds[i]).SetActive(true);
            var pair = Enumerable.Range(1, 8).ToList().Shuffle(); //Makes sure that the two numbers aren't equal.
            texts[2 * i].text = pair[0].ToString();
            texts[2 * i + 1].text = pair[1].ToString();
        }

        int changedPos = Rnd.Range(0, 8);

        Case = Rnd.Range(0, 4);
        Case = 3; //ZAMN

        switch (Case) {
            case 0:
                flickerObjs.Add(texts[changedPos].gameObject);
                int newVal = Ut.RandBool() ? 0 : 9;
                texts[changedPos].text = newVal.ToString();
                LogQuirk("there is a {0}", newVal);
            break;
            case 1:
                flickerObjs.Add(texts[changedPos].gameObject);
                int adjacentPos = changedPos % 2 == 0 ? changedPos + 1 : changedPos - 1;
                texts[changedPos].text = texts[adjacentPos].text;
                flickerObjs.Add(texts[adjacentPos].gameObject);
                LogQuirk("there are two numbers that are the same on the same pair");
            break;
            case 2: //added by Blan
                changedPos = changedPos % 4;
                flickerObjs.Add(ccwtf[changedPos].gameObject);
                bool rng = Ut.RandBool();
                redLeds[changedPos].GetComponent<MeshRenderer>().material = susmats[rng ? 0 : 1];
                greenLeds[changedPos].GetComponent<MeshRenderer>().material = susmats[rng ? 0 : 1];
                LogQuirk("there is a {0} LED", rng ? "yellow" : "blue");
            break;
            case 3:
                flickerObjs.Add(texts[8].gameObject);
                string word = fakeWords.PickRandom();
                texts[8].text = word;
                LogQuirk("the button says '{0}'", word);
            break;
        }
    }
}

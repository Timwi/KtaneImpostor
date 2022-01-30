using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeMorsematics : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Mmt"; } }
    public MeshRenderer lights;
    public Transform submissionArea;

    private Color OFF = Color.black, ON = new Color(0.7f, 0.6f, 0.2f, 0.4f);
    int Case;

    string[] morses = new[] { "x xxx", "xxx x x x", "xxx x xxx x", "xxx x x x", "x", "x x xxx x", "xxx xxx x", "x x x x", "x x", "x xxx xxx xxx", "xxx x xxx", "x xxx x x", "xxx xxx", "xxx x", "xxx xxx xxx", "x xxx xxx x", "xxx xxx x xxx", "x xxx x", "x x x", "xxx", "x x xxx", "x x x xxx", "x xxx xxx", "xxx x x xxx", "xxx x xxx xxx", "xxx xxx x x" }
                                            .Select(str => str + "   ").ToArray(); //3 space buffer

    string[] sequences = new string[3];
    bool[] lightStates = new bool[3];

    void Start()
    {
        Case = Rnd.Range(0, 3);
        morses.Shuffle();
        for (int i = 0; i < 3; i++)
            sequences[i] = morses[i];
        switch (Case)
        {
            case 0:
                int brokenLED = Rnd.Range(0, 3);
                sequences[brokenLED] = " ";
                LogQuirk("the {0} LED is completely off", Ut.Ordinal(3 - brokenLED));
                flickerObjs.Add(lights.gameObject);
                break;
            case 1:
                LogQuirk("the LEDs are flashing irregularly");
                flickerObjs.Add(lights.gameObject);
                break;
            case 2:
                LogQuirk("the submit button and display are flipped");
                submissionArea.localScale = new Vector3(-1, +1, +1);
                flickerObjs.Add(submissionArea.gameObject);
                break;
        }
    }
    public override void OnActivate()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Case == 1)
                StartCoroutine(GoOffOomfie(i));
            else StartCoroutine(FlashSequence(i));
        }
    }
    IEnumerator FlashSequence(int pos)
    {
        while (true)
        {
            for (int pointer = 0; pointer < sequences[pos].Length; pointer++)
            {
                lights.materials[pos + 1].color = sequences[pos][pointer] == 'x' ? ON : OFF;
                yield return new WaitForSeconds(0.24f);
            }
        }
    }
    IEnumerator GoOffOomfie(int pos)
    {
        while (true)
        {
            lights.materials[pos + 1].color = lightStates[pos] ? ON : OFF;
            lightStates[pos] ^= true;
            yield return new WaitForSeconds(Rnd.Range(0.1f, 1));
        }
    }
}

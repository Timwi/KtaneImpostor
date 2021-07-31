using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeSwitches : ImpostorMod 
{
    [SerializeField]
    private Transform[] switches;
    [SerializeField]
    private MeshRenderer[] leds;
    private int Case;
    private bool[] switchPositions = new bool[5];
    private bool[] ledPositions = new bool[5];
    private bool[] ledVals = new bool[10];
    private int changedPos;
    private static readonly string[] ordinals = { "1st", "2nd", "3rd", "4th", "5th" };

    void Start()
    {
        Case = Rnd.Range(0, 3);
        for (int i = 0; i < 5; i++)
            switchPositions[i] = Rnd.Range(0, 2) == 0;
        if (Case == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                ledPositions[i] = switchPositions[i];
                flickerObjs.Add(switches[i].gameObject);
                flickerObjs.Add(leds[i].gameObject);
                flickerObjs.Add(leds[i + 5].gameObject);
            }
            Log("...the switches are already in the correct positions, that doesn't seem like normal.");
        }
        else
            do
                for (int i = 0; i < 5; i++)
                    ledPositions[i] = Rnd.Range(0, 2) == 0;
            while (switchPositions.SequenceEqual(ledPositions));
        if (Case == 1)
        {
            for (int i = 0; i < 5; i++)
                flickerObjs.Add(switches[i].gameObject);
            Log("...the switches are all in the middle, that doesn't seem normal.");
        }
        else
            for (int i = 0; i < 5; i++)
                switches[i].localEulerAngles = (switchPositions[i] ? 50 : -50) * Vector3.right;
        if (Case == 2)
        {
            changedPos = Rnd.Range(0, 10);
            Log(string.Format("...the {0} LED pair has two of the same state.", ordinals[changedPos % 5]));
        }
    }
    public override void OnActivate()
    {

        for (int i = 0; i < 5; i++)
            ledVals[ledPositions[i] ? i : i + 5] = true;
        if (Case == 2)
        {
            ledVals[changedPos] = !ledVals[changedPos];
            flickerObjs.Add(leds[changedPos].gameObject);
            flickerObjs.Add(leds[(changedPos + 5) % 10].gameObject);
        }
        for (int i = 0; i < 10; i++)
            if (ledVals[i])
                leds[i].material.color = new Color32(72, 255, 0, 255);
    }
}

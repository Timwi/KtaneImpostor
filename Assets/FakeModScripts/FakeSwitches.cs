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

    void Start()
    {
        Case = Rnd.Range(0, 3);
        for (int i = 0; i < 5; i++)
            switchPositions[i] = Ut.RandBool();
        if (Case == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                ledPositions[i] = switchPositions[i];
                flickerObjs.Add(switches[i].gameObject);
                flickerObjs.Add(leds[i].gameObject);
                flickerObjs.Add(leds[i + 5].gameObject);
            }
            LogQuirk("the switches are already in the correct positions");
        }
        else
        {
            do
                for (int i = 0; i < 5; i++)
                    ledPositions[i] = Ut.RandBool();
            while (switchPositions.SequenceEqual(ledPositions));
        }
        if (Case == 1)
        {
            for (int i = 0; i < 5; i++)
                flickerObjs.Add(switches[i].gameObject);
            LogQuirk("the switches are all in the middle");
        }
        else
            for (int i = 0; i < 5; i++)
                switches[i].localEulerAngles = (switchPositions[i] ? 50 : -50) * Vector3.right;
        if (Case == 2)
        {
            changedPos = Rnd.Range(0, 10);
            LogQuirk("the {0} LED pair has two of the same state.", Ut.Ordinal(changedPos % 5 + 1));
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

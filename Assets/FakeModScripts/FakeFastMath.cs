using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

// Made by Kilo Bites

public class FakeFastMath : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Fast Math"; } }
    public override SLPositions SLPos  //Can be ignored if SL Position is TR
    { get { return SLPositions.TR; } }
    public TextMesh display, goText;
    private int Case;

    private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private int[] blan = new int[2];
    private string blan2;

    void Start()
    {
        Case = Rnd.Range(0, 2);
        switch (Case)
        {
            case 0:
                goText.text = "STOP!";
                blan2 = string.Format("{0} {1}", alphabet.PickRandom(), alphabet.PickRandom());
                AddFlicker(buttons[11].gameObject);
                LogQuirk("the red button says STOP! ");
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    blan[i] = Rnd.Range(0, 10);
                }
                blan2 = string.Format("{0} {1}", blan[0], blan[1]);
                LogQuirk("the screen displays numbers");
                AddFlicker(display.gameObject);
                break;
        }
    }
    public override void OnActivate()
    {
        display.text = blan2;
    }
}

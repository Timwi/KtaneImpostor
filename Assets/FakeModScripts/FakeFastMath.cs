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
    public override string ModAbbreviation { get { return "Fm"; } }
    public TextMesh display, goText;
    private int Case;

    private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string displayedText;

    void Start()
    {
        Case = Rnd.Range(0, 2);
        switch (Case)
        {
            case 0:
                goText.text = "STOP!";
                displayedText = string.Format("{0} {1}", alphabet.PickRandom(), alphabet.PickRandom());
                AddFlicker(buttons[11].gameObject);
                LogQuirk("the red button says STOP! ");
                break;
            case 1:
                int[] dispNums = new int[2]; 
                for (int i = 0; i < 2; i++)
                    dispNums[i] = Rnd.Range(0, 10);
                displayedText = dispNums.Join();
                LogQuirk("the screen displays numbers");
                AddFlicker(display.gameObject);
                break;
        }
    }
    public override void OnActivate()
    {
        display.text = displayedText;
    }
}

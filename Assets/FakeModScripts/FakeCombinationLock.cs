using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeCombinationLock : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Cl"; } }
    public TextMesh[] ButtonTexts;
    public TextMesh LockText;
    public TextMesh ResetText;
    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0:
                ButtonTexts[0].text = ">";
                ButtonTexts[1].text = "<";
                AddFlicker(ButtonTexts[0], ButtonTexts[1]);
                LogQuirk("The left and right buttons are swapped.");
                break;
            case 1:
                LockText.text = Rnd.Range(20, 100).ToString();
                AddFlicker(LockText);
                LogQuirk("The number on the lock is greater than 19.");
                break;
            case 2:
                ResetText.text = "SUBMIT";
                AddFlicker(ResetText);
                LogQuirk("The reset button says submit.");
                break;
        }
    }
}

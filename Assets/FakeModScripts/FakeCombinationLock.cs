using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeCombinationLock : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] ButtonTexts;
    [SerializeField]
    private TextMesh LockText;
    [SerializeField]
    private TextMesh ResetText;
    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0:
                ButtonTexts[0].text = ">";
                ButtonTexts[1].text = "<";
                flickerObjs.Add(ButtonTexts[0].gameObject);
                flickerObjs.Add(ButtonTexts[1].gameObject);
                Log("The left and right buttons are swapped.");
                break;
            case 1:
                LockText.text = Rnd.Range(20, 100).ToString();
                flickerObjs.Add(LockText.gameObject);
                Log("The number on the lock is greater than 19.");
                break;
            case 2:
                ResetText.text = "SUBMIT";
                flickerObjs.Add(ResetText.gameObject);
                Log("The reset button says submit.");
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class TemplateFakeScript : ImposterMod 
{
    [SerializeField]
    private GameObject[] objects; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.
    public override SLPositions SLPos  //Can be ignored if SL Position is TR
    { get { return SLPositions.TR; } } 
    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 2); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                flickerObjs.Add(null); //Replace null with whatever you're modifying
                break;
            case 1:
                flickerObjs.Add(null);
                break;
        }
        Log(string.Format("Test message 2+2={0}", 2+2));
    }
}

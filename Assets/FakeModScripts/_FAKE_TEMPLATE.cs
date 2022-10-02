using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class TemplateFakeScript : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Template"; } }
    public override SLPositions SLPos  //Can be ignored if SL Position is TR
    { get { return SLPositions.TR; } } 
    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 3); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                flickerObjs.Add(null); //Replace null with whatever you're modifying
                break;
            case 1:
                flickerObjs.Add(null);
                break;
            case 2:
                flickerObjs.Add(null);
                break;
        }
        LogQuirk("Test message 2+2={0}", 2+2);
    }
    public override void OnActivate() { }
    protected override void OnColorblindToggle(bool cb)
    {
        base.OnColorblindToggle(cb);
    }
}

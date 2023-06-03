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
                AddFlicker(new GameObject()); //Replace new GameObject() with whatever you're modifying
                break;
            case 1:
                AddFlicker(new GameObject());
                break;
            case 2:
                AddFlicker(new GameObject());
                break;
        }
        LogQuirk("Test message 2+2={0}", 2+2);
    }
    public override void OnActivate() { }
    protected override void OnColorblindToggle() { }
}

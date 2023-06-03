using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

//by Blan
public class FakeEmojiMath : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Emjm"; } }
    
    public TextMesh displaytext;
    public TextMesh minustext;
    public Color[] colors;
    private static readonly string[] emoticons = {":)", "=(", "(:", ")=", ":(", "):", "=)", "(=", ":|", "|:"};

    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 4);

        AddFlicker(Case == 3 ? minustext : displaytext);
        switch (Case)
        {
            case 0:
                displaytext.text = (Rnd.Range(0,100) + (Ut.RandBool() ? "+" : "-") + Rnd.Range(0,100));
                LogQuirk("the display is an actual math equation");
                break;
            case 1:
                displaytext.text = ((Rnd.Range(0,10) == 0 ? "" : emoticons.PickRandom()) + emoticons.PickRandom() + (Ut.RandBool() ? "+" : "-") + (Rnd.Range(0,10) == 0 ? "" : emoticons.PickRandom()) + emoticons.PickRandom());
                displaytext.color = colors.PickRandom();
                LogQuirk("the text is not red");
                break;
            case 2:
                displaytext.text = ((Ut.RandBool() ? "+" : "-") + (Rnd.Range(0,10) == 0 ? "" : emoticons.PickRandom()) + emoticons.PickRandom());
                LogQuirk("there are no symbols before the operator");
                break;
            case 3:
                displaytext.text = ((Rnd.Range(0,10) == 0 ? "" : emoticons.PickRandom()) + emoticons.PickRandom() + (Ut.RandBool() ? "+" : "-") + (Rnd.Range(0,10) == 0 ? "" : emoticons.PickRandom()) + emoticons.PickRandom());
                minustext.text = "+";
                LogQuirk("the middle right button is a plus");
                break;
        }
    }
}

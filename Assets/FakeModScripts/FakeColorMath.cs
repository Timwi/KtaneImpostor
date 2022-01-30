using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeColorMath : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Com"; } }
    public MeshRenderer[] leds;
    public TextMesh text;
    private static readonly Color32[] colors = new[]
    {
        new Color32(0x00, 0x00, 0xFF,  0xFF),
        new Color32(0x00, 0xFF, 0x00,  0xFF),
        new Color32(0x80, 0x00, 0x80,  0xFF),
        new Color32(0xFF, 0xFF, 0x00,  0xFF),
        new Color32(0xFF, 0xFF, 0xFF,  0xFF),
        new Color32(0xFF, 0x00, 0xFF,  0xFF),
        new Color32(0xFF, 0x00, 0x00,  0xFF),
        new Color32(0xFF, 0x79, 0x00,  0xFF),
        new Color32(0x83, 0x83, 0x83,  0xFF),
        new Color32(0x00, 0x00, 0x00,  0xFF)
    };
    void Start()
    {
        for (int i = 0; i < 8; i++)
            leds[i].material.color = colors.PickRandom();
        flickerObjs.Add(text.gameObject);
        if (Ut.RandBool())
        {
            if (Ut.RandBool())
            {
                text.color = Color.blue;
                LogQuirk("The letter in the center of the module is blue");
            }
            else
            {
                text.color = Color.yellow;
                LogQuirk("The letter in the center of the module is yellow");
            }
            text.text = "ASMD".PickRandom().ToString();
        }
        else
        {
            text.color = Ut.RandBool() ? Color.red : Color.green;
            text.text = "BCEFGHIJKLNOPQRTUVWXYZ".PickRandom().ToString();
            LogQuirk("The letter in the center of the module is {0}", text.text);
        }
    }
}

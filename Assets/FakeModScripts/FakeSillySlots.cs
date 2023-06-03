using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeSillySlots : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Sys"; } }
    public GameObject[] wheels;
    public Texture[] wheelTextures;
    public TextMesh display;
    private static readonly string[] positions = { "left", "middle", "right" };
    private static readonly string[] realKeywords = { "Sassy", "Silly", "Soggy", "Sally", "Simon", "Sausage", "Steven" };
    private static readonly string[] fakeKeywords = { "Stumbles", "Stupid", "Spongy", "Stinky", "Spoiled", "Seven", "Swedish", "Skewed", "Sussy", "Small", "Slotted", "Samuel", "Sexual", "Samfun", "Sellout", "Strange", "Salmon", "Screaming", "Sticky", "Sloppy", "Sleepy",  };
    private static readonly float[] wheelAngles = { 7, 39, 66, 101, 130, 160, 190, 220, 250, 280, 310, 340 };
    void Start()
    {
        transform.Find("LED1").GetComponent<MeshRenderer>().material.SetFloat("Blend", 1);
        for (int i = 0; i < 3; i++)
            wheels[i].transform.localRotation = Quaternion.AngleAxis(wheelAngles.PickRandom(), Vector3.right);
        if (Ut.RandBool())
        {
            int pos = Rnd.Range(0, 3);
            AddFlicker(wheels[pos]);
            wheels[pos].transform.localRotation = Quaternion.AngleAxis(190, Vector3.right);
            wheels[pos].GetComponent<MeshRenderer>().material.mainTexture = wheelTextures.PickRandom();
            LogQuirk("there is a foreign symbol on the {0} slot", positions[pos]);
            display.text = realKeywords.PickRandom();
        }
        else
        {
            AddFlicker(display);
            display.text = fakeKeywords.PickRandom();
            LogQuirk("the displayed keyword is {0}", display.text);
        }
    }
}

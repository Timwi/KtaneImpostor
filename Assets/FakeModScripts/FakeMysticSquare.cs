using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeMysticSquare : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Mysq"; } }
    public TextMesh[] texts;
    public Transform[] btnObjects;
    public Transform skull;
    private int[] order = Enumerable.Range(1, 8).ToArray();
    int movedPos, skullPos;
    private int Case;

    void Start()
    {
        order.Shuffle();
        movedPos = Rnd.Range(0, 9);
        if (movedPos != 8)
            btnObjects[movedPos].localPosition = new Vector3(0.086f, 0, -0.085f);
        buttons[movedPos].gameObject.SetActive(false);  
        do skullPos = Rnd.Range(0, 9);
        while (skullPos == movedPos);
        if (Ut.RandBool())
        {
            int brokePos = Rnd.Range(0, 8);
            int result = Ut.RandBool() ? 0 : 9;
            LogQuirk("there is a {0}", result);
            AddFlicker(texts[brokePos]);
            order[brokePos] = result;
        }
        else
        {
            AddFlicker(skull);
            skullPos = movedPos;
            LogQuirk("the skull is already revealed");
        }

        for (int i = 0; i < 8; i++)
            texts[i].text = order[i].ToString();
        skull.localPosition = new Vector3(.063f - (skullPos % 3) * .045f, 0.0005f, -.028f + .0425f * (skullPos / 3));
    }
}

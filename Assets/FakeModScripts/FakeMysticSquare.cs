using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeMysticSquare : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] texts;
    [SerializeField]
    private Transform[] btnObjects;
    [SerializeField]
    private Transform skull;
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
        Case = Rnd.Range(0, 2);
        switch (Case)
        {
            case 0:
                int brokePos = Rnd.Range(0, 8);
                int result = Rnd.Range(0, 2) == 0 ? 0 : 9;
                Log("there is a " + result);
                flickerObjs.Add(texts[brokePos].gameObject);
                order[brokePos] = result;
                break;
            case 1:
                flickerObjs.Add(skull.gameObject);
                skullPos = movedPos;
                Log("the skull is already revealed");
                break;
        }
        for (int i = 0; i < 8; i++)
            texts[i].text = order[i].ToString();
        skull.localPosition = new Vector3(.063f - (skullPos % 3) * .045f, 0.0005f, -.028f + .0425f * (skullPos / 3));
    }
}

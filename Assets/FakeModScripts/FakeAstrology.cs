using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeAstrology : ImpostorMod 
{
    [SerializeField]
    private MeshRenderer[] displays; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.
    [SerializeField]
    private Texture[] elements, planets, zodiacs;
    [SerializeField]
    private TextMesh[] buttonLabels;
    private static readonly string[] ordinals = { "1st", "2nd", "3rd" };
    private int Case;
    int change;
    void Start()
    {
        displays[0].material.mainTexture = elements.PickRandom();
        displays[1].material.mainTexture = planets.PickRandom();
        displays[2].material.mainTexture = zodiacs.PickRandom();
        Case = Rnd.Range(0, 5); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                change = Rnd.Range(1, 3);
                displays[change].material.mainTexture = elements.PickRandom();
                flickerObjs.Add(displays[change].gameObject);
                Log("...there is a duplicate element, that doesn't seem normal.");
                break;
            case 1:
                change = Rnd.Range(0, 1) * 2;
                displays[change].material.mainTexture = planets.PickRandom();
                flickerObjs.Add(displays[change].gameObject);
                Log("...there is a duplicate planet, that doesn't seem normal.");
                break;
            case 2:
                change = Rnd.Range(0, 1);
                displays[change].material.mainTexture = zodiacs.PickRandom();
                flickerObjs.Add(displays[change].gameObject);
                Log("...there is a duplicate zodiac, that doesn't seem normal.");
                break;
            case 3:
                buttonLabels[0].text = "good\nomen";
                flickerObjs.Add(buttonLabels[0].gameObject);
                Log("...the poor omen button says \"good omen\", that doesn't seem normal.");
                break;
            case 4:
                buttonLabels[1].text = "poor\nomen";
                flickerObjs.Add(buttonLabels[1].gameObject);
                Log("...the good omen button says \"poor omen\", that doesn't seem normal.");
                break;
        }
    }
}

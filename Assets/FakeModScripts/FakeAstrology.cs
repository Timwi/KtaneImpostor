using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeAstrology : ImpostorMod 
{
    [SerializeField]
    private MeshRenderer[] displays; //SerializeField causes the variable to show up in the inspector,
                                     //keeping it a private variable.
    [SerializeField]
    private Texture[] elements, planets, zodiacs;
    [SerializeField]
    private TextMesh[] buttonLabels;
    int change;
    void Start()
    {
        displays[0].material.mainTexture = elements.PickRandom();
        displays[1].material.mainTexture = planets.PickRandom();
        displays[2].material.mainTexture = zodiacs.PickRandom();
        if (Ut.RandBool())
        {
            switch (Rnd.Range(0, 3))
            {
                case 0:
                    change = Rnd.Range(1, 3);
                    displays[change].material.mainTexture = elements.PickRandom();
                    flickerObjs.Add(displays[change].gameObject);
                    LogQuirk("there is a duplicate element");
                    break;
                case 1:
                    change = Rnd.Range(0, 1) * 2;
                    displays[change].material.mainTexture = planets.PickRandom();
                    flickerObjs.Add(displays[change].gameObject);
                    LogQuirk("there is a duplicate planet");
                    break;
                case 2:
                    change = Rnd.Range(0, 1);
                    displays[change].material.mainTexture = zodiacs.PickRandom();
                    flickerObjs.Add(displays[change].gameObject);
                    LogQuirk("there is a duplicate zodiac");
                    break;
            }
        }
        else
        {
            if (Ut.RandBool())
            {
                buttonLabels[0].text = "good\nomen";
                flickerObjs.Add(buttonLabels[0].gameObject);
                LogQuirk("the poor omen button says \"good omen\"");
            }
            else
            {
                buttonLabels[2].text = "poor\nomen";
                flickerObjs.Add(buttonLabels[2].gameObject);
                LogQuirk("the good omen button says \"poor omen\"");
            }
        }
    }
}

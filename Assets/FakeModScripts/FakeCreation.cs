using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeCreation : ImpostorMod 
{
    [SerializeField]
    private MeshRenderer[] displays; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.
    [SerializeField]
    private MeshRenderer weatherDisplay;
    [SerializeField]
    private Texture[] elements, funnyElements, weathers, funnyWeathers;

    private int Case;
    private int[] elementOrder;
    void Start()
    {
        elementOrder = Enumerable.Range(0, 4).ToArray().Shuffle();
        for (int i = 0; i < 4; i++)
            displays[i].material.mainTexture = elements[elementOrder[i]];
        weatherDisplay.material.mainTexture = weathers.PickRandom();

        if (Ut.RandBool())
        {
                int changePos = Rnd.Range(0, 4);
                displays[changePos].material.mainTexture = funnyElements[elementOrder[changePos]];
                LogQuirk("element {0} is replaced with {1}", elements[elementOrder[changePos]].name, funnyElements[elementOrder[changePos]].name);
                flickerObjs.Add(displays[changePos].gameObject); 
        }
        else
        {
                int newWeather = Rnd.Range(0, funnyWeathers.Length);
                weatherDisplay.material.mainTexture = funnyWeathers[newWeather];
                LogQuirk("the weather is {0}", funnyWeathers[newWeather].name);
                flickerObjs.Add(weatherDisplay.gameObject);
        }
    }
}

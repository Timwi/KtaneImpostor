using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeGridlock : ImpostorMod 
{
    [SerializeField] TextMesh topNum, bottomNum, nextBtn;
    [SerializeField] MeshRenderer[] symbolsRenderers, backings;
    [SerializeField] Material[] colors;
    [SerializeField] Texture[] symbols, arrows;

    private int Case;

    int pageCount, starPos;

    void Start()
    {
        SetUpGrid();
        Case = Rnd.Range(0, 3); //However many cases you want there to be.
        switch (Case)
        {
            case 0: //PREV button
                flickerObjs.Add(null); //Replace null with whatever you're modifying
                break;
            case 1: //10 of 5
                flickerObjs.Add(null);
                break;
            case 2://no star
                flickerObjs.Add(null);
                break;
        }
    }
    void SetUpGrid()
    {
        pageCount = Rnd.Range(5, 11);
        bottomNum.text = pageCount.ToString();

        for (int i = 0; i < 16; i++)
        {
            int disp = Rnd.Range(0, 3);
            if (disp == 0) //Arrow
            {
                symbolsRenderers[i].material.mainTexture = arrows[Rnd.Range(0, 8)];
                symbolsRenderers[i].material.color = Color.white;
            }
            else if (disp == 1) //Star
            {
                symbolsRenderers[i].material.mainTexture = symbols[Rnd.Range(0, 3)];
                symbolsRenderers[i].material.color = Color.white;
                backings[i].material = colors[Rnd.Range(0,4)];
            }
            else
                symbolsRenderers[i].material = colors[4];
        }
        starPos = Rnd.Range(0, 16);
        symbolsRenderers[starPos].material.mainTexture = symbols[3];
        backings[starPos].material = colors.PickRandom();

    }
}

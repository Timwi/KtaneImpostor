using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeGridlock : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Gl"; } }
    public TextMesh topNum, bottomNum, nextBtn;
    public MeshRenderer[] symbolsRenderers, backings;
    public Material[] colors;
    public Texture[] symbols, arrows;

    private int Case;

    int pageCount, starPos;

    void Start()
    {
        SetUpGrid();
        Case = Rnd.Range(0, 3); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                nextBtn.text = "PREV";
                flickerObjs.Add(nextBtn.gameObject);
                LogQuirk("the next button says previous");
                break;
            case 1: //10 of 5
                topNum.text = (pageCount + Rnd.Range(1, 5)).ToString();
                flickerObjs.Add(topNum.gameObject);
                LogQuirk("the current page is higher than the page count");
                break;
            case 2://no star
                RandomizeCell(starPos);
                flickerObjs.AddRange(symbolsRenderers.Select(x => x.gameObject));
                LogQuirk("no star is present");
                break;
        }
    }
    void SetUpGrid()
    {
        pageCount = Rnd.Range(5, 11);
        bottomNum.text = pageCount.ToString();

        for (int i = 0; i < 16; i++)
            RandomizeCell(i);
        starPos = Rnd.Range(0, 16);
        symbolsRenderers[starPos].enabled = true;
        symbolsRenderers[starPos].material.mainTexture = symbols[3];
        backings[starPos].material = colors[Rnd.Range(0,4)];
    }
    void RandomizeCell(int ix)
    {
        int disp = Rnd.Range(0, 3);
        symbolsRenderers[ix].enabled = true;
        if (disp == 0) //Arrow
        {
            symbolsRenderers[ix].material.mainTexture = arrows[Rnd.Range(0, 8)];
            backings[ix].material = colors[4];
        }
        else if (disp == 1) //Star
        {
            symbolsRenderers[ix].material.mainTexture = symbols[Rnd.Range(0, 3)];
            backings[ix].material = colors[Rnd.Range(0, 4)];
        }
        else
        {
            symbolsRenderers[ix].enabled = false;
            backings[ix].material = colors[4];
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

// Made by Kilo Bites

public class FakePerspectivePegs : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Pp"; } }
    public TextMesh[] peg0CB, peg1CB, peg2CB, peg3CB, peg4CB;
    public MeshRenderer[] peg0Mat, peg1Mat, peg2Mat, peg3Mat, peg4Mat;
    public Material[] colors;
    public GameObject[] bases, pegs;
    private int Case;
    private bool[] peg = new bool[5];
    private readonly Coroutine[] raisingPegs = new Coroutine[5];
    private char[] colorNames = "RYGBP".ToCharArray();

    private int[][] colorIx = new int[5][];

    void Start()
    {
        Case = Rnd.Range(0, 2);
        switch (Case)
        {
            case 0:
                var ix = Rnd.Range(0, 5);
                for (int i = 0; i < 5; i++)
                {
                    peg[i] = true;
                    colorIx[i] = new int[5];
                    for (int j = 0; j < 5; j++)
                        colorIx[i][j] = ix;
                }
                AddFlicker(bases);
                LogQuirk("all of the pegs share the same color");
                break;
            case 1:
                var pegsToRaise = new List<int>();

                var counted = Rnd.Range(1, 4);

                for (int i = 0; i < counted; i++)
                {
                    var idx = Enumerable.Range(0, 5).Where(x => !pegsToRaise.Contains(x)).PickRandom();
                    pegsToRaise.Add(idx);
                }

                for (int i = 0; i < 5; i++)
                {
                    peg[i] = pegsToRaise.Contains(i);
                    colorIx[i] = new int[5];
                    for (int j = 0; j < 5; j++)
                        colorIx[i][j] = Rnd.Range(0, 5);

                    if (!peg[i])
                        AddFlicker(bases[i]);

                }
                LogQuirk("some of the pegs are pushed in");
                break;
        }
    }

    void ApplyPegs()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                switch (i)
                {
                    case 0:
                        peg0Mat[j].material = colors[colorIx[i][j]];
                        peg0CB[j].text = colorNames[colorIx[i][j]].ToString();
                        break;
                    case 1:
                        peg1Mat[j].material = colors[colorIx[i][j]];
                        peg1CB[j].text = colorNames[colorIx[i][j]].ToString();
                        break;
                    case 2:
                        peg2Mat[j].material = colors[colorIx[i][j]];
                        peg2CB[j].text = colorNames[colorIx[i][j]].ToString();
                        break;
                    case 3:
                        peg3Mat[j].material = colors[colorIx[i][j]];
                        peg3CB[j].text = colorNames[colorIx[i][j]].ToString();
                        break;
                    case 4:
                        peg4Mat[j].material = colors[colorIx[i][j]];
                        peg4CB[j].text = colorNames[colorIx[i][j]].ToString();
                        break;
                }
            }
        }
    }
    public override void OnActivate()
    {
        ApplyPegs();

        for (int i = 0; i < 5; i++)
            if (peg[i])
                raisingPegs[i] = StartCoroutine(MovePegs(i));
    }

    IEnumerator MovePegs(int pos)
    {
        var duration = 0.75f;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            pegs[pos].transform.localPosition = new Vector3(pegs[pos].transform.localPosition.x, pegs[pos].transform.localPosition.y, Mathf.Lerp(-1.5f, 0f, elapsed / duration));
            yield return null;
            elapsed += Time.deltaTime;
        }

        pegs[pos].transform.localPosition = new Vector3(pegs[pos].transform.localPosition.x, pegs[pos].transform.localPosition.y, 0f);
    }
    protected override void OnColorblindToggle()
    {
        for (int i = 0; i < 5; i++)
        {
            peg0CB[i].gameObject.SetActive(true);
            peg1CB[i].gameObject.SetActive(true);
            peg2CB[i].gameObject.SetActive(true);
            peg3CB[i].gameObject.SetActive(true);
            peg4CB[i].gameObject.SetActive(true);
        }
    }
}

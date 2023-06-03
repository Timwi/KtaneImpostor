using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeNumberPad : ImpostorMod
{
    public override string ModAbbreviation { get { return "Nump"; } }
    public GameObject[] NumButtonObjs;
    public GameObject[] SubObjs;
    public TextMesh[] NumButtonText;
    public TextMesh[] SubTexts;
    public Material[] SubMats;
    private int Case;
    private static readonly Color[] BtnColors =
    {
        new Color(1, 1, 1),
        new Color(1, 0.3f, 0.3f),
        new Color(0.3f, 1, 0.3f),
        new Color(0.3f, 0.3f, 1),
        new Color(1, 1, 0.3f)
    };
    private static readonly Color[] FakeColors =
    {
        new Color(1, 0.5f, 0.3f),
        new Color(1, 0.3f, 1),
        new Color(0.5f, 0.3f, 1),
        new Color(0.3f, 0.3f, 0.3f),
        new Color(0.3f, 1, 1),
    };
    private static readonly string[] FakeColorNames = new string[] { "orange", "magenta", "purple", "gray", "cyan" };

    void Start()
    {
        foreach (var btn in NumButtonObjs)
            btn.GetComponent<MeshRenderer>().material.color = BtnColors[Rnd.Range(0, BtnColors.Length)];

        Case = Rnd.Range(0, 4);
        var rndBtn = Rnd.Range(0, 10);
        switch (Case)
        {
            case 0:
                for (int i = 1; i < NumButtonText.Length; i++)
                    NumButtonText[i].text = (10 - i).ToString();
                AddFlicker(NumButtonText);
                LogQuirk("the button labels are in reverse order", 2 + 2);
                break;
            case 1:
                SubTexts[0].text = "YES";
                SubTexts[1].text = "NO";
                AddFlicker(SubTexts);
                LogQuirk("the submit and clear buttons say YES and NO");
                break;
            case 2:
                var rndColor = Rnd.Range(0, FakeColors.Length);
                NumButtonObjs[rndBtn].GetComponent<MeshRenderer>().material.color = FakeColors[rndColor];
                AddFlicker(NumButtonObjs[rndBtn]);
                LogQuirk("button {0} is colored {1}", rndBtn, FakeColorNames[rndColor]);
                break;
            case 3:
                var rndLet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)].ToString();
                NumButtonText[rndBtn].text = rndLet;
                AddFlicker(NumButtonText[rndBtn]);
                LogQuirk("button {0} is labelled {1}", rndBtn, rndLet);
                break;
        }
    }
}

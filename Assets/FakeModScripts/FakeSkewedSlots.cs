using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

// Made by Kilo Bites

public class FakeSkewedSlots : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Skewed Slots"; } }
    public override SLPositions SLPos
    { get { return SLPositions.TR; } }
    public TextMesh[] numDisplays;
    public TextMesh submitText;
    public GameObject[] buttonObj;
    private int Case;

    private int[] numbers = new int[3];
    private bool inf;
    private bool[] randomExclamation = new bool[3];
    private readonly string[] scuffedSlotsNames = { "Ze", "Quinn Wuest", "River", "Zaakk", "Ash", "BlvdBroken", "meh", "Vinco", "Grunkle", "Blan" };

    void Start()
    {
        Case = Rnd.Range(0, 4);

        for (int i = 0; i < 3; i++)
        {
            numbers[i] = Rnd.Range(0, 10);
        }

        switch (Case)
        {
            case 0:
                submitText.text = Rnd.Range(0,2) == 0 ? "Bugged" : scuffedSlotsNames.PickRandom();
                submitText.fontSize = submitText.text.Length > 6 ? 50 : 90;
                AddFlicker(buttons[0].gameObject);
                LogQuirk("the submit button says {0}", submitText.text.ToUpperInvariant());
                break;
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    buttonObj[i].transform.localPosition = new Vector3(buttonObj[i].transform.localPosition.x, buttonObj[i].transform.localPosition.y, i % 2 == 0 ? 0.5f : -0.5f);
                }
                AddFlicker(buttonObj);
                LogQuirk("the up and down arrows are swapped");
                break;
            case 2:
                inf = true;
                AddFlicker(numDisplays);
                LogQuirk("the slots continue to scroll");
                break;
            case 3:
                var random = Rnd.Range(0, 3);
                randomExclamation[random] = true;
                AddFlicker(numDisplays[random].gameObject);
                LogQuirk("there is a ! on one of the slots");
                break;
        }
    }
    public override void OnActivate()
    {
        StartCoroutine(spinningText());
    }

    IEnumerator spinningText()
    {
        yield return null;

        loop:
        var spins = new[] { 40 + Rnd.Range(-5, 5), 60 + Rnd.Range(-5, 5), 80 + Rnd.Range(-5, 5) };

        while (spins[2] > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (spins[i] > 0)
                {
                    var num = (numbers[i] + 1) % 10;

                    numbers[i] = num;
                    spins[i]--;
                }

                if (spins[i] == 0 && randomExclamation[i])
                {
                    numbers[i] = 10;
                }
            }


            updateText();
            yield return new WaitForSeconds(0.03f);
            if (inf)
                goto loop;

        }
    }

    void updateText()
    {
        for (int i = 0; i < 3; i++)
        {
            numDisplays[i].text = numbers[i] < 10 ? numbers[i].ToString() : "!";
        }
    }
}

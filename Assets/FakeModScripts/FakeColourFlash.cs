using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeColourFlash : ImposterMod
{
    [SerializeField]
    private TextMesh yes, no, display; 
    private static readonly string[] fakeYes = { "YEE", "YAS", "YEP", "YEA", "YEH", "YAH" };
    private static readonly string[] fakeNo = { "NOPE", "NAH", "NAW", "NOT", "NIL", "NADA" };
    private static readonly string[] colorNames = { "RED", "YELLOW", "GREEN", "BLUE", "MAGENTA", "WHITE" };
    private static readonly Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta, Color.white };

    private int Case;
    private int[] displayedWords, displayedColors;

    void Start()
    {
        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0:
                flickerObjs.Add(yes.gameObject);
                yes.text = fakeYes.PickRandom();
                Log(string.Format("...the left button says {0}, that doesn't seem normal.", yes.text));
                break;
            case 1:
                flickerObjs.Add(no.gameObject);
                no.text = fakeNo.PickRandom();
                Log(string.Format("...the right button says {0}, that doesn't seem normal.", no.text));
                break;
            case 2:
                flickerObjs.Add(yes.gameObject);
                flickerObjs.Add(no.gameObject);
                yes.text = "NO";
                no.text = "YES";
                Log("...the yes and no buttons are swapped, that doesn't seem normal.");
                break;
        }
        displayedWords = new int[8].Select(x => Rnd.Range(0, 6)).ToArray();
        displayedColors = new int[8].Select(x => Rnd.Range(0, 6)).ToArray();
        StartCoroutine(Flash());
    }
    private IEnumerator Flash()
    {
        while (true)
        {
            for (int i = 0; i < 8; i++)
            {
                display.text = colorNames[displayedWords[i]];
                display.color = colors[displayedColors[i]];
                yield return new WaitForSeconds(0.75f);
            }
            display.text = "";
            yield return new WaitForSeconds(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

public class FakePianoKeys : ImpostorMod 
{
    [SerializeField]
    private TextMesh display; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.

    private int Case;

    void Start()
    {
        flickerObjs.Add(display.gameObject);
        string set = "nb#mTcCUB";
        List<string> symbols = new List<string> {};
        PKretry:
        for (int i = 0; i < 3; i++) {
            symbols.Add(set.PickRandom().ToString());
        }
        if ((symbols[0] == symbols[1]) || (symbols[0] == symbols[2]) || (symbols[1] == symbols[2])) {
            Log("the display has identical symbols");
        } else {
            symbols.Clear();
            goto PKretry;
        }
        display.text = symbols[0] + "  " + symbols[1] + "  " + symbols[2];
    }
}

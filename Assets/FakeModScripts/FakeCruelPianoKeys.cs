using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

//Coded by blananas2
public class FakeCruelPianoKeys : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Cpk"; } }
    public TextMesh display; 
    void Start()
    {
        flickerObjs.Add(display.gameObject);
        string set = "nb#mTcCUB";
        List<string> symbols = new List<string> {};
        PKretry:
        for (int i = 0; i < 4; i++) {
            symbols.Add(set.PickRandom().ToString());
        }
        if (symbols.HasDuplicates()) {
            LogQuirk("the display has identical symbols");
        } else {
            symbols.Clear();
            goto PKretry;
        }
        display.text = symbols.Join();
    }
}

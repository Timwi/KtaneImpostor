using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

//Coded by Blan
public class FakePianoKeys : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Pk"; } }

    public TextMesh display;
    public Material[] mats;
    public MeshRenderer[] objs;
    public GameObject whole;
    
    private int Case;

    void Start()
    {
        string set = "nb#mTcCUB";
        List<string> symbols = new List<string> {};

        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0: //Identical symbols
                flickerObjs.Add(display.gameObject);
                retry0:
                for (int i = 0; i < 3; i++) {
                    symbols.Add(set.PickRandom().ToString());
                }
                if (!symbols.HasDuplicates()) {
                    symbols.Clear();
                    goto retry0;LogQuirk("the display has identical symbols");
                }
                LogQuirk("the display has identical symbols");
            break;
            case 1: //Inverted colors
                for (int o = 0; o < 12; o++) {
                    flickerObjs.Add(objs[o].gameObject);
                    objs[o].material = mats[(o < 7 ? 0 : 1)];
                }
                retry1:
                for (int i = 0; i < 3; i++) {
                    symbols.Add(set.PickRandom().ToString());
                }
                if (symbols.HasDuplicates()) {
                    symbols.Clear();
                    goto retry1;
                }
                LogQuirk("the keys have inverted colors");
            break;
            case 2: //Vertically flipped or upside down
                for (int o = 0; o < 12; o++) {
                    flickerObjs.Add(objs[o].gameObject);
                }
                Vector3 origPos = whole.transform.localPosition;
                Vector3 origScale = whole.transform.localScale;
                whole.transform.localPosition = new Vector3(origPos.x, origPos.y, 0.043f);
                whole.transform.localScale = new Vector3((Ut.RandBool() ? 1 : -1) * origScale.x, origScale.y, -origScale.z);
                retry2:
                for (int i = 0; i < 3; i++) {
                    symbols.Add(set.PickRandom().ToString());
                }
                if (symbols.HasDuplicates()) {
                    symbols.Clear();
                    goto retry2;
                }
                LogQuirk("the keys are upside-down");
            break;
        }

        display.text = symbols.Join("    ");
    }
}

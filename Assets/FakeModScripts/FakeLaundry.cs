using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeLaundry : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Ln"; } }
    public GameObject coinSlot;
    public Transform[] dials, symbols;
    public TextMesh topDisp, bottomDisp;
    private int Case;

    string[] funnyIrons = { "TOASTER", "BUMPER CAR", "TWO SEATED\nBUMPER CAR", "DELUX\nBUMPER CAR", "BUMPER CAR\nWITH NO LEGS", "BATHTUB", "IRON BAR", "GOLD", "IRONY", "98 DEGREES", "800°F", "THE IRON MAN", "TONY STARK", "VITAMIN B-12", "IRON-59", "FIRE" };
    string[] funnySpecials = { "INCREASED MOISTURE", "ZERO", "APATHY", "PINKS", "FRENCH CLOTHES", "I GIVE UP", "LONG CYCLE", "SYMBOL CYCLE", "SMELFED CLIMPS", "NO TETRISSPRINT", "PLEASE BLEACH", "DRY WASHING", "NEGATIVE HEAT", "TOO COLD", "WASH CAREFULLY", "DON'T SPEAK", "ALL-CHLORINE\nBLEACH" };

    void Start()
    {
        Case = Rnd.Range(0, 3); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                coinSlot.SetActive(false);
                flickerObjs.Add(coinSlot);
                LogQuirk("the coin slot is gone");
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    Vector3 temp = symbols[i].localPosition;
                    symbols[i].localPosition = new Vector3(dials[i].localPosition.x, -2.262985f, dials[i].localPosition.z);
                    dials[i].localPosition = new Vector3(temp.x, -2.151655f, temp.z);
                }
                flickerObjs.AddRange(dials.Concat(symbols).Select(x => x.gameObject));
                LogQuirk("the dials and symbols are swapped");
                break;
            case 2:
                if (Ut.RandBool())
                {
                    string iron = funnyIrons.PickRandom();
                    if (iron.Contains("\n"))
                        topDisp.transform.localScale = new Vector3(.01f, .005f, .01f);
                    topDisp.text = iron;
                    LogQuirk("The ironing display says {0}", iron.Replace('\n', ' '));
                    flickerObjs.Add(topDisp.gameObject);
                }
                else
                {
                    string special = funnySpecials.PickRandom();
                    if (special.Contains("\n"))
                        bottomDisp.transform.localScale = new Vector3(.01f, .005f, .01f);
                    bottomDisp.text = special;
                    LogQuirk("The ironing display says {0}", special.Replace('\n', ' '));
                    flickerObjs.Add(bottomDisp.gameObject);
                }
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeTwoBits : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] letters;
    [SerializeField]
    private TextMesh display, query, submit;

    private string[] table = { "KB", "DK", "GV", "TK", "PV", "KP", "BV", "VT", "PZ", "DT", "EE", "ZK", "KE", "CK", "ZP", "PP", "TP", "TG", "PD", "PT", "TZ", "EB", "EC", "CC", "CZ", "ZV", "CV", "GC", "BT", "GT", "BZ", "PK", "KZ", "KG", "VD", "CE", "VB", "KD", "GG", "DG", "PB", "VV", "GE", "KV", "DZ", "PE", "DB", "CD", "TD", "CB", "GB", "TV", "KK", "BG", "BP", "VP", "EP", "TT", "ED", "ZG", "DE", "DD", "EV", "TE", "ZD", "BB", "PC", "BD", "KC", "ZB", "EG", "BC", "TC", "ZE", "ZC", "GP", "ET", "VC", "TB", "VZ", "EZ", "EK", "DV", "CG", "VE", "DP", "BK", "PG", "GK", "GZ", "KT", "CT", "ZZ", "VG", "GD", "CP", "BE", "ZT", "VK", "DC" };
    private string forbiddenLetters = "AFHIJLMNOQRSUWXY";
    private string displayedLetters = "BCDEGKPTVZ";

    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 3); //However many cases you want there to be.
        switch (Case)
        {
            case 0:
                LogQuirk("the display is permanently displaying \"WORKING...\"");
                flickerObjs.Add(display.gameObject);
                display.text = "WORKING...";
                break;
            case 1:
                LogQuirk("the submit and query buttons are swapped");
                flickerObjs.Add(query.gameObject);
                flickerObjs.Add(submit.gameObject);
                query.text = "SUBMIT";
                submit.text = "QUERY";
                break;
            case 2:
                char init = GetInitQuery().First();
                int changedPos = displayedLetters.IndexOf(init);
                letters[changedPos].text = forbiddenLetters.PickRandom().ToString();
                flickerObjs.Add(letters[changedPos].gameObject);
                LogQuirk("letter {0} is changed to a {1}", displayedLetters[changedPos], letters[changedPos].text);
                break;
        }
    }
    private string GetInitQuery()
    {
        int val = BombInfo.GetSerialNumberLetters().First() - 'A' + 1;
        val += BombInfo.GetSerialNumberNumbers().Last() * BombInfo.GetBatteryCount();
        if (BombInfo.IsPortPresent(Port.StereoRCA) && !BombInfo.IsPortPresent(Port.RJ45))
            val *= 2;
        val %= 100;
        return table[val];
    }
}

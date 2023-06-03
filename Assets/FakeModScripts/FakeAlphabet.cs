using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeAlphabet : ImpostorMod
{
    public override string ModAbbreviation { get { return "Apb"; } }
    public TextMesh[] texts;
    public SpriteRenderer[] spriteRenderers;
    public Sprite[] keypadSymbols;

    private string decoyLetters = "38☺♣*Ñæøδπ$";
    static string[] positions = { "top-left", "top-right", "bottom-left", "bottom-right" };
    static readonly string[] wordList = {"JQXZ","QEW","AC","ZNY","TJL","OKBV","DFW","YKQ","LXE","GS","VSI","PQJS","VCN","JR","IRNM","OP","QYDX","HDU","PKD","ARGF"};
    
    
    void Start()
    {
        string word = wordList.PickRandom();
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Where(x => !word.Contains(x)).ToArray();
        alphabet.Shuffle();
        for (int i = 0; word.Length < 4; i++)
            word += alphabet[i];
        for (int i = 0; i < 4; i++)
            texts[i].text = word[i].ToString();

        int changed = Rnd.Range(0, 4);
        if (Ut.RandBool()) //Fake letter
        {
            texts[changed].text = decoyLetters.PickRandom().ToString();
            LogQuirk("the {0} key says {1}", positions[changed], texts[changed].text);
            AddFlicker(texts[changed]);
        }
        else //Keypad symbol
        {
            texts[changed].gameObject.SetActive(false);
            spriteRenderers[changed].sprite = keypadSymbols.PickRandom();
            LogQuirk("the {0} key has a keypad symbol on it", positions[changed]);
            AddFlicker(spriteRenderers[changed]);
        }
    }
}

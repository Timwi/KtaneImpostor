using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

//by Blan
public class FakeRoundKeypad : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Rkp"; } }

    public TextMesh[] texts;
    public GameObject[] keys;
    public GameObject keypad;
    public Font susfont;
    public Material susmat;

    private char[] symbols = { 'Ѽ', 'æ', '©', 'Ӭ', 'Ҩ', 'Ҋ', 'ϗ', 'Ϟ', 'Ԇ', 'Ϙ', 'Ѯ', 'ƛ', 'Ω', '¶', 'ψ', '¿', 'Ϭ', 'Ͼ', 'Ͽ', '★', '☆', 'ټ', '҂', 'Ѣ', 'Ѭ', 'Ѧ', 'Җ' };
    private char[] sussymbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'T', 'X', 'Z' };

    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 3);

        switch (Case)
        {
            case 0:
                symbols = symbols.Shuffle();
                Case = Rnd.Range(0, 8);
                for (int k = 0; k < 8; k++) {
                    if (Case == k) {
                        texts[k].text = " ";
                        AddFlicker(keys[k]);
                    } else {
                        texts[k].text = symbols[k].ToString();
                    }
                }
                LogQuirk("a symbol is missing");
                break;
            case 1:
                AddFlicker(keypad);
                symbols = symbols.Shuffle();
                keypad.transform.localRotation = Quaternion.Euler(0f, 22.5f, 0f);
                for (int k = 0; k < 8; k++) {
                    texts[k].text = symbols[k].ToString();
                    texts[k].transform.rotation = Quaternion.Euler(90f, -90f, -90f);
                }
                LogQuirk("the keypad is rotated by 22.5°");
                break;
            case 2:
                sussymbols = sussymbols.Shuffle();
                for (int k = 0; k < 8; k++) {
                    texts[k].font = susfont;
                    texts[k].GetComponent<MeshRenderer>().material = susmat;
                    texts[k].fontSize = 150;
                    texts[k].text = sussymbols[k].ToString();
                }
                AddFlicker(texts);
                LogQuirk("another symbol set is used");
                break;
        }
    }
}

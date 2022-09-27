﻿using System.Collections;
using System.Linq;
using UnityEngine;

using Rnd = UnityEngine.Random;

public class FakeSimonSends : ImpostorMod
{
    public override string ModAbbreviation { get { return "Ssnd"; } }

    public MeshRenderer Diode;
    public TextMesh ColorblindDiodeText;
    public Light[] Lights;
    public MeshRenderer[] SendsButtons;
    public GameObject[] SendsButtonTexts;

    private static readonly string[] _morse = ".-|-...|-.-.|-..|.|..-.|--.|....|..|.---|-.-|.-..|--|-.|---|.--.|--.-|.-.|...|-|..-|...-|.--|-..-|-.--|--..".Split('|');
    private static readonly string[] _colorblindTextNames = { "BLACK", "BLUE", "GREEN", "CYAN", "RED", "MAGENTA", "YELLOW", "WHITE" };
    private string _morseR, _morseG, _morseB;
    private int _morseRPos, _morseGPos, _morseBPos;

    void Start()
    {
        var available = Enumerable.Range(0, 26).ToList().Shuffle();
        var r = (char) (available[0] + 'A');
        var g = (char) (available[1] + 'A');
        var b = (char) (available[2] + 'A');

        _morseR = getMorse(r) + "___";
        _morseRPos = Rnd.Range(0, _morseR.Length);
        _morseG = getMorse(g) + "___";
        _morseGPos = Rnd.Range(0, _morseG.Length);
        _morseB = getMorse(b) + "___";
        _morseBPos = Rnd.Range(0, _morseB.Length);

        switch (Rnd.Range(0, 2))
        {
            case 0: // colors of the buttons are out of order
                var materials = SendsButtons.Select(btn => btn.sharedMaterial).ToList().Shuffle();
                // Throw the player a bone by making sure that *every* color is in the wrong place, not just some of them
                while (Enumerable.Range(0, materials.Count).Any(ix => materials[ix] == SendsButtons[ix].sharedMaterial))
                    materials.Shuffle();
                for (var i = 0; i < materials.Count; i++)
                    SendsButtonTexts[i].transform.SetParent(SendsButtons[materials.IndexOf(SendsButtons[i].sharedMaterial)].transform, false);
                for (var i = 0; i < materials.Count; i++)
                    SendsButtons[i].sharedMaterial = materials[i];
                LogQuirk("the colors of the buttons are not in the order KBGCRMYW");
                flickerObjs.AddRange(SendsButtons.Select(btn => btn.gameObject));
                break;

            case 1: // one of the color channels is missing
                switch (Rnd.Range(0, 3))
                {
                    case 0: _morseR = null; LogQuirk("the red color channel was missing from the diode"); break;
                    case 1: _morseG = null; LogQuirk("the green color channel was missing from the diode"); break;
                    case 2: _morseB = null; LogQuirk("the blue color channel was missing from the diode"); break;
                }
                flickerObjs.Add(Diode.gameObject);
                break;
        }

        for (var i = 0; i < SendsButtonTexts.Length; i++)
            SendsButtonTexts[i].gameObject.SetActive(ColorblindMode);
        ColorblindDiodeText.gameObject.SetActive(ColorblindMode);

        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        const float dark = .1f;
        const float bright = .65f;

        // _answerSoFar is set to null when the module is solved.
        while (!willSolve)
        {
            bool red = _morseR != null && _morseR[_morseRPos] == '#';
            bool green = _morseG != null && _morseG[_morseGPos] == '#';
            bool blue = _morseB != null && _morseB[_morseBPos] == '#';
            var color = new Color(red ? bright : dark, green ? bright : dark, blue ? bright : dark);
            Diode.material.color = color;
            ColorblindDiodeText.gameObject.SetActive(ColorblindMode);
            ColorblindDiodeText.text = _colorblindTextNames[(red ? 4 : 0) + (green ? 2 : 0) + (blue ? 1 : 0)];
            foreach (var text in SendsButtonTexts)
                text.gameObject.SetActive(ColorblindMode);
            foreach (var light in Lights)
                light.color = new Color(red ? 1 : 0, green ? 1 : 0, blue ? 1 : 0);
            yield return new WaitForSeconds(1);
            _morseRPos = (_morseRPos + 1) % (_morseR == null ? 1 : _morseR.Length);
            _morseGPos = (_morseGPos + 1) % (_morseG == null ? 1 : _morseG.Length);
            _morseBPos = (_morseBPos + 1) % (_morseB == null ? 1 : _morseB.Length);
        }
        Diode.material.color = new Color(dark, dark, dark);
        foreach (var light in Lights)
            light.gameObject.SetActive(false);
        ColorblindDiodeText.gameObject.SetActive(false);
    }

    private static string getMorse(char letter)
    {
        return _morse[letter - 'A'].Select(ch => ch == '.' ? "#" : "###").Join("_");
    }
}
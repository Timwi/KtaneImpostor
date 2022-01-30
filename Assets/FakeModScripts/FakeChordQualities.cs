using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeChordQualities : ImpostorMod
{
    public Transform _wheelButton;
    public Material _lightMat;
    public Renderer[] _buttonRenderers;
    public TextMesh[] _textMeshes, _noteTextMeshes;
    public override string ModAbbreviation { get { return "Cq"; } }

    private void Start()
    {
        int position = Rnd.Range(0, 12);
        _wheelButton.Rotate(new Vector3(0, 1, 0), (position * -360.0f / 12.0f));

        _noteTextMeshes[position].GetComponentInParent<SpriteRenderer>().color = new Color(0.99371195f, 1f, 0.5441177f);

        Chord givenChord = new Chord(Quality.getQualities()[Rnd.Range(0, Quality.getQualities().Length)], Rnd.Range(0, 12));
        foreach(TextMesh t in _textMeshes)
            t.text = string.Empty;

        int[] offsets = new int[4];

        offsets[0] = givenChord.note;
        _textMeshes[givenChord.note].text = "▲";
        for(int i = 0; i < givenChord.quality.offsets.Length; i++)
        {
            offsets[i + 1] = (givenChord.quality.offsets[i] + givenChord.note) % 12;
            _textMeshes[offsets[i + 1]].text = "▲";
        }

        int moduleCase = Rnd.Range(0, 8);
        switch(moduleCase)
        {
            case 0: // 3 notes
                int missing = Rnd.Range(0, offsets.Length);
                _textMeshes[offsets[missing]].text = string.Empty;
                flickerObjs.AddRange(_textMeshes.Select(m => m.gameObject));
                LogQuirk("there are only three notes");
                break;
            case 1: // 5 notes
                _textMeshes.PickRandom(t => t.text == string.Empty).text = "▲";
                flickerObjs.AddRange(_textMeshes.Select(m => m.gameObject));
                LogQuirk("there are five notes");
                break;
            case 2: // Input/output swapped
                foreach(TextMesh t in _textMeshes.Where(t => t.text != string.Empty))
                {
                    t.text = string.Empty;
                    Transform parent = t.transform.parent.parent.GetChild(2).GetChild(0);
                    parent.GetChild(0).GetComponent<Renderer>().material = _lightMat;
                    parent.GetChild(1).GetComponent<Renderer>().enabled = true;
                    flickerObjs.Add(parent.gameObject);
                }
                LogQuirk("the lights are on instead of there being arrows");
                break;
            case 3: // Duplicate buttons
                if(Ut.RandBool())
                {
                    _buttonRenderers[0].enabled = false;
                    _buttonRenderers[1].enabled = true;
                    flickerObjs.Add(_buttonRenderers[1].gameObject);
                }
                else
                {
                    _buttonRenderers[2].enabled = false;
                    _buttonRenderers[3].enabled = true;
                    flickerObjs.Add(_buttonRenderers[3].gameObject);
                }
                LogQuirk("the buttons are the same");
                break;
            case 4: // Number labels
                TextMesh[] texts = _noteTextMeshes.Where(t => t.text.Length == 1).ToArray();
                for(int i = 0; i < texts.Length; i++)
                {
                    texts[i].text = (i + 1).ToString();
                }
                flickerObjs.AddRange(texts.Select(t => t.gameObject));
                LogQuirk("the note labels are numbers");
                break;
            case 5: // Sussy accidentals
                TextMesh[] texts2 = _noteTextMeshes.Where(t => t.text.Length != 1).ToArray();
                for(int i = 0; i < texts2.Length; i++)
                {
                    texts2[i].text = "ඞ";
                }
                flickerObjs.AddRange(texts2.Select(t => t.gameObject));
                LogQuirk("the sharp/flat note labels are suspicious");
                break;
            case 6: // Inverted arrows
                foreach(TextMesh t in _textMeshes.Where(t => t.text != string.Empty))
                    t.text = "▼";
                flickerObjs.AddRange(_textMeshes.Select(m => m.gameObject));
                LogQuirk("the arrows are upside-down");
                break;
            case 7: // Wrong highlighted note
                int other = (position + Rnd.Range(1, 11)) % 12;
                _noteTextMeshes[position].GetComponentInParent<SpriteRenderer>().color = Color.white;
                _noteTextMeshes[other].GetComponentInParent<SpriteRenderer>().color = new Color(0.99371195f, 1f, 0.5441177f);
                flickerObjs.Add(_noteTextMeshes[position].GetComponentInParent<SpriteRenderer>().gameObject);
                flickerObjs.Add(_noteTextMeshes[other].GetComponentInParent<SpriteRenderer>().gameObject);
                LogQuirk("the wrong position is highlighted");
                break;
        }
    }

    private class Quality
    {
        internal int[] offsets;

        internal static string[] notes = new string[] { "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯" };

        internal Quality(int[] off)
        {
            offsets = off;
        }

        internal static Quality[] getQualities()
        {
            return new Quality[12] {
                new Quality(new int[] {4,7,10}),
                new Quality(new int[] {3,7,10}),
                new Quality(new int[] {4,7,11}),
                new Quality(new int[] {3,7,11}),
                new Quality(new int[] {3,4,10}),
                new Quality(new int[] {3,6,10}),
                new Quality(new int[] {2,4,7}),
                new Quality(new int[] {2,3,7}),
                new Quality(new int[] {4,8,10}),
                new Quality(new int[] {4,8,11}),
                new Quality(new int[] {5,7,10}),
                new Quality(new int[] {3,8,11})
            };
        }
    }

    private class Chord
    {
        internal int note;
        internal Quality quality;

        internal Chord(Quality q, int r)
        {
            note = r;
            quality = q;
        }
    }
}

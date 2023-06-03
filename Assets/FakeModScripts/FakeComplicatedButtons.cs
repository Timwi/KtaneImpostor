using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeComplicatedButtons : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Cb"; } }
    public TextMesh[] buttonTexts;
    public Renderer[] buttonRenderers;
    public Texture[] Textures;
    public Mesh shortButtonMesh;
    private int Case;

    string[] standardWords = { "Press", "Hold", "Detonate" };
    string[] funkyWords = { "Explode", "KABOOOM!", "Drive", "Play", "Pause", "Stop", "Halt", "??!!!???", "&&£^~@", "Report", "KURO", "GOODIFSH", "AYEKA"};
    static readonly Color[] colors = new[] { new Color32(0xff, 0x41, 0x41, 0xff), new Color32(0x41, 0x86, 0xff, 0xff), new Color32(0xff, 0xff, 0xff, 0xff) }.Select(c => (Color)c).ToArray();

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var color1 = Rnd.Range(0, 3);    // 0 = red, 1 = blue, 2 = white
            var color2 = Rnd.Range(0, 3);
            buttonRenderers[i].material.mainTexture = Textures[color1 * 3 + color2 + (i == 0 ? 0 : 9)];
            buttonRenderers[i].material.color = color1 == color2 ? colors[color1] : Color.white;
            buttonTexts[i].text = standardWords.PickRandom();
        }

        int randomButton = Rnd.Range(0, 3);

        Case = Rnd.Range(0, 3);
        switch (Case)
        {
            case 0:
                AddFlicker(buttonRenderers[randomButton]);
                buttonRenderers[randomButton].material.mainTexture = null;
                buttonRenderers[randomButton].material.color = new Color32(180, 38, 255, 255);
                LogQuirk("button {0} is purple", randomButton+1);
                break;
            case 1:
                AddFlicker(buttonTexts[randomButton]);
                buttonTexts[randomButton].text = funkyWords.PickRandom();
                LogQuirk("button {0} says {1}", randomButton+1, buttonTexts[randomButton].text);
                break;
            case 2:
                int randomBottom = Rnd.Range(0, 2)+1;
                AddFlicker(buttonRenderers[randomBottom]);
                buttons[randomBottom].GetComponent<MeshFilter>().mesh = shortButtonMesh;
                Transform buttonTransform = buttons[randomBottom].gameObject.transform;
                buttonTransform.localPosition = new Vector3(-0.0185f, 0.0038f, buttonTransform.localPosition.z);
                buttonRenderers[randomBottom].material.mainTexture = Textures[(Array.IndexOf(Textures, buttonRenderers[randomBottom].material.mainTexture) + 9) % 18];

                Transform buttonHighlightTransform = buttons[randomBottom].GetComponentInChildren<KMHighlightable>().transform;
                buttonHighlightTransform.localScale = new Vector3(0.09f, buttonHighlightTransform.localScale.y, buttonHighlightTransform.localScale.z);
                LogQuirk("button {0} is too short", randomBottom+1);
                break;

        }
    }
}

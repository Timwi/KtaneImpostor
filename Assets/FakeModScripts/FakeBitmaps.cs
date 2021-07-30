using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeBitmaps : ImpostorMod {

    public TextMesh[] texts;
    public MeshRenderer BmpObject;

    private bool[][] _bitmap;
    private static readonly string[] ordinals = { "1st", "2nd", "3rd", "4th" };
    private static readonly Color[] _lightColors = new[] { new Color(1, .9f, .9f), new Color(.9f, 1, .9f), new Color(.9f, .9f, 1), new Color(1, 1, .9f), new Color(.9f, 1, 1), new Color(1, .9f, 1) };
    private static readonly Color[] _darkColors = new[] { new Color(.75f, .5f, .5f), new Color(.5f, .75f, .5f), new Color(.5f, .5f, .75f), new Color(.75f, .75f, .5f), new Color(.5f, .75f, .75f), new Color(.75f, .5f, .75f) };
    private int _colorIx;
    private int changedButton;

    // Use this for initialization
    void Start () {
        _colorIx = Rnd.Range(0, 6);
        _bitmap = new bool[8][];
        for (int j = 0; j < 8; j++)
        {
            _bitmap[j] = new bool[8];
            for (int i = 0; i < 8; i++)
                _bitmap[j][i] = Rnd.Range(0, 2) == 0;
        }
        BmpObject.material.mainTexture = generateTexture();
        changedButton = Rnd.Range(0, 4);
        texts[changedButton].text = (Enumerable.Range(-1, 9).Where(x => x != changedButton).PickRandom() + 1).ToString();
        flickerObjs.Add(texts[changedButton].gameObject);
        Log(string.Format("the {0} button has its label set to {1}", ordinals[changedButton], texts[changedButton].text));
    }

    private Texture generateTexture()
    {
        const int padding = 9;
        const int thickSpacing = 6;
        const int thinSpacing = 3;
        const int cellWidth = 30;

        const int bitmapSize = 8 * cellWidth + 6 * thinSpacing + 1 * thickSpacing + 2 * padding;

        var tex = new Texture2D(bitmapSize, bitmapSize, TextureFormat.ARGB32, false);

        for (int x = 0; x < bitmapSize; x++)
            for (int y = 0; y < bitmapSize; y++)
                tex.SetPixel(x, y, new Color(0, 0, 0));

        Action<int, Color[]> drawLine = (int c, Color[] colors) =>
        {
            for (int j = 0; j < bitmapSize; j++)
            {
                tex.SetPixel(c, j, colors[_colorIx]);
                tex.SetPixel(j, c, colors[_colorIx]);
            }
        };

        var offsets = new List<int>();

        var crd = 0;
        for (int p = 0; p < padding; p++)
            drawLine(crd++, _lightColors);
        for (int i = 0; i < 3; i++)
        {
            offsets.Add(crd);
            crd += cellWidth;
            for (int q = 0; q < thinSpacing; q++)
                drawLine(crd++, _lightColors);
        }
        offsets.Add(crd);
        crd += cellWidth;
        for (int q = 0; q < thickSpacing; q++)
            drawLine(crd++, _lightColors);
        for (int i = 0; i < 3; i++)
        {
            offsets.Add(crd);
            crd += cellWidth;
            for (int q = 0; q < thinSpacing; q++)
                drawLine(crd++, _lightColors);
        }
        offsets.Add(crd);
        crd += cellWidth;
        for (int p = 0; p < padding; p++)
            drawLine(crd++, _lightColors);

        for (int x = 0; x < _bitmap.Length; x++)
            for (int y = 0; y < _bitmap[x].Length; y++)
                if (_bitmap[x][y])
                    for (int i = 0; i < cellWidth; i++)
                        for (int j = 0; j < cellWidth; j++)
                            tex.SetPixel(
                                // The bitmap is displayed mirrored in the X direction, so swap left/right here
                                bitmapSize - 1 - offsets[x] - i,
                                offsets[y] + j,
                                _darkColors[_colorIx]);

        tex.Apply();
        return tex;
    }
}

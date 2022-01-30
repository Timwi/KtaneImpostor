using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeMicrocontroller : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Mcc"; } }
    public GameObject[] chipSizes, wholeLEDs;
    public MeshRenderer[] leds;
    public Material whiteLED;
    public Transform dot;
    public TextMesh bigLabel, number;
    public Texture[] bgTextures;
    public MeshRenderer bg;

    private static string[] realLabels = { "STRK", "LEDS", "CNTD", "EXPL"};
    private static string[] fakeLabels = { "CHGG", "BLAN", "BOMB", "BOOM", "DETN", "TIMR", "MICR", "IPST"};
    private int chipVal, litLED, dotPos;
    private int Case;

    float transZ = 0.0535f;
    float[] transXs = { -0.0435f, -0.066f, -0.088f };
    private int PinCount
    { get { return 2 * chipVal + 6; } }


    void Start()
    {
        for (int i = 0; i < 3; i++)
            chipSizes[i].SetActive(false);

        chipVal = Rnd.Range(0, 3);
        chipSizes[chipVal].SetActive(true);
        bg.material.mainTexture = bgTextures[chipVal];
        for (int i = PinCount; i < 10; i++)
            wholeLEDs[i].SetActive(false);

        bigLabel.text = realLabels.PickRandom();
        GetSerial();

        dotPos = Rnd.Range(0, 4);
        MoveDot();

        litLED = Rnd.Range(0, PinCount);
        leds[litLED].material = whiteLED;
        
        Case = Rnd.Range(0,3); 
        switch (Case)
        {
            case 0:
                dot.gameObject.SetActive(false);
                flickerObjs.Add(dot.gameObject);
                LogQuirk("the dot is missing");
                break;
            case 1:
                int otherLED;
                do otherLED = Rnd.Range(0, PinCount);
                while (otherLED == litLED);
                leds[otherLED].material = whiteLED;
                LogQuirk("there are two lit LEDs");
                flickerObjs.Add(wholeLEDs[litLED]);
                flickerObjs.Add(wholeLEDs[otherLED]);
                break;
            case 2:
                bigLabel.text = fakeLabels.PickRandom();
                LogQuirk("the chip's type is {0}", bigLabel.text);
                flickerObjs.Add(bigLabel.gameObject);
                break;
        }
    }
    void GetSerial()
    {
        int limit = chipVal == 0 ? 4 : 7;
        string serial = "";
        for (int i = 0; i < limit; i++)
            serial += Rnd.Range(0, 10);
        serial = serial.Insert(Ut.RandBool() ? 2 : 3, "-");
        number.text = serial;
    }
    void MoveDot()
    {
        float x = transXs[chipVal];
        float z = transZ;
        switch (dotPos)
        {
            case 0: dot.localPosition += new Vector3(0, 0, 0); break;
            case 1: dot.localPosition += new Vector3(x, 0, 0); break;
            case 2: dot.localPosition += new Vector3(0, 0, z); break;
            case 3: dot.localPosition += new Vector3(x, 0, z); break;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}

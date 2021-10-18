using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeOrientationCube : ImpostorMod 
{
    [SerializeField]
    private GameObject observer;
    [SerializeField]
    private Transform observerTF;
    [SerializeField]
    public TextMesh[] buttonLabels, cubeFaceLabels;
    private int Case;
    private static readonly string[] btnNames = { "left", "right", "clockwise", "counter-clockwise" };
    void Start()
    {
        observerTF.localEulerAngles = new Vector3(-90, 90 * Rnd.Range(0, 4), 0);
        Case = Rnd.Range(0, 4);
        switch (Case)   
        {
            case 0:
                int alteredPos = Rnd.Range(1, 5);
                buttonLabels[0].text = buttonLabels[alteredPos].text;
                buttonLabels[alteredPos].text = "SET";
                buttonLabels[0].transform.localScale = buttonLabels[alteredPos].transform.localScale;
                buttonLabels[alteredPos].transform.localScale = buttonLabels[0].transform.localScale;

                flickerObjs.Add(buttonLabels[0].gameObject);
                flickerObjs.Add(buttonLabels[alteredPos].gameObject);
                LogQuirk("the SET button is swapped with the {0} button", btnNames[alteredPos - 1]);
                break;
            case 1:
                observer.SetActive(false);
                LogQuirk("there is no observer");
                flickerObjs.Add(observer);
                break;
            case 2:
                observerTF.localEulerAngles += 45 * Vector3.up;
                LogQuirk("the observer is at a 45° angle");
                flickerObjs.Add(observer);
                break;
            case 3:
                int alteredFace = Rnd.Range(1, 5);
                cubeFaceLabels[0].text = cubeFaceLabels[alteredFace].text;
                cubeFaceLabels[0].transform.localScale = cubeFaceLabels[alteredFace].transform.localScale;
                LogQuirk("the top face of the cube says {0}", cubeFaceLabels[0].text);
                flickerObjs.Add(cubeFaceLabels[0].gameObject);
                break;
        }
    }
}

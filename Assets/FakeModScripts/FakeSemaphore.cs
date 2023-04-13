using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

//Coded by Blan
public class FakeSemaphore : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Smp"; } }

    public TextMesh[] texts;
    public GameObject[] flags; 
    private static readonly string[] dummies = {"K", "KO", " ", "YES", "MK", "OJ"};
    private float timer = 0f;
    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 3);
        switch (Case) {
            case 0:
                string chosen = dummies.PickRandom().ToString();
                texts[2].text = chosen;
                flickerObjs.Add(texts[2].gameObject);
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, (Ut.RandBool() ? -45f : -90f));
                LogQuirk(string.Format("the square buttons says \"{0}\"", chosen));
            break;
            case 1:
                texts[0].text = ">";
                texts[1].text = "<";
                flickerObjs.Add(texts[0].gameObject);
                flickerObjs.Add(texts[1].gameObject);
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, (Ut.RandBool() ? -45f : -90f));
                LogQuirk("the left and right buttons have swapped");
            break;
            case 2:
                flickerObjs.Add(flags[0]);
                flickerObjs.Add(flags[1]);
                LogQuirk("the flags are waving");
                StartCoroutine(Wave());
            break;
        }
    }

    private IEnumerator Wave()
    {
        while (!willSolve) {
            if (timer < 0.25f) {
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, -45f-(timer*360f));
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 45f+(timer*360f));
                yield return null;
                timer += Time.deltaTime;
            } else if (timer < 0.5f) {
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, -45f-((0.5f-timer)*360f));
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 45f+((0.5f-timer)*360f));
                yield return null;
                timer += Time.deltaTime;
            } else {
                timer = 0f;
            }
        }
    }
}

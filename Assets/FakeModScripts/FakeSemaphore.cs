using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

//Coded by blananas2
public class FakeSemaphore : ImpostorMod 
{
    public TextMesh[] texts;
    public GameObject[] flags; 
    private static readonly string[] dummies = {"K", "KO", " ", "YES", "MK", "OJ"};

    void Start()
    {
        if (Ut.RandBool())
        {
            string chosen = dummies.PickRandom().ToString();
            texts[2].text = chosen;
            flickerObjs.Add(texts[2].gameObject);
            flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, ((Rnd.Range(0,2)==0) ? -45f : -90f));
            LogQuirk(string.Format("the square buttons says \"{0}\"", chosen));
        }
        else
        {
            texts[0].text = ">";
            texts[1].text = "<";
            flickerObjs.Add(texts[0].gameObject);
            flickerObjs.Add(texts[1].gameObject);
            flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, ((Rnd.Range(0,2)==0) ? -45f : -90f));
            LogQuirk("the left and right buttons have swapped");
        }
    }

}

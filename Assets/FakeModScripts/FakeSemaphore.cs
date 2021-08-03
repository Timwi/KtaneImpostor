using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

public class FakeSemaphore : ImpostorMod 
{
    [SerializeField]
	private TextMesh[] texts;
    [SerializeField]
    private GameObject[] flags; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.
    private int Case;

    void Start()
    {
        string[] dummies = {"K", "KO", " "};
        int left = UnityEngine.Random.Range(0, 8);
        int right = UnityEngine.Random.Range(0, 8);
        while (left == 0 && (right == 1 || right == 2)) {
            left = UnityEngine.Random.Range(0, 8);
            right = UnityEngine.Random.Range(0, 8);
        }
        Case = Rnd.Range(0, 3); //However many cases you want there to be.
        switch (Case)
        {
            case 0: //ok
                string chosen = dummies.PickRandom().ToString();
                texts[2].text = chosen;
                flickerObjs.Add(texts[2].gameObject);
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, ((Rnd.Range(0,2)==0) ? -45f : -90f));
                Log(string.Format("the square buttons says \"{0}\"", chosen));
                break;
            case 1:
                texts[0].text = ">";
                texts[1].text = "<";
                flickerObjs.Add(texts[0].gameObject);
                flickerObjs.Add(texts[1].gameObject);
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, ((Rnd.Range(0,2)==0) ? -45f : -90f));
                Log("the left and right buttons have swapped");
                break;
            case 2:
                flickerObjs.Add(flags[0]);
                flickerObjs.Add(flags[1]);
                flags[0].transform.localRotation = Quaternion.Euler(0f, 0f, left*45f);
                flags[1].transform.localRotation = Quaternion.Euler(0f, 0f, right*-45f);
                Log(string.Format("...the flags are {0}", SemaFlags(left, right)));
                break;
        }
    }

    string SemaFlags(int l, int r) {
        string[] sinister = {"N", "NW", "W", "SW", "S", "SE", "E", "NE"};
        string[] dexter = {"N", "NE", "E", "SE", "S", "SW", "W", "NW"};
        return sinister[l] + " and " + dexter[r];
    }
}

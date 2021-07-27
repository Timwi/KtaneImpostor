using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class impostorScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public GameObject[] Prefabs;
    private GameObject ChosenPrefab;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    private string[] modNames = {"Letter Keys", "Piano Keys"};
    private int chosenMod;

    void Awake () {
        moduleId = moduleIdCounter++;
    }

    // Use this for initialization
    void Start () {
        chosenMod = UnityEngine.Random.Range(0, modNames.Length);
        Debug.LogFormat("[The Impostor #{0}] I may look like {1}, but do not be fooled...", moduleId, modNames[chosenMod]);
        ChosenPrefab = Instantiate(Prefabs[chosenMod], Vector3.zero, Quaternion.identity, this.transform);
    }
}

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
    public KMBombModule Module;
    public KMHighlightable ModHL;
    public KMSelectable SelectableComp;

    public GameObject[] Prefabs;
    public GameObject BG;
    public GameObject SL;
    private GameObject ChosenPrefab;
    private ImpostorMod chosenScript;
        
    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private int chosenMod;
    private bool solved;

    void Awake () {
        moduleId = moduleIdCounter++;
    }

    // Use this for initialization
    private void Start () 
    {
        GetMod();
        GetScript();
        GetSelectables();
        StartCoroutine(Hehe());
        Module.OnActivate += delegate () { chosenScript.OnActivate(); };
        if (Bomb.GetModuleNames().Contains("Organization"))
        {
            Debug.LogFormat("[The Impostor #{0}] Organization detected, the module will not award strikes.", moduleId);
            chosenScript.orgPresent = true;
        }
    }
    private void GetMod()
    {
        BG.SetActive(false);
        chosenMod = UnityEngine.Random.Range(0, Prefabs.Length);
chosenMod = Prefabs.Length - 2;
        ChosenPrefab = Instantiate(Prefabs[chosenMod], Vector3.zero, Quaternion.identity, this.transform);
        ChosenPrefab.transform.localPosition = Vector3.zero;
        ChosenPrefab.transform.localRotation = Quaternion.identity;
        Debug.LogFormat("[The Impostor #{0}] I may look like {1}, but do not be fooled...", moduleId, Prefabs[chosenMod].name);
    }
    private void GetScript()
    {
        chosenScript = ChosenPrefab.GetComponent<ImpostorMod>();
        chosenScript.moduleId = moduleId;
        chosenScript.Audio = Audio;
        chosenScript.Module = Module;
        chosenScript.BombInfo = Bomb;
        chosenScript.solve += delegate () { Solve(); };
        SL.transform.localPosition = SLP.StatusPositions[chosenScript.SLPos];
    }
    private void GetSelectables()
    {
        KMSelectable[] btns = chosenScript.buttons;
        SelectableComp.Children = new KMSelectable[btns.Length];
        for (int i = 0; i < btns.Length; i++)
        {
            SelectableComp.Children[i] = btns[i];
            btns[i].Parent = SelectableComp;
        }
        SelectableComp.UpdateChildren();
    }
    private void Solve()
    {
        solved = true;
        Debug.LogFormat("[The Impostor #{0}] Module solved.", moduleId);
        Module.HandlePass();
        Audio.PlaySoundAtTransform("willSolve", transform);
        ChosenPrefab.SetActive(false);
        SL.transform.localPosition = SLP.StatusPositions[SLPositions.TR];
        BG.SetActive(true);
        SelectableComp.Children = new KMSelectable[0];
    }
    IEnumerator Hehe()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(15f, 30f));
        if (!solved)
        {
            Audio.PlaySoundAtTransform("hello", transform);
            Debug.LogFormat("<The Impostor #{0}> Laugh occured.", moduleId);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public sealed class impostorScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;
    public KMHighlightable ModHL;
    public KMSelectable SelectableComp;

    public GameObject[] Prefabs;
    public GameObject BG;
    public GameObject SL;
    private GameObject chosenPrefab;
    private ImpostorMod chosenScript;
    private static ImpostorSettings settings = new ImpostorSettings();
        
    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private int chosenMod;
    private bool solved;

    void Awake () {
        moduleId = moduleIdCounter++;
    }

    private void Start () 
    {
        Debug.LogFormat("<The Impostor #{0}> Impostor module loaded with version 2.0.0", moduleId);
        SetUpModSettings();
        GetMod();
        GetScript();
        GetSelectables();
        StartCoroutine(Hehe());
        Module.OnActivate += () => chosenScript.OnActivate();
        if (Bomb.GetModuleNames().Contains("Organization"))
        {
            Debug.LogFormat("[The Impostor #{0}] Organization detected, the module will not award strikes.", moduleId);
            chosenScript.orgPresent = true;
        }
    }
    private void GetMod()
    {
        BG.SetActive(false);
        List<int> allowedPrefabIndices = new List<int>();
        if (!Application.isEditor)
        {
            for (int i = 0; i < Prefabs.Length; i++)
            {
                string key = "Disable " + Prefabs[i].name;
                if (!settings.disabledModsList.ContainsKey(key))
                    Debug.LogFormat("[The Impostor] Prefab name {0} not found within modsettings dictionary!", Prefabs[i].name);
                else if (!settings.disabledModsList[key])
                    allowedPrefabIndices.Add(i);
            }
        }
        if (allowedPrefabIndices.Count == 0)
            allowedPrefabIndices = Enumerable.Range(0, Prefabs.Length).ToList();
        chosenMod = allowedPrefabIndices.PickRandom();
chosenMod = Enumerable.Range(0, Prefabs.Length).First(x => Prefabs[x].name.StartsWith("Only Connect", StringComparison.InvariantCultureIgnoreCase));

        chosenPrefab = Instantiate(Prefabs[chosenMod], Vector3.zero, Quaternion.identity, this.transform);
        chosenPrefab.transform.localPosition = Vector3.zero;
        chosenPrefab.transform.localRotation = Quaternion.identity;
        Debug.LogFormat("[The Impostor #{0}] I may look like {1}, but do not be fooled...", moduleId, Prefabs[chosenMod].name);
    }
    private void GetScript()
    {
        chosenScript = chosenPrefab.GetComponent<ImpostorMod>();
        chosenScript.moduleId = moduleId;
        chosenScript.Audio = Audio;
        chosenScript.Module = Module;
        chosenScript.BombInfo = Bomb;
        chosenScript.solve += () => Solve(); 
        SL.transform.localPosition = SLDict.StatusPositions[chosenScript.SLPos];
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
        Audio.PlaySoundAtTransform("solve", transform);
        chosenPrefab.SetActive(false);
        SL.transform.localPosition = SLDict.StatusPositions[SLPositions.TR];
        BG.SetActive(true);
        SelectableComp.Children = new KMSelectable[0];
    }
    IEnumerator Hehe()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(15f, 30f));
        if (!solved)
            Audio.PlaySoundAtTransform("hehe", transform);
    }
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use <!{0} disarm> to solve the module. Any other command will cause a strike.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToUpperInvariant();
        yield return null;
        chosenScript.buttons[0].OnInteract();
        if (command == "DISARM")
            yield return new WaitUntil(() => chosenScript.willSolve);
        yield return new WaitForSeconds(0.2f);
        chosenScript.buttons[0].OnInteractEnded();
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        chosenScript.buttons[0].OnInteract();
        yield return new WaitForSeconds(0.1f);
        while (!chosenScript.willSolve)
            yield return true;
        yield return new WaitForSeconds(0.1f);
        chosenScript.buttons[0].OnInteractEnded();
    }

    class ImpostorSettings
    {
        public Dictionary<string, bool> disabledModsList = new Dictionary<string, bool>();
        public string note;
    }
    private void SetUpModSettings()
    {
        ModConfig<ImpostorSettings> config = new ModConfig<ImpostorSettings>("ImpostorSettings");
        settings = config.Read();
        foreach (GameObject prefab in Prefabs)
        {
            string key = "Disable " + prefab.name;
            if (!settings.disabledModsList.ContainsKey(key))
            {
                Debug.LogFormat("[The Impostor] Added entry {0} to the settings file", key);
                settings.disabledModsList.Add(key, false);
            }
        }
        config.Write(settings);
    }

    private static Dictionary<string, object>[] TweaksEditorSettings = new Dictionary<string, object>[]
    {
            new Dictionary<string, object>
            {
            { "Filename", "ImpostorSettings.json"},
            { "Name", "The Impostor" },
            { "Listings", new List<Dictionary<string, object>>
                {
                   // new Dictionary<string, object>
                   // {
                   // { "Key", "disabledModsList" },
                   // { "Text", "Prevent these mods from appearing."}
                   // },
                    new Dictionary<string, object>
                    {
                        {"Key", "note" },
                        {"Text", "Please enter the ImpostorSettings.json file to manage disabled mods." }
                    }
                }
            }
            }
    };

}

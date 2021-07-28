using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class ImposterMod : MonoBehaviour
{
    [HideInInspector]
    public KMAudio Audio;
    [HideInInspector]
    public KMBombModule Mod;
    [HideInInspector]
    public int moduleId;
    [HideInInspector]
    public List<GameObject> flickerObjs;

    public KMSelectable[] buttons;
    public SLPositions SLPos { get ; set; }
    public Action solve;
    private bool isHeld, solved;

    void Awake()
    {
        foreach (KMSelectable btn in buttons)
        {
            btn.OnInteract += delegate () { StartCoroutine(HoldBtn(btn)); return false; };
            btn.OnInteractEnded += delegate () { ReleaseBtn(btn); };
        }
    }
    public void Log(string msg)
    {
        Debug.LogFormat("[The Impostor #{0}] {1}", moduleId, msg);
    }

    IEnumerator HoldBtn(KMSelectable btn)
    {
        if (isHeld)
            yield break;
        btn.AddInteractionPunch(1);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
        isHeld = true;
        yield return new WaitForSeconds(3);
        if (isHeld)
        {
            Audio.PlaySoundAtTransform("willSolve", Mod.transform);
            solved = true;
        }

    }
    void ReleaseBtn(KMSelectable btn)
    {
        btn.AddInteractionPunch(0.25f);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, btn.transform);
        isHeld = false;
        if (solved)
            solve.Invoke();
        else
        {
            Log("Released a button before 3 seconds, strike.");
            Audio.PlaySoundAtTransform("strike", Mod.transform);
            Mod.HandleStrike();
            StartCoroutine(Flicker());
        }
    }
    IEnumerator Flicker()
    {
        solved = true;
        for (int i = 0; i < 6; i++)
        {
            foreach (GameObject obj in flickerObjs)
                obj.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            foreach (GameObject obj in flickerObjs)
                obj.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
        solve.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class ImpostorMod : MonoBehaviour
{
    [HideInInspector]
    public KMAudio Audio;
    [HideInInspector]
    public KMBombModule Module;
    [HideInInspector]
    public KMBombInfo BombInfo;
    [HideInInspector]
    public int moduleId;
    [HideInInspector]
    public bool orgPresent;
    [HideInInspector]
    public bool willSolve;
    ///<summary>
    ///A list of GameObjects which will flicker when the module strikes.
    ///</summary>
    [HideInInspector]
    public List<GameObject> flickerObjs;

    public KMSelectable[] buttons;
    /// <summary>
    /// The position of the status light. Defaults to Top-Right
    /// </summary>
    public virtual SLPositions SLPos { get; set; }
    /// <summary>
    /// Lets the fake mod communicate to Impostor when it is going to solve.
    /// </summary>
    public Action solve;
    private bool isHeld;


    void Awake()
    {
        foreach (KMSelectable btn in buttons)
        {
            btn.OnInteract += delegate () { StartCoroutine(HoldBtn(btn)); return false; };
            btn.OnInteractEnded += delegate () { ReleaseBtn(btn); };
        }
    }
    /// <summary>
    /// Sends a log message which works with the LFA. 
    /// </summary>
    /// <param name="msg">The message to be logged, use string.Format for interpolation.</param>
    public void Log(string msg, params object[] args)
    {
        Debug.LogFormat("[The Impostor #{0}] ...{1}, that doesn't seem normal.", moduleId, string.Format(msg, args));
    }

    /// <summary>
    /// Gets called when the lights turn on. Needs to be overridden.
    /// </summary>
    public virtual void OnActivate()
    { }

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
            Audio.PlaySoundAtTransform("willSolve", Module.transform);
            willSolve = true;
        }

    }
    void ReleaseBtn(KMSelectable btn)
    {
        btn.AddInteractionPunch(0.25f);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, btn.transform);
        isHeld = false;
        if (willSolve)
            solve.Invoke();
        else
        {
            Debug.LogFormat("[The Impostor #{0}] You weren't able to identify that I'm The Impostor. Flashing change...", moduleId);
            Audio.PlaySoundAtTransform("strike", Module.transform);
            if (!orgPresent)
                Module.HandleStrike();
            StartCoroutine(Flicker());
        }
    }
    IEnumerator Flicker()
    {
        willSolve = true;
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

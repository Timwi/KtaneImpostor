﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImpostorMod : MonoBehaviour
{
    [HideInInspector]
    public KMAudio Audio;
    [HideInInspector]
    public KMBombModule Module;
    [HideInInspector]
    public KMBombInfo BombInfo;
    [HideInInspector]
    public bool orgPresent;
    public int moduleId { private get; set; }
    ///<summary>
    ///A list of GameObjects which will flicker when the module strikes.
    ///</summary>
    [HideInInspector]
    protected readonly List<GameObject> flickerObjs = new List<GameObject>();
    [HideInInspector]
    public bool cb;

    public bool willSolve { get; private set; }

    public KMSelectable[] buttons;
    /// <summary>
    /// The position of the status light. Defaults to Top-Right
    /// </summary>
    public virtual SLPositions SLPos { get; protected set; }
    /// <summary>
    /// Lets the fake mod communicate to Impostor when it is going to solve.
    /// </summary>
    public Action solve;

    private bool isHeld;

    public abstract string ModAbbreviation { get; }

    private void Awake()
    {
        foreach (KMSelectable btn in buttons)
        {
            btn.OnInteract += delegate () { StartCoroutine(HoldBtn(btn)); return false; };
            btn.OnInteractEnded += delegate () { ReleaseBtn(btn); };
        }
    }
    /// <summary>
    /// Sends a log message which works with the LFA using string.Format<br></br>Do not capitalize the message, or put punctuation at its end. 
    /// </summary>
    /// <example>LogQuirk("the button is red");</example>
    /// <example>LogQuirk("the {0} button is blue", "left")</example>
    /// <param name="msg">The message to be logged, use string.Format's syntax for interpolation.</param>
    protected void LogQuirk(string msg, params object[] args)
    {
        Debug.LogFormat("[The Impostor #{0}] ...{1}, that doesn't seem normal.", moduleId, string.Format(msg, args));
    }
    /// <summary>
    /// Sends a log message which works with the LFA. 
    /// </summary>
    /// <param name="msg">The message to be logged, use string.Format's syntax for interpolation.</param>
    protected void Log(string msg, params object[] args)
    {
        Debug.LogFormat("[The Impostor #{0}] {1}", moduleId, string.Format(msg, args));
    }
    public void ToggleColorblind()
    {
        cb = !cb;
        OnColorblindToggle();
    }

    /// <summary>
    /// Gets called when the lights turn on.
    /// </summary>
    public virtual void OnActivate()
    { }
    /// <summary>
    /// Gets called when colorblind mode is toggled.
    /// </summary>
    protected virtual void OnColorblindToggle() { }
    private IEnumerator HoldBtn(KMSelectable btn)
    {
        if (isHeld)
            yield break;
        btn.AddInteractionPunch(1);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
        isHeld = true;
        yield return new WaitForSeconds(3);
        if (isHeld)
        {
            Audio.PlaySoundAtTransform("solve", Module.transform);
            willSolve = true;
        }
    }
    private void ReleaseBtn(KMSelectable btn)
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
    private IEnumerator Flicker()
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeMurder : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] texts; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.

    private Color[] colors = new Color[]
    {
        new Color (1f, 0.05f, 0.05f),
        new Color (0.8f, 0.15f, 0.55f),
        new Color (0.3f, 0.3f, 1f),
        new Color (0.1f, 0.8f, 0.1f),
        new Color (0.85f, 0.85f, 0.15f),
        new Color (1f, 1f, 1f),
    };
    private static readonly string[] fullNames = { "Miss Scarlet", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White" };
    private static readonly string[] weapons = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner" };
    private static readonly string[] rooms = { "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library" };
    private static readonly string[] prefixes = { "Miss", "Professor", "Mrs", "Reverend", "Colonel", "Mrs" };
    private static readonly string[] names = { "Scarlet", "Plum", "Peacock", "Green", "Mustard", "White" };

    private string displayedName, displayedWeapon;
    private Color displayedColor;
    private int chosenPerson;
    private int Case;

    void Start()
    {
        chosenPerson = Rnd.Range(0, 6);
        displayedWeapon = weapons.PickRandom();
        Case = Rnd.Range(0, 3);
        flickerObjs.Add(texts[0].gameObject);
        if (Case == 0)
        {
            int colorIx = (chosenPerson + Rnd.Range(1, 6)) % 6;
            displayedColor = colors[colorIx];
            Log(string.Format("...{0} is actually using {1}'s color. That doesn't seem normal.", fullNames[chosenPerson], fullNames[colorIx]));
        }
        else displayedColor = colors[chosenPerson];
        if (Case == 1)
        {
            var pair = Enumerable.Range(0, 6).ToArray().Shuffle().Take(2).ToArray();
            displayedName = prefixes[pair[0]] + " " + names[pair[1]];
            Log(string.Format("...the suspect displayed is {0}. That doesn't seem normal.", displayedName));
        }
        else displayedName = fullNames.PickRandom();
        if (Case == 2)
        {
            Log("...the suspect is on the middle screen and the weapon is on the top screen. That doesn't seem normal.");
            flickerObjs.Add(texts[1].gameObject);
            texts[0].text = displayedWeapon;
            texts[1].text = displayedName;
        }
        else
        {
            texts[0].text = displayedName;
            texts[1].text = displayedWeapon;
        }
        texts[0].color = displayedColor;
        texts[2].text = rooms.PickRandom();
        if (Rnd.Range(0, 9) == 0)
            texts[2].color = new Color(1f, 0.05f, 0.05f);
    }
}

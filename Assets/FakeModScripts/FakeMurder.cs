using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeMurder : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] texts;

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
    private static readonly string[] prefixes = { "Miss", "Professor", "Mrs", "Reverend", "Colonel", "Mrs"};
    private static readonly string[] names = { "Scarlet", "Plum", "Peacock", "Green", "Mustard", "White" };

    private string displayedName, displayedWeapon;
    private Color displayedColor;
    private int chosenPerson;
    private int Case;

    void Start()
    {
        chosenPerson = Rnd.Range(0, 6);
        displayedName = fullNames[chosenPerson];
        displayedWeapon = weapons.PickRandom();
        displayedColor = colors[chosenPerson];
        Case = Rnd.Range(0, 4);
        flickerObjs.Add(texts[0].gameObject);
        if (Case == 0)
        {
            int colorIx;
            do colorIx = Rnd.Range(0, 5);
            while (colorIx == chosenPerson);
            displayedColor = colors[colorIx];
            Log("{0} is actually using {1}'s color.", fullNames[chosenPerson], fullNames[colorIx]);
         }
        else if (Case == 1)
        {
            int ending = Rnd.Range(0, 6);
            int prefix = Enumerable.Range(0, 6).Where(x => prefixes[x][0] != prefixes[ending][0]).PickRandom();
            displayedName = prefixes[prefix] + " " + names[ending];
            Debug.Log(displayedName.Length);
            if (displayedName.Length >= 16)
                texts[0].transform.localScale = new Vector3(0.0009f, 0.001f, 0.001f);
            displayedColor = colors[ending];
            Log("the suspect displayed is {0}", displayedName);
        }
        else if (Case == 2)
        {
            displayedName = "Dr. Orchid";
            displayedColor = "#C83291".Color();
            Debug.LogFormat("[The Impostor #{0}] The suspect displayed is Dr. Orchid. This is terrible. This should never happen. Dispose of the bomb immediately.", moduleId);
        }
        if (Case == 3)
        {
            Log("the suspect is on the middle screen and the weapon is on the top screen");
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

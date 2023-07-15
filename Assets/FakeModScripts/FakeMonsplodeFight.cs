using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeMonsplodeFight : ImpostorMod
{
    public override string ModAbbreviation { get { return "Mf"; } }

    public SpriteRenderer screen;
    public Sprite[] sussySprites, regularSprites;
    public Sprite missingNo;

    public TextMesh[] buttonTexts;

    public MeshRenderer[] backgrounds;
    private int Case;

    int[] moveIx = new int[4];
    private readonly string[] moveNames =
    {
        "Appearify", "Battery Power", "Bedrock", "Boo", "Boom", "Bug Spray", "Countdown", "Dark Portal", "Fiery Soul", "Finale", "Freak out",
        "Glyph", "Last Word", "Sendify", "Shock", "Shrink", "Sidestep", "Stretch", "Void", "Defuse",
        "Candle", "Cave In", "Double Zap", "Earthquake", "Flame Spear", "Fountain", "Grass Blade", "Heavy Rain", "High Voltage", "Hollow Gaze", "Ivy Spikes",
        "Spectre", "Splash", "Tac", "Tangle", "Tic", "Toe", "Torchlight", "Toxic Waste", "Venom Fang", "Zap"
    };

    private readonly string[] sussyNames = { "Amogus", "Big Smoke", "ENA", "Espik", "Fat Pikachu", "Jerma", "Rosie Pepsi", "Shadow Wizard Money Gang" };

    private readonly Coroutine[] flickerCoroutines = new Coroutine[6];

    void Start()
    {
        Case = Rnd.Range(0, 2);
        switch (Case)
        {
            case 0:
                var rndSussy = Rnd.Range(0, sussySprites.Length);
                screen.sprite = sussySprites[rndSussy];
                for (int i = 0; i < 4; i++)
                {
                    moveIx[i] = Enumerable.Range(0, moveNames.Length).Where(x => x != 19).PickRandom();
                    buttonTexts[i].text = moveNames[moveIx[i]].Replace(" ", "\n");
                }
                LogQuirk("your opponent is {0}", sussyNames[rndSussy]);
                AddFlicker(screen);
                break;
            case 1:
                screen.sprite = regularSprites.PickRandom();
                foreach (var text in buttonTexts)
                {
                    text.text = "Boom";
                }
                LogQuirk("all of the buttons say \"Boom\"");
                AddFlicker(buttonTexts);
                break;
        }
    }
}

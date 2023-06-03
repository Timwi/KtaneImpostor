using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeLogic : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Lo"; } }
    public TextMesh[] letters, logicSymbols, buttonLabels;
    public Transform[] parentheses;
    public MeshRenderer[] leds, buttonRenderers;
    private int Case;

    static readonly string standardSymbols = "∧∨⊻|↓↔→←";
    static readonly string alternateSymbols = "&⊕↑⊙ඞ";

    bool[] tfVals = new bool[2];
    string[] positions = { "top-left", "top-right", "bottom-left", "bottom-right" };

    public override void OnActivate()
    {
        for (int i = 0; i < 6; i++)
        {
            letters[i].text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".PickRandom().ToString();
            if (Ut.RandBool())
                leds[i].material.color = Color.red;
        }
        for (int i = 0; i < 4; i++)
            logicSymbols[i].text = standardSymbols.PickRandom().ToString();

        for (int i = 0; i < 2; i++)
            if (Ut.RandBool())
                parentheses[i].localScale = new Vector3(-1, 1, 1);
        for (int i = 0; i < 2; i++)
        {
            tfVals[i] = Ut.RandBool();
            buttonLabels[i].text = tfVals[i] ? "T" : "F";
            buttonRenderers[i].material.color = tfVals[i] ? Color.green : Color.red;
        }

        Case = Rnd.Range(0, 3);
        int change;
        switch (Case)
        {
            case 0: //Remove parentheses
                change = Rnd.Range(0, 2);
                parentheses[change].gameObject.SetActive(false);
                AddFlicker(parentheses[change]);
                LogQuirk("the {0} expression has no brackets", change == 0 ? "top" : "bottom");
                break;
            case 1: //Inconsistent button color
                change = Rnd.Range(0, 2);
                buttonLabels[change].text = tfVals[change] ? "F" : "T";
                AddFlicker(buttonLabels[change]);
                LogQuirk("the {0} button's label is {1} while it's color is {2}", change == 0 ? "top" : "bottom", buttonLabels[change].text, tfVals[change] ? "green" : "red");
                break;
            case 2: //Fake operator
                change = Rnd.Range(0, 4);
                logicSymbols[change].text = alternateSymbols.PickRandom().ToString();
                AddFlicker(logicSymbols[change]);
                LogQuirk("the {0} operator is a {1}", positions[change], logicSymbols[change].text);
                break;
        }
    }
}

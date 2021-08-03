using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeColourFlash : ImpostorMod
{
    [SerializeField]
    private TextMesh yes, no, display; 
    private static readonly string[] fakeYes = { "YEE", "YAS", "YEP", "YEA", "YEH", "YAH" };
    private static readonly string[] fakeNo = { "NOPE", "NAH", "NAW", "NOT", "NIL", "NADA" };
    private static readonly string[] colorNames = { "RED", "YELLOW", "GREEN", "BLUE", "MAGENTA", "WHITE" };
    private static readonly Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta, Color.white };
    private static readonly string[] weirdColors = { "IVORY", "CHERRY", "NAVY", "OLIVE", "LEMON", "MAHOGANY", "BONE", "SAGE", "MERLOT", "ROSE", "SEAFOAM", "HONEY", "FUCHSIA", "SCARLET", "PICKLE", "FLAMINGO", "DIJON", "PEACOCK", "PINK", "CHIFFON", "BLONDE", "BRICK", "LAPIS", "PINE", "GARNET", "COBALT", "COTTON", "CORAL", "CRIMSON", "EMERALD", "DENIM", "FANDANGO", "CONIFER", "SALT", "MAYA", "RUBY", "FOREST", "DOLLY", "PLUM", "EGGSHELL", "MANTIS", "PEARL", "CINNABAR", "FLAX", "SHAMROCK", "AZURE", "PHLOX", "FROST", "INDIGO", "TURMERIC", "ORCHID", "CERULEAN", "CANARY", "ALGAE", "LINEN", "MUSTARD", "SANGUINE", "EGGPLANT", "SPRING", "RUST", "CARDINAL", "JADE", "LACE", "ATOLL", "LILAC", "SWAMP", "MALIBU", "CLARET", "CHALK", "CREAM", "GIMBLET", "BOUQUET", "SIENNA", "MAUVE", "MARIGOLD", "COCONUT", "FERN", "LAGOON", "MINT", "JAVA", "CERAMIC", "MATISSE", "PEAR", "SNOW", "MILANO", "MULBERRY", "TACHA", "BURGUNDY", "CHENIN", "BLOSSOM", "PRUSSIA", "MANGO", "SPINEL", "CHINO", "LAUREL", "MARINER", "MARBLE", "CHERUB", "GLACIER", "TOPAZ" };

    private string[] wordSequence = new string[8];
    private Color[] colorSequence = new Color[8];

    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 4);
        for (int i = 0; i < 8; i++)
        {
            wordSequence[i] = colorNames.PickRandom();
            colorSequence[i] = colors.PickRandom();
        }
        switch (Case)
        {
            case 0:
                if (Rnd.Range(0,2) == 0)
                {
                    flickerObjs.Add(yes.gameObject);
                    yes.text = fakeYes.PickRandom();
                    Log(string.Format("the left button says {0}", yes.text));
                    break;
                }
                else
                {
                    flickerObjs.Add(no.gameObject);
                    no.text = fakeNo.PickRandom();
                    Log(string.Format("the right button says {0}", no.text));
                    break;
                }
            case 1:
                flickerObjs.Add(yes.gameObject);
                flickerObjs.Add(no.gameObject);
                yes.text = "NO";
                no.text = "YES";
                Log("the 'YES' and 'NO' buttons have swapped");
                break;
            case 2:
                flickerObjs.Add(display.gameObject);
                wordSequence[7] = weirdColors.PickRandom();
                Log(string.Format("the last word is {0}", wordSequence[7]));
                break;
            case 3:
                flickerObjs.Add(display.gameObject);
                Log("the sequence never ends");
                break;
        }
        
    }
    public override void OnActivate()
    {
        StartCoroutine(Flash());
    }
    private IEnumerator Flash()
    {
        if (Case != 4)
            while (true)
            {
                for (int i = 0; i < 8; i++)
                {
                    display.text = wordSequence[i];
                    display.color = colorSequence[i];
                    yield return new WaitForSeconds(0.75f);
                }
                display.text = "";
                yield return new WaitForSeconds(2);
            }
        else 
            while (true)
            {
                for (int i = 0; i < 8; i++)
                {
                    display.text = colorNames.PickRandom();
                    display.color = colors.PickRandom();
                    yield return new WaitForSeconds(0.75f);
                }
            }
    }
}

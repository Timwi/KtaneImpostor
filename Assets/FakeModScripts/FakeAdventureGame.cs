using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeAdventureGame : ImpostorMod 
{
    [SerializeField] TextMesh enemy, stat, item;
    private int Case;

    static readonly string[] standardEnemies = { "DEMON", "DRAGON", "EAGLE", "GOBLIN", "GOLEM", "TROLL", "LIZARD", "WIZARD" };
    static readonly string[] standardItems = { "BALLOON", "BATTERY", "BELLOWS", "CHEAT CODE", "CRYSTAL BALL", "FEATHER ", "HARD DRIVE", "LAMP", "MOONSTONE", "POTION", "SMALL DOG", "STEPLADDER", "SUNSTONE", "SYMBOL", "TICKET", "TROPHY", "BROADSWORD", "CABER", "NASTY KNIFE", "LONGBOW", "MAGIC ORB", "GRIMOIRE" };
    
    static readonly string[] funnyEnemies = { "JOHN CENA", "DEAF", "BLAN", "EXISH", "ANTON", "UNITY", "15L H₂SO₄", "JACK BLACK", "TIMWI", "ELMO", "THE POPE", "MATPAT", "SANS", "GAMING", "NEW YORK", "EL PASO, TX", "MITT ROMNEY", "CLEMP", "SMILFS", "MCNUGGET", "DICEY", "ALBANIA", "CENTURION", "RHOJUS", "LEMON" };
    static readonly string[] funnyStats = { "{0} SWG", "{0} mSv", "{0}{1} radians", "{0} EYES", "${0}.{1}{2}", "{0}{1}% ABV", "0.{0}{1} BAC", "{0} RETWEETS", "{0} FBI", "5{1} SHADES", "T-{0} SECONDS", "{0} LUST", "{0}{1} BMI", "0 BITCHES", "{1}.{0}{2} ETH", "{1} DABLOONS", "{0}0 CENT", "{0}:1 RATIO", "{0}{1}{2} CAL", "{0} KROMER",  };
    static readonly string[] funnyItems = { "GUN", "STEPBROTHER", "HOLY WATER", "LARGE DOG", "BRICK", "TWIX BAR", "BOMB", "M249", "BOSNIAN FLAG", "SUBWAY WINGS", "BIG EGG", "FAT MAN", "PESTILENCE", "REDDIT GOLD", "CLEAN KNIFE",  };
    void Start()
    {
        enemy.text = standardEnemies.PickRandom();
        stat.text = Rnd.Range(1, 11) + " STR";
        item.text = standardItems.PickRandom();
        Case = Rnd.Range(0, 3);

        switch (Case)
        {
            case 0:
                enemy.text = funnyEnemies.PickRandom();
                LogQuirk("the enemy is {0}", enemy.text);
                flickerObjs.Add(enemy.gameObject);
                break;
            case 1:
                stat.text = string.Format(funnyStats.PickRandom(), Rnd.Range(1, 10), Rnd.Range(0, 10), Rnd.Range(0, 10));
                LogQuirk("the displayed statistic is {0}", stat.text);
                flickerObjs.Add(stat.gameObject);
                break;
            case 2:
                item.text = funnyItems.PickRandom();
                LogQuirk("the displayed item is {0}", item.text);
                flickerObjs.Add(item.gameObject);
                break;
        }
    }
}

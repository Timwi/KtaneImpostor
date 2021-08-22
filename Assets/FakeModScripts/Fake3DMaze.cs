﻿using UnityEngine;
using Rnd = UnityEngine.Random;

public class Fake3DMaze : ImpostorMod 
{
    [SerializeField]
    private MeshRenderer[] walls;
    [SerializeField]
    private MeshRenderer bottomLetter;
    [SerializeField]
    private Texture[] letters;
    void Start()
    {
        foreach (MeshRenderer rend in walls)
            rend.enabled = Rnd.Range(0, 2) == 0;
        bottomLetter.material.mainTexture = letters.PickRandom();
        flickerObjs.Add(bottomLetter.gameObject);
        Log("the letter on the bottom of the module is a " + bottomLetter.material.name[0]);
    }
}
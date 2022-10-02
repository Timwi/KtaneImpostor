using UnityEngine;

public class Fake3DMaze : ImpostorMod 
{
    public override string ModAbbreviation { get { return "3dmz"; } }
    public MeshRenderer[] walls;
    public MeshRenderer bottomLetter;
    public Texture[] letters;
    void Start()
    {
        foreach (MeshRenderer rend in walls)
            rend.enabled = Ut.RandBool();
        bottomLetter.material.mainTexture = letters.PickRandom();
        flickerObjs.Add(bottomLetter.gameObject);
        LogQuirk("the letter on the bottom of the module is a {0}", bottomLetter.material.name[0]);
    }
}
using System.Collections.Generic;
using UnityEngine;

public enum SLPositions
{
    TR,
    TL,
    BL,
    BR,
    Missing
}
public static class SLDict
{
    public static Dictionary<SLPositions, Vector3> StatusPositions = new Dictionary<SLPositions, Vector3>()
    {
        { SLPositions.TR, new Vector3(0.075167f, 0.01986f, 0.076057f) },
        { SLPositions.TL, new Vector3(-0.075167f, 0.01986f, 0.076057f) },
        { SLPositions.BL, new Vector3(-0.075167f, 0.01986f, -0.076057f) },
        { SLPositions.BR, new Vector3(0.075167f, 0.01986f, -0.076057f) },
        { SLPositions.Missing, new Vector3(0.075167f, -0.03f, 0.076057f) },
    };
}

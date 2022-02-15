using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData {

	public string missionID { get; private set; }
	public string[] availableMods { get; private set; }

    public MissionData(string missionID, params string[] availableMods)
    {
        this.missionID = missionID;
        this.availableMods = availableMods;
    }

    public static readonly List<MissionData> allMissions = new List<MissionData>()
    {
        new MissionData("mod_how123_impostorous", "Anagrams", "Bitmaps", "Connection Check", "Cruel Piano Keys", "Festive Piano Keys", "Letter Keys", "Murder", "Colour Flash", "Piano Keys", "Semaphore", "Switches", "Word Scramble", "Morsematics"),
    };
}

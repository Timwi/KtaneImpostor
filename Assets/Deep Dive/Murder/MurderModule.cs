using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class MurderModule : MonoBehaviour
{
	public KMBombInfo BombInfo;
	public KMSelectable[] buttons;
	public TextMesh[] Display;

	string[,] names = new string[3,9] {{"Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White", "-", "-", "-"},
									   {"Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", "-", "-", "-"},
									   {"Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library"} };
	int[,] locationTable = new int[9,6] {{0,8,3,2,1,5}, {1,7,4,3,2,8}, {2,4,6,8,5,0}, {3,6,0,5,7,2}, {4,2,1,6,0,7}, {5,3,8,1,4,6}, {6,5,2,7,8,1}, {7,1,5,0,3,4}, {8,0,7,4,6,3}};
	int[] solution = new int[3];
	int[,] skipDisplay = new int[2,6];
	int[] displayVal = new int[3] {0, 0, 0};
	int[,] potentialSolution = new int[4, 3];
	int numOfSolutions = 0;
	int suspects = 6;
	int weapons = 6;
	int bodyFound;

	Color[] colours = {
		new Color (1f, 0.05f, 0.05f),
		new Color (0.8f, 0.15f, 0.55f), 
		new Color (0.3f, 0.3f, 1f), 
		new Color (0.1f, 0.8f, 0.1f), 
		new Color (0.85f, 0.85f, 0.15f),
		new Color (1f, 1f, 1f),
		new Color (0.8f, 0.8f, 0.8f)
	};

	bool isActivated = false;
	bool isSolved = false;
	bool ohDear = false;

    void Start() {
        Init();
        GetComponent<KMBombModule>().OnActivate += ActivateModule;
    }

    void Init() {
		buttons [0].OnInteract += delegate () {buttons[0].AddInteractionPunch(0.2f); ChangeDisplay (0, -1); return false; };
		buttons [1].OnInteract += delegate () {buttons[1].AddInteractionPunch(0.2f); ChangeDisplay(0, 1); return false; };
		buttons [2].OnInteract += delegate () {buttons[2].AddInteractionPunch(0.2f); ChangeDisplay(1, -1); return false; };
		buttons [3].OnInteract += delegate () {buttons[3].AddInteractionPunch(0.2f); ChangeDisplay(1, 1); return false; };
		buttons [4].OnInteract += delegate () {buttons[4].AddInteractionPunch(0.2f); ChangeDisplay(2, -1); return false; };
		buttons [5].OnInteract += delegate () {buttons[5].AddInteractionPunch(0.2f); ChangeDisplay(2, 1); return false; };
		buttons [6].OnInteract += delegate () {buttons[6].AddInteractionPunch(1); Accuse();	return false; };

		List<string> Response;

		int batteryCount = 0;
		int batteryDCount = 0;
		Response = BombInfo.QueryWidgets (KMBombInfo.QUERYKEY_GET_BATTERIES, null);
		foreach( string Value in Response ) {
			Dictionary< string,int > batteryInfo = JsonConvert.DeserializeObject <Dictionary<string,int>> (Value);
			batteryCount += batteryInfo ["numbatteries"];
			if (batteryInfo ["numbatteries"] == 1) {
				batteryDCount += 1;
			}
		}

		bool TRNon = false;
		bool noLit = true;
		bool FRQoff = false;
		Response = BombInfo.QueryWidgets (KMBombInfo.QUERYKEY_GET_INDICATOR, null);
		foreach( string Value in Response ) {
			Dictionary< string,string > IndicatorInfo = JsonConvert.DeserializeObject< Dictionary< string,string >> (Value);
			if (IndicatorInfo ["on"] == "True") {
				noLit = false;
				if (IndicatorInfo ["label"] == "TRN") {
					TRNon = true;
				}
			} else if (IndicatorInfo ["on"] == "False") {
				if (IndicatorInfo ["label"] == "FRQ") {
					FRQoff = true;
				}
			}
		}

		bool serialPort = false;
		int RCAPortCount = 0;
		Response = BombInfo.QueryWidgets (KMBombInfo.QUERYKEY_GET_PORTS, null);
		foreach( string Value in Response ) {
			Dictionary< string, List<string> > PortInfo = JsonConvert.DeserializeObject< Dictionary< string, List<string> >> (Value);
			foreach( string PortType in PortInfo["presentPorts"] ) {
				if (PortType == "Serial") {
					serialPort = true;
				} else if (PortType == "StereoRCA") {
					RCAPortCount++;
				}
			}
		}

		bodyFound = Random.Range (0, 9);

		Debug.Log("[Murder] Number of batteries: " + batteryCount);
		Debug.Log("[Murder] Number of D batteries: " + batteryDCount);
		Debug.Log("[Murder] Has lit indicators: " + (noLit ? "No" : "Yes"));
		Debug.Log("[Murder] Has lit TRN indicator: " + (TRNon ? "Yes" : "No"));
		Debug.Log("[Murder] Has unlit FRQ indicator: " + (FRQoff ? "Yes" : "No"));
		Debug.Log("[Murder] Has serial port: " + (serialPort ? "Yes" : "No"));
		Debug.Log("[Murder] Number of RCA ports: " + RCAPortCount);
		Debug.Log("[Murder] Body found in " + names[2,bodyFound]);

		int suspectRow;
		int weaponRow;
		if (TRNon) {
			suspectRow = 4;
		} else if (bodyFound == 0) {
			suspectRow = 6;
		} else if (RCAPortCount > 1) {
			suspectRow = 7;
		} else if (batteryDCount == 0) {
			suspectRow = 1;
		} else if (bodyFound == 1) {
			suspectRow = 3;
		} else if (batteryCount > 4) {
			suspectRow = 8;
		} else if (FRQoff) {
			suspectRow = 0;
		} else if (bodyFound == 5) {
			suspectRow = 2;
		} else {
			suspectRow = 5;
		}
		if (bodyFound == 3) {
			weaponRow = 2;
		} else if (batteryCount > 4) {
			weaponRow = 0;
		} else if (serialPort) {
			weaponRow = 8;
		} else if (bodyFound == 4) {
			weaponRow = 3;
		} else if (batteryCount == 0) {
			weaponRow = 5;
		} else if (noLit) {
			weaponRow = 4;
		} else if (bodyFound == 7) {
			weaponRow = 6;
		} else if (RCAPortCount > 1) {
			weaponRow = 1;
		} else {
			weaponRow = 7;
		}

		Debug.Log("[Murder] Suspect row: " + (suspectRow + 1));
		Debug.Log("[Murder] Weapon row: " + (weaponRow + 1));

		if(suspectRow == weaponRow) {
			// This shouldn't happen
			ohDear = true;
			Display[0].text = "ERROR";
			Display[1].text = "ERROR";
			Display[2].text = "ERROR";
			GetComponent<KMBombModule> ().HandlePass ();
			isSolved = true;
		}

		Debug.Log("[Murder] Potential solutions:");

		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 6; j++) {
				if (locationTable [suspectRow, i] == locationTable [weaponRow, j]) {
					potentialSolution [numOfSolutions, 0] = i;
					potentialSolution [numOfSolutions, 1] = j;
					potentialSolution [numOfSolutions, 2] = locationTable [suspectRow, i];
					numOfSolutions++;
					Debug.Log("[Murder] " + names[0,i] + " with the " + names[1,j] + " in the " + names[2,locationTable[suspectRow,i]]);
				}
			}
		}

		int r = Random.Range (0, numOfSolutions);
		solution [0] = potentialSolution [r, 0];
		solution [1] = potentialSolution [r, 1];
		solution [2] = potentialSolution [r, 2];

		Debug.Log("[Murder] Actual solution: " + names[0,solution[0]] + " with the " + names[1,solution[1]] + " in the " + names[2,solution[2]]);

		for(int i = 0; i < numOfSolutions; i++) {
			if(i != r) {
				if(suspects > 4 && weapons > 4) {
					int r2 = Random.Range (0,2);
					if (r2 == 0) {
						skipDisplay[0,potentialSolution[i,0]] = 1;
						suspects--;
						Debug.Log("[Murder] Eliminating " + names[0,potentialSolution[i,0]] + " to remove a potential solution.");
					} else {
						skipDisplay[1,potentialSolution[i,1]] = 1;
						weapons--;
						Debug.Log("[Murder] Eliminating " + names[1,potentialSolution[i,1]] + " to remove a potential solution.");
					}
				} else if (suspects > 4) {
					skipDisplay[0,potentialSolution[i,0]] = 1;
					suspects--;
					Debug.Log("[Murder] Eliminating " + names[0,potentialSolution[i,0]] + " to remove a potential solution.");
				} else {
					skipDisplay[1,potentialSolution[i,1]] = 1;
					weapons--;
					Debug.Log("[Murder] Eliminating " + names[1,potentialSolution[i,1]] + " to remove a potential solution.");
				}
			}
		}
		while (suspects > 4) {
			int r2 = Random.Range (0, 6);
			if (r2 != solution [0] && skipDisplay [0, r2] == 0) {
				skipDisplay [0, r2] = 1;
				suspects--;
				Debug.Log("[Murder] Eliminating " + names[0,r2] + " to reduce to 4 suspects.");
			}
		}
		while (weapons > 4) {
			int r2 = Random.Range (0, 6);
			if (r2 != solution [1] && skipDisplay [1, r2] == 0) {
				skipDisplay [1, r2] = 1;
				weapons--;
				Debug.Log("[Murder] Eliminating " + names[1,r2] + " to reduce to 4 weapons.");
			}
		}
	}

    void ActivateModule() {
		if (!ohDear) {
			isActivated = true;
			displayVal [0] = Random.Range (0, 6);
			displayVal [1] = Random.Range (0, 6);
			displayVal [2] = Random.Range (0, 9);
			for (int i = 0; i < 3; i++) {
				if (i < 2) {
					while (skipDisplay [i, displayVal [i]] == 1) {
						displayVal [i]++;
						displayVal [i] = (displayVal [i] + 6) % 6;
					}
				}
				Display [i].text = names [i, displayVal [i]];
				if (i == 0) {
					Display [i].color = colours [displayVal [i]];
				} else if (i == 2 && displayVal [i] == bodyFound) {
					Display [i].color = colours [0];
				} else {
					Display [i].color = colours [6];
				}
			}
		}
    }

	void ChangeDisplay(int disp, int change) {
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (isActivated && !isSolved && !ohDear) {
			if (disp < 2) {
				displayVal [disp] += change;
				displayVal [disp] = (displayVal [disp] + 6) % 6;
				while (skipDisplay [disp, displayVal [disp]] == 1) {
					displayVal [disp] += change;
					displayVal [disp] = (displayVal [disp] + 6) % 6;
				}
			} else {
				displayVal [disp] += change;
				displayVal [disp] = (displayVal [disp] + 9) % 9;
			}
			Display [disp].text = names[disp, displayVal [disp]];

			if (disp == 0) {
				Display [disp].color = colours [displayVal [disp]];
			} else if(disp == 2 && displayVal[disp] == bodyFound) {
				Display [disp].color = colours [0];
			} else {
				Display [disp].color = colours [6];
			}
		}
	}

	void Accuse() {
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (!isActivated) {
			GetComponent<KMBombModule>().HandleStrike();
		} else if (!isSolved) {
			if (displayVal [0] == solution [0] && displayVal [1] == solution [1] && displayVal [2] == solution [2]) {
				GetComponent<KMBombModule> ().HandlePass ();
				isSolved = true;
			} else {
				GetComponent<KMBombModule>().HandleStrike();
			}
		}
	}
}

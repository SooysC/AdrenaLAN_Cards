using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobals {
	public static bool done = true;
	public static Dictionary <string, string[]> Attack_Map;

	public static void createGameActions () {
		Attack_Map = new Dictionary<string, string[]>();
		Attack_Map.Add("Deer", new string[] {"Hill", "House"});
		Attack_Map.Add("Farmer", new string[] {"Bear", "Wolf"});
		Attack_Map.Add("Fox", new string[] {"Hare", "Squirrel"});
		Attack_Map.Add("Boar", new string[] {"Forest", "CaveT"});
		Attack_Map.Add("Hare", new string[] {"Field", "Farm"});
		Attack_Map.Add("Wolf", new string[] {"Deer", "Boar"});
		Attack_Map.Add("Bear", new string[] {"Sphinx", "Fox"});
		Attack_Map.Add("Squirrel", new string[] {"Desert", "Farmer"});
	}
}

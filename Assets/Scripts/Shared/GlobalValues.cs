//
//	Global values and definitions
//

using Realm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Shared { 
	static public class GlobalValues { 

		// current map for editing or play
		static public LevelMap currentMap;

		// Offscreen Location of preloaded tiles.
		static public readonly float TILE_X_LOC = 1000f;
		static public readonly float TILE_Y_LOC = 1000f;
	}
}


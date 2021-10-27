
namespace Realm {

	using Realm.Enums;
	using Realm.Puzzle;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class RealmAnalyzer {

		/// <summary>
		/// A map must have at least one entrance, one chest, and a path between the two.
		/// </summary>
		/// <param name="map"></param>
		/// <returns>null for success, message for failure</returns>
		static public string Validate( PuzzleMap map ) {

			int doorCount=0,chestCount=0;

			var loop = map.GetPlaceLoop();
			foreach ( var spot in loop ) {

				if (spot.Flag==FlagEnum.Door) doorCount++;
				if (spot.Flag==FlagEnum.Chest) chestCount++;

			}

			if (doorCount == 0) return "No doors for map entry";
			if (chestCount == 0) return "No chests for map completion";

			return null;
		}
	}
}

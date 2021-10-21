using Realm.Game;
using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Brain {

	public interface BrainIF {

		/// <summary>
		/// A brain will select actions based on a map and faction.
		/// </summary>
		/// <param name="who"></param>
		/// <param name="puzzle"></param>
		/// <returns></returns>
		ActionChain Next( int faction, PuzzleMap puzzle );
	}

}

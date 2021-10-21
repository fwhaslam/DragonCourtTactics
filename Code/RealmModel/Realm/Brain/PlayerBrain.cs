using Realm.Game;
using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Brain {

	/// <summary>
	/// Wrapper for player taking turns for the player faction.
	/// This will 'wait' for player action events, then pass them up.
	/// hmmm .... not sure this is a good way to handle it ...
	public class PlayerBrain : BrainIF {
		
		/// <summary>
		/// A brain will select actions based on a map and agent.
		/// </summary>
		/// <param name="who"></param>
		/// <param name="puzzle"></param>
		/// <returns></returns>
		public ActionChain Next( int faction, PuzzleMap puzzle ) {
			return null;
		}
	}
}

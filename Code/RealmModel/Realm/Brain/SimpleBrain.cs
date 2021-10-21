using Realm.Game;
using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Brain {

	/// <summary>
	/// Move directly towards enemies and strike when possible.
	/// Weakest act first.
	/// </summary>
	public class SimpleBrain : BrainIF {
		
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

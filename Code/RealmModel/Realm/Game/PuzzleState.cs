

namespace Realm.Game {

	using Realm.Puzzle;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class PuzzleState {

		public PuzzleState() { }

		public PuzzleState( PuzzleMap map ) { Puzzle = map; }


		public PuzzleState( ActionChain action, PuzzleMap map ) { 
			this.Action = action;
			this.Puzzle = map; 
		}

		/// <summary>
		/// The action that created this state from the previous state.
		/// </summary>
		public ActionChain Action { get; set; }
		
		public PuzzleMap Puzzle { get; set; }

	}
}

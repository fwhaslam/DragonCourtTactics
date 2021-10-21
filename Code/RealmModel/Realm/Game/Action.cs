using Realm.Puzzle;

using Realm.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realm.Tools;

namespace Realm.Game {
	

	/// <summary>
	/// An action creates a change, which results in a new puzzle state.
	/// </summary>
	public class Action {

		// what change is taking place
		public ActionType Type {  get; set; }

		// where the action takes effect, usually an adjacent space, sometimes same place, sometimes a distant place
		public Where Look { get; set; }
	}
}

using Realm.Puzzle;

using Realm.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Game {
	

	/// <summary>
	/// An agent can take multiple sequential actions in his turn.
	/// We are calling this an action chain.
	/// </summary>
	public class ActionChain {

		public Agent actor { get; set; }

		public List<Action> Actions {  get; set; }

	}
}

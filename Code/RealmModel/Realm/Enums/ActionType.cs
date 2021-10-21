using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Enums {
	public enum ActionType {

		None,		
		EndFaction,	// the current faction declares it is done taking actions, end of a 'docket'
		EndGame,	// victory conditions achieved, end of game

		Move,		// agent changes place
		Turn,		// agent changes facing
		Strike,		// agent attacks another agent

		Actuate,	// pull a lever, open a door, etc.
		Defend,		// take a defensive stance / block
	}
}

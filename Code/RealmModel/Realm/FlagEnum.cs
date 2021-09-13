using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm {

	/// <summary>
	/// Valid Values for Flag layer
	/// </summary>
	public enum FlagEnum {
		None,
		Entry,		// heros start here
		Exit,		// goal, all heros must exit
		Door,		// blocks until 'lockpick' trait opens
		Spikes,		// damages those who enter
		Chest,		// goal, collect chest
		Princess,	// goal, grab princess, carry to exit
		Prisoner	// goal, release peasant, escort to exit
	}

	
}

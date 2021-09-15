//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	/// <summary>
	/// Valid Values for Flag layer
	/// </summary>
	public enum FlagEnum {
		None,

		Entry,		// heros start here
		Door,		// blocks until 'locks' trait opens
		Chest,		// goal, collect chest
		Princess,	// goal, release princess, escort to exit

		Lever,		// trigger
		Gears,		// hidden, will extend trigger to mechanisms
		Pitfall,	// mechanism, switch from floor to pit
		Hatch,		// mechanism, switch to remove/restore wall
		Bridge,		// mechanism, switch between bridge and pit (? dupe of pitfall?)
		Spikes,		// mechanism, spikes raise damaging those who enter
	}

	
}

//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	/// <summary>
	/// Valid Values for Flag layer
	/// </summary>
	public enum FlagEnum {

		None,

		Entry,		// heros start here
		Door,		// blocks until 'locks' trait opens
		Hostage,	// primary goal, all primaries = game over
		Chest,		// primary goal, all primaries = game over
		Sack,		// secondary goal, extra points

		Lever,		// trigger, voluntary
		Switch,		// trigger, involuntary
		Gears,		// hidden, will extend trigger to mechanisms
		Pitfall,	// mechanism, switch from floor to pit
		Masher,		// mechanism, switch from floor to wall

		Lower,		// mechanism, floor lowers one
		Raise,		// mechanism, floor raises one
		Spikes,		// mechanism, spikes from floor damaging those who enter
		Arrows,
		Boulder
	}

	
}

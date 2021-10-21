//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using System;

	public class AgentTrait {

		public AgentTrait(string k, string i) {
			Key = k;
			Info = i;
		}

		public string Key { get; internal set; }
		public string Info { get; internal set; }

		//======================================================================================================================

		// morale attributes
		static public readonly AgentTrait COWARD =
			new AgentTrait("Coward", "Will not step in front of enemies.");

		static public readonly AgentTrait BRAVE =
			new AgentTrait("Brave", "Not affected by Scary.");

		static public readonly AgentTrait SCARY =
			new AgentTrait("Scary", "Enemies fear to approach.");

		static public readonly AgentTrait HOLY =
			new AgentTrait("Holy", "Will scare the undead.");

		static public readonly AgentTrait UNDEAD =
			new AgentTrait("Undead", "Only fears the holy.");

		// health attributes
		static public readonly AgentTrait STURDY =
			new AgentTrait("Sturday", "Has greater health.");
		static public readonly AgentTrait FRAIL =
			new AgentTrait("Frail", "Has lower health.");

		// damage attributes
		static public readonly AgentTrait WEAK =
			new AgentTrait("Weak", "Does less damage.");

		// movement
		static public readonly AgentTrait SLOW =
			new AgentTrait("Slow", "Has less movement.");

		static public readonly AgentTrait FAST =
			new AgentTrait("Fast", "Has more movement.");

		static public readonly AgentTrait SHORT =
			new AgentTrait("Short", "Can hide in rubble.");

		static public readonly AgentTrait TALL =
			new AgentTrait("Short", "Can climb two steps at a time.");

		static public readonly AgentTrait HOVER =
			new AgentTrait("Hover", "Can cross pits.  Cannot trigger switches.");

		static public readonly AgentTrait Fly =
			new AgentTrait("Fly", "Can climb all steps at once, and move over enemies.");


		// status attributes
		static public readonly AgentTrait SCREAMS =
			new AgentTrait("Screams", "When active, this will activate all allies at arrow range.");


		// combat attributes
		static public readonly AgentTrait SHIELD =
			new AgentTrait("Shield", "Attacks from front are reduced by one");

		static public readonly AgentTrait SACRED =
			new AgentTrait("Sacred", "Cause extra injury to the undead.");

		static public readonly AgentTrait MYSTIC =
			new AgentTrait("Mystic", "Cannot be harmed by normal weapons.");

		static public readonly AgentTrait STRONG =
			new AgentTrait("Strong", "Cause extra injury to enemies.");

		static public readonly AgentTrait RANGED =
			new AgentTrait("Ranged", "Can attack at arrow range.");

	}
}

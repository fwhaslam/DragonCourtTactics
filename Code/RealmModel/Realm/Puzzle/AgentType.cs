//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using System;
	using System.Linq;
	using System.Collections.Generic;


	using static Realm.Puzzle.AgentTrait;
	using YamlDotNet.Serialization;

	public class AgentType {

		static internal Dictionary<string, AgentType> registry = new Dictionary<string, AgentType>();
		static internal List<AgentType> list = new List<AgentType>();

		//======================================================================================================================

		// Tier Zero
		static public readonly AgentType PEASANT =
			MakeAgentType("Peasant", FRAIL, WEAK);

		static public readonly AgentType GOBLIN =
			MakeAgentType("Goblin", COWARD, SHORT, WEAK);

		static public readonly AgentType SKELETON =
			MakeAgentType("Skeleton", FRAIL, SLOW, WEAK, UNDEAD);

		// Tier One
		static public readonly AgentType GHOST =
			MakeAgentType("Ghost", HOVER, SLOW, WEAK, MYSTIC);

		//======================================================================================================================

		/// <summary>
		/// Create and register an agent
		/// </summary>
		/// <param name="at"></param>
		/// <returns></returns>
		static internal AgentType MakeAgentType(string n, params AgentTrait[] at) {

			AgentType make = new AgentType();
			make.Name = n;
			make.Index = list.Count;
			make.Traits = new HashSet<AgentTrait>(at);

			registry[make.Name] = make;
			list.Add(make);
			return make;
		}

		static public int Count() { return list.Count; }
		static public AgentType Get(int ix) { return list[ix]; }
		static public AgentType Get(string key) { return registry[key]; }

		/// <summary>
		/// Return a copy of the registry keys.
		/// </summary>
		/// <returns></returns>
		static public List<string> Keys() {
			return new List<string>(registry.Keys);
		}

		//======================================================================================================================

		// Descriptor
		public string Name { get; internal set; }

		// Traits, binary values associated to agent.
		public HashSet<AgentTrait> Traits { get; internal set; }

		// position in type list
		[YamlIgnore]
		public int Index { get; internal set; }

		/// <summary>
		/// Does this AgentType have this trait?
		/// </summary>
		/// <param name="trait"></param>
		/// <returns></returns>
		public bool Has(AgentTrait trait) {
			return Traits.Contains(trait);
		}

		//======================================================================================================================

		// Movement
		public int Steps {
			get {
				int num = 5;
				if (Has(FAST)) num += 3;
				if (Has(SLOW)) num -= 2;
				return num;
			}
		}

		// Health, reduced by enemy damage
		public int Health {
			get {
				int num = 5;
				if (Has(STURDY)) num += 3;
				if (Has(FRAIL)) num -= 2;
				return num;
			}
		}

		// Damage, reduces enemy health
		public int Damage {
			get {
				int num = 2;
				if (Has(STRONG)) num += 2;
				if (Has(WEAK)) num -= 1;
				return num;
			}
		}

		// How much attack damage is reduced from enemies
		public int Armor {
			get {
				int num = 0;
				return num;
			}
		}

		// How far away can this agent attack?
		// Expressed in steps.
		public int Range {
			get {
				int num = 3;
				return num;
			}
		}

		//======================================================================================================================

		/// <summary>
		/// Is this agent afraid of that agent?
		/// </summary>
		/// <param name="what"></param>
		/// <returns></returns>
		public bool AfraidOf(AgentType who) {

			if (Has(COWARD)) return true;

			// Brave and Undead not afraid of Scary
			if (who.Has(SCARY)) {
				if (!Has(BRAVE) && !Has(UNDEAD)) return true;
			}

			// Undead afraid of Holy
			if (who.Has(HOLY)) {
				if (Has(UNDEAD)) return true;
			}

			return false;

		}

	}
}

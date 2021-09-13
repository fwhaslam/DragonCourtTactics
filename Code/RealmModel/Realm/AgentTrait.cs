
namespace Realm {

	using System;

	public class AgentTrait {

		public AgentTrait(string k,string i) {
			this.Key = k;
			this.Info = i;
		}

		public string Key { get; internal set; }
		public string Info { get; internal set; }

//======================================================================================================================

		static public readonly AgentTrait COWARD = 
			new AgentTrait("Coward","Will step next to enemies.");

		static public readonly AgentTrait SHIELD = 
			new AgentTrait("Shield","Attacks from front are reduced by one");

		static public readonly AgentTrait ARMOR = 
			new AgentTrait("Armor","All attacks are reduced by one.");

		static public readonly AgentTrait TOUGH = 
			new AgentTrait("Tough","All attacks are reduced by one.");

	}
}

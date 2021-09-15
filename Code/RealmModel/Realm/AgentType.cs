//
//	Copyright 2021 Frederick William Haslam born 1962
//


namespace Realm {

	using System;
	using System.Linq;
	using System.Collections.Generic;


using static Realm.AgentTrait;

	public class AgentType {

		static internal Dictionary<string,AgentType> registry = new Dictionary<string,AgentType>();
		static internal List<AgentType> list = new List<AgentType>();

		// heros
		static public readonly AgentType PEASANT = 
			MakeAgentType("Peasant",5,5,2,0,1,COWARD);

		static public readonly AgentType SOLDIER = 
			MakeAgentType("Soldier",5,5,3,0,1);

		static public readonly AgentType GUARD = 
			MakeAgentType("Guard",5,5,3,0,1,SHIELD);

		// villains
		static public readonly AgentType GOBLIN = 
			MakeAgentType("Goblin",5,5,2,0,1);

		static public readonly AgentType ORC = 
			MakeAgentType("Orc",5,5,3,0,1);

		/// <summary>
		/// Create and register an agent
		/// </summary>
		/// <param name="n"></param>
		/// <param name="s"></param>
		/// <param name="h"></param>
		/// <param name="d"></param>
		/// <param name="a"></param>
		/// <param name="r"></param>
		/// <param name="at"></param>
		/// <returns></returns>
		static internal AgentType MakeAgentType(String n,int s,int h,int d,int a,int r,params AgentTrait[] at) {

			AgentType make = new AgentType();
			make.Name = n;
			make.Index = list.Count;
			make.Steps = s;
			make.Health = h;
			make.Damage = d;
			make.Armor = a;
			make.Range = r;
			make.Traits = new HashSet<AgentTrait>( at );

			registry[ make.Name ] = make ;
			list.Add( make );
			return make;
		}

		static public int Count() { return list.Count; }
		static public AgentType Get(int ix) { return list[ix]; }

		/// <summary>
		/// Options are all the types, plus 'none' for no agent.
		/// </summary>
		/// <returns></returns>
		static public List<string> GetOptions() {
			var list = new List<string>();
			list.Add( "None" );
			foreach ( object key in registry.Keys ) list.Add( key.ToString() );
			return list;
		}

		// Descriptor
		public string Name { get; internal set; }

		// position in type list
		public int Index { get; internal set; }

		// Movement
		public int Steps { get; internal set; }

		// Health, reduced by enemy damage
		public int Health { get; internal set; }

		// Damage, reduces enemy health
		public int Damage { get; internal set; }

		// How much attack damage is reduced from enemies
		public int Armor { get; internal set; }

		// How far away can this agent attack?
		public int Range { get; internal set; }

		// Traits, binary values associated to agent.
		public HashSet<AgentTrait> Traits { get; internal set; }

	}
}

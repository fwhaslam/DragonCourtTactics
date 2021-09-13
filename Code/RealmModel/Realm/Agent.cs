
namespace Realm {

	using System;

	public class Agent {
 
		/// <summary>
		/// Location in Agent List.  ID for map.
		/// </summary>
		public int Index {  get; set; }

		public AgentType Type {  get; set; }

		public DirEnum Face { get; set; }

		public int X { get; set; }

		public int Y { get; set; }

	}
}

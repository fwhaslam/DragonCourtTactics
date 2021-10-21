//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {
	using Realm.Enums;
	using Realm.Tools;

	using YamlDotNet.Serialization;

	public class Agent {

		/// <summary>
		/// Provided to support YAML.
		/// </summary>
		public Agent() { }

		public Agent(int x, int y) {
			Where = new Where(x, y);
		}
		public Agent(Where loc) {
			Where = new Where(loc);
		}

		public Agent(Agent src) {
			Where = src.Where;
			Type = src.Type;
			Face = src.Face;
			Faction = src.Faction;
		}

		/// <summary>
		/// Type is summarized to AgentType.Name
		/// </summary>
		[YamlIgnore]
		public AgentType Type { get; set; } = AgentType.PEASANT;

		public string Name {
			get => Type.Name;
			set => Type = AgentType.Get(value);
		}

		public DirEnum Face { get; set; } = DirEnum.North;

		[YamlIgnore]
		public Where Where { get; set; }

		public StatusEnum Status { get; set; } = StatusEnum.Alert;

		public int Faction { get; set; } = 0;
	}
}

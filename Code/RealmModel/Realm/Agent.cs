//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	using Realm.Enums;
	using Realm.Tools;

	using YamlDotNet.Serialization;

	public class Agent {
 
		public Agent(int  x,int y) {
			Where = new Where(x,y);
		}
		public Agent(Where loc) {
			Where = new Where(loc);
		}

		public Agent( Agent src ) {
			this.Where = src.Where;
			this.Type = src.Type;
			this.Face = src.Face;
			this.Faction = src.Faction;
		}

		/// <summary>
		/// Type is summarized to AgentType.Name
		/// </summary>
		[YamlIgnore]
		public AgentType Type {  get; set; } = AgentType.PEASANT;

		public string Name { get => Type.Name; }

		public DirEnum Face { get; set; } = DirEnum.North;

		[YamlIgnore]
		public Where Where { get; set; }

		public StatusEnum Status {  get; set; } = StatusEnum.Alert;

		public int Faction {  get; set; } = 0;
	}
}

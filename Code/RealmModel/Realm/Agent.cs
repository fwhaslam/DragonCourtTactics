//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	using Realm.Enums;
	using Realm.Tools;

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
		}

		public AgentType Type {  get; set; }

		public DirEnum Face { get; set; }

		public Where Where { get; set; }


	}
}

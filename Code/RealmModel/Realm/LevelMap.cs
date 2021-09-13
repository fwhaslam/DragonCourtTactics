using System.Collections;
using System.Collections.Generic;

namespace Realm {

	/// <summary>
	/// Representation of the play region.  Always a rectangle.
	/// Each tile is a square, has height, type, and sometimes an item.
	/// Items are Agents and Objects.  Agents are Heros and Villains.
	/// Objects are interactive terrain.
	/// </summary>
	public class LevelMap {

		static public int NO_AGENT = 0;

		internal LevelMap() { }

		static public LevelMap Allocate( int w, int t ) {

			var work = new LevelMap();
			
			work.Wide = w;
			work.Tall = t;

			work.HeightLayer = new HeightEnum[w,t];
			work.FlagLayer = new FlagEnum[w,t];
			work.AgentLayer = new int[w,t];

			// first agent position is empty, so 'zero' means no agent
			work.Agents= new List<Agent>();
			work.Agents.Add( null );	

			return work;
		}

		public int Wide { get; internal set; }

		public int Tall { get; internal set; }

		// height
		public HeightEnum[,] HeightLayer { get; internal set; }

		// flags
		public FlagEnum[,] FlagLayer { get; internal set; }

		// index into Agents
		public int[,] AgentLayer { get; internal set; }

		public List<Agent> Agents { get; internal set; }

		/// <summary>
		/// Create an agent on the map.
		/// </summary>
		public void AddAgent( AgentType type, int x, int y, DirEnum face ) {

			int id = Agents.Count;

			Agent agent = new Agent();
			agent.Type = type;
			agent.X = x;
			agent.Y = y;
			agent.Face = face;
			agent.Index = id;

			Agents.Add( agent );
			AgentLayer[ x, y ] = id;
		}

		public void DropAgent( Agent who ) {
			int id = who.Index;
			Agents.Insert( id, null );
			AgentLayer[ who.X, who.Y ] = NO_AGENT;
		}

		public void AddFlag( FlagEnum type, int x, int y ) {
			FlagLayer[x,y] = type;
		}

		public void DropFlag( int x, int y ) {
			FlagLayer[x,y] = FlagEnum.None;
		}
	}

}
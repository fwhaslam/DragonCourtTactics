//
//	Copyright 2021 Frederick William Haslam born 1962
//


namespace Realm {

	using System;
	using System.Collections.Generic;
	using Realm.Enums;
	using Realm.Tools;
	using System.Linq;

	using static Realm.Tools.MapTools;

	/// <summary>
	/// Representation of the play region.  Always a rectangle.
	/// Each tile is a square, has height, type, and sometimes an item.
	/// Items are Agents and Objects.  Agents are Heros and Villains.
	/// Objects are interactive terrain.
	/// </summary>
	public class LevelMap {

		internal LevelMap() { }

		static public LevelMap Allocate( int w, int t ) {

			var work = new LevelMap();
			
			work.Wide = w;
			work.Tall = t;

			work.Places = Create2DArray<Place>( CreatePlace, w, t );

			work.Agents= new List<Agent>();

			return work;
		}

		static public Place CreatePlace(int x, int y, Place src) { 
			return new Place(x,y);
		}

//======================================================================================================================

		public int Wide { get; internal set; }

		public int Tall { get; internal set; }

		public Place[,] Places { get; internal set; }

		public List<Agent> Agents { get; internal set; }

		/// <summary>
		/// Create an agent on the map, removing any in the way.
		/// </summary>
		public void AddAgent( AgentType type, Where loc, DirEnum face ) {

			Place place = Places[loc.X,loc.Y];
			if (place.Agent!=null) {
				DropAgent( place.Agent );
			}

			place.Agent = new Agent( loc );
			place.Agent.Type = type;
			place.Agent.Face = face;

			Agents.Add( place.Agent );
		}

		public void DropAgent( Agent who ) {
			if (who!=null) {
				Agents.Remove(who);
				Places[ who.Where.X, who.Where.Y ] = null;
			}
		}

		public void AddFlag( FlagEnum type, int x, int y ) {
			Places[x,y].Flag = type;
		}

		public void DropFlag( int x, int y ) {
			Places[x,y].Flag = FlagEnum.None;
		}

		public void AddRow( DirEnum dir ) {

			Tuple<int,int> delta = DirEnumTraits.Delta(dir);
			if ( delta.Item1*delta.Item2 != 0 ) throw new ArgumentException( "Can only use orthogonal directions to add a row" );

			// old wide/tall
			int ow = Wide, ot = Tall;
			// new wide/tall
			int nw = ow, nt = ot;
			// shift in copy
			int sw = 0, st = 0;

			switch (dir) {
				case DirEnum.North:  // tall top
					nt++;
					break;			
				case DirEnum.South:  // tall zero
					nt++;
					st = 1;
					break;				
				case DirEnum.East:  // wide top
					nw++;
					break;
				case DirEnum.West:  // wide zero
					nw++;
					sw = 1;
					break;	
				default:
					throw new ArgumentException( "Can only use orthogonal directions to add a row" );
			}

			// copy old values into new layers
			LevelMap temp = LevelMap.Allocate( nw, nt );
			for ( int wx=0;wx<ow;wx++ ) {
				for (int tx=0;tx<ot;tx++) {
					temp.Places[ wx+sw, tx+st ] = Places[ wx, tx ];
				}
			}

			// copy over
			Wide = nw;
			Tall = nt;
			Places = temp.Places;
		}

		public void DropRow( DirEnum dir ) {

			Tuple<int,int> delta = DirEnumTraits.Delta(dir);
			if ( delta.Item1*delta.Item2 != 0 ) throw new ArgumentException( "Can only use orthogonal directions to add a row" );

			// old wide/tall
			int ow = Wide, ot = Tall;
			// new wide/tall
			int nw = ow, nt = ot;
			// shift in copy
			int sw = 0, st = 0;

			switch (dir) {
				case DirEnum.North:  // tall top
					nt--;
					break;			
				case DirEnum.South:  // tall zero
					nt--;
					st = 1;
					break;				
				case DirEnum.East:  // wide top
					nw--;
					break;
				case DirEnum.West:  // wide zero
					nw--;
					sw = 1;
					break;	
				default:
					throw new ArgumentException( "Can only use orthogonal directions to add a row" );
			}

			// copy old Places into new Places
			LevelMap temp = LevelMap.Allocate( nw, nt );
			for ( int wx=0;wx<nw;wx++ ) {
				for (int tx=0;tx<nt;tx++) {
					temp.Places[ wx, tx ] = Places[ wx-sw, tx-st ] ;
				}
			}

			// remove dropped agents
			foreach (var who in Agents.ToList()) { 
				who.Where.X -= sw;
				who.Where.Y -= st;
				// new spot is out of bounds
				if (who.Where.X>=nw || who.Where.Y>=nt || who.Where.X<0 || who.Where.Y<0 ) {
					Agents.Remove(who);
				}
			}
									
			// copy over
			Wide = nw;
			Tall = nt;
			Places = temp.Places;

		}

	}

}
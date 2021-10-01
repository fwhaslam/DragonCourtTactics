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
	using System.Text;
	using YamlDotNet.Serialization;

	/// <summary>
	/// Representation of the play region.  Always a rectangle.
	/// Each tile is a square, has height, type, and sometimes an item.
	/// Items are Agents and Objects.  Agents are Heros and Villains.
	/// Objects are interactive terrain.
	/// </summary>
	public class LevelMap {

		internal Dictionary<string,string> textMap = new Dictionary<string, string>();

		internal LevelMap() { }

		static public LevelMap Allocate( int w, int t ) {

			var work = new LevelMap();
			
			work.Title = "Empty Map";
			work.Image = "pic1.png";
			work.Text["Start"] = "Some Story";

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

		public string Title {  get; set; }

		public string Image { get; set; }

		public int Wide { get; internal set; }

		public int Tall { get; internal set; }
		
		/// <summary>
		/// Places become 'Map' for Yaml storage.
		/// </summary>
		[YamlIgnore]
		public Place[,] Places { get; internal set; }

		public List<string> Map { get => MapAsStrings(); set => StringsAsMap(value); }

		public List<Agent> Agents { get; internal set; }

		public Dictionary<string,string> Text {  get => textMap; set => textMap = value; }

//======================================================================================================================

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
					if (wx+sw<Wide || tx+st<Tall) {
						temp.Places[ wx+sw, tx+st ] = Places[ wx, tx ];
					}
				}
			}
			
			// shift some agents
			foreach (var who in Agents.ToList()) { 
				who.Where.X += sw;
				who.Where.Y += st;
Console.Out.WriteLine("AGENT AT ="+who.Where );
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
					st = -1;
					break;				
				case DirEnum.East:  // wide top
					nw--;
					break;
				case DirEnum.West:  // wide zero
					nw--;
					sw = -1;
					break;	
				default:
					throw new ArgumentException( "Can only use orthogonal directions to cut a row" );
			}

			// copy old Places into new Places
			LevelMap temp = LevelMap.Allocate( nw, nt );
Console.Out.WriteLine("   sw="+sw+"  st="+st );
			for ( int wx=0;wx<nw;wx++ ) {
				for (int tx=0;tx<nt;tx++) {
					if (wx-sw>=0 && tx-st>=0) { 
						temp.Places[ wx, tx ] = Places[ wx-sw, tx-st ] ;
					}
				}
			}

			// remove dropped agents
			foreach (var who in Agents.ToList()) { 
				who.Where.X += sw;
				who.Where.Y += st;
Console.Out.WriteLine("AGENT AT ="+who.Where );
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

//======================================================================================================================

		/// <summary>
		/// String representation of the map.
		/// </summary>
		/// <returns></returns>
		List<string> MapAsStrings() {
						
			StringBuilder buf = new StringBuilder();

			List<string> list = new List<string>();
			for (int dt=0;dt<Tall;dt++) {
				buf.Clear();
				for (int dw = 0; dw < Wide; dw++) {
					if (dw>0) buf.Append("/");

					Place place = Places[dw, dt];
					buf.Append( HeightEnumTraits.Symbol( place.Height ));
					buf.Append( FlagEnumTraits.Symbol( place.Flag ) );
					if (place.Agent==null) {
						buf.Append("__");
					}
					else { 
						int indexOf = Agents.IndexOf( place.Agent );
						buf.Append( indexOf.ToString("00"));
					}
				}
				list.Add( buf.ToString() );
			}
			return list;
		}
	
		/// <summary>
		/// Use strings to reconstruct map.
		/// </summary>
		/// <param name="source"></param>
		void StringsAsMap( List<string> source ) {

		}


	}
}
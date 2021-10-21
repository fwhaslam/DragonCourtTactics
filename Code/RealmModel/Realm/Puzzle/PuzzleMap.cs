//
//	Copyright 2021 Frederick William Haslam born 1962
//


namespace Realm.Puzzle {

	using System;
	using System.Collections.Generic;
	using Realm.Enums;
	using Realm.Tools;
	using System.Linq;

	using static Realm.Tools.MapTools;
	using System.Text;
	using YamlDotNet.Serialization;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Representation of the play region.  Always a rectangle.
	/// Each tile is a square, has height, type, and sometimes an item.
	/// Items are Agents and Objects.  Agents are Heros and Villains.
	/// Objects are interactive terrain.
	/// </summary>
	public class PuzzleMap {

		internal Dictionary<string, string> textMap = new Dictionary<string, string>();

		/// <summary>
		/// Provided to support YAML.
		/// </summary>
		public PuzzleMap() { }

		static public PuzzleMap Allocate(int w, int t) {

			var work = new PuzzleMap();

			work.Title = "Empty Map";
			work.Image = "pic1.png";
			work.Text["Start"] = "Some Story";

			work.Wide = w;
			work.Tall = t;

			work.Places = Create2DArray<Place>(CreatePlace, w, t);

			work.Agents = new List<Agent>();

			return work;
		}

		static public Place CreatePlace(int col, int row, Place src) {
			return new Place(col, row);
		}

		//======================================================================================================================

		public string Title { get; set; }

		public string Image { get; set; }

		public int Wide { get; internal set; }

		public int Tall { get; internal set; }

		public List<Agent> Agents { get; internal set; }

		/// <summary>
		/// Places become 'Map' for Yaml storage.
		/// </summary>
		[YamlIgnore]
		public Place[,] Places { get; internal set; }

		public List<string> Map { get => MapAsStrings(); set => StringsAsMap(value); }

		public Dictionary<string, string> Text { get => textMap; set => textMap = value; }

		//======================================================================================================================

		/// <summary>
		/// Create an agent on the map, removing any in the way.
		/// </summary>
		public void AddAgent(AgentType type, Where loc, DirEnum face) {

			Place place = Places[loc.X, loc.Y];
			if (place.Agent != null) {
				DropAgent(place.Agent);
			}

			place.Agent = new Agent(loc);
			place.Agent.Type = type;
			place.Agent.Face = face;

			Agents.Add(place.Agent);
		}

		public void DropAgent(Agent who) {
			if (who != null) {
				Agents.Remove(who);
				Places[who.Where.X, who.Where.Y] = null;
			}
		}

		public void AddFlag(FlagEnum type, int x, int y) {
			Places[x, y].Flag = type;
		}

		public void DropFlag(int x, int y) {
			Places[x, y].Flag = FlagEnum.None;
		}

		public void AddRow(DirEnum dir) {

			Tuple<int, int> delta = DirEnumTraits.Delta(dir);
			if (delta.Item1 * delta.Item2 != 0) throw new ArgumentException("Can only use orthogonal directions to add a row");

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
					throw new ArgumentException("Can only use orthogonal directions to add a row");
			}

			// copy old values into new layers
			PuzzleMap temp = Allocate(nw, nt);
			for (int wx = 0; wx < ow; wx++) {
				for (int tx = 0; tx < ot; tx++) {
					if (wx + sw < Wide || tx + st < Tall) {
						temp.Places[wx + sw, tx + st] = Places[wx, tx];
					}
				}
			}

			// shift some agents
			foreach (var who in Agents.ToList()) {
				who.Where.X += sw;
				who.Where.Y += st;
				//Console.Out.WriteLine("AGENT AT ="+who.Where );
			}

			// copy over
			Wide = nw;
			Tall = nt;
			Places = temp.Places;
		}

		public void DropRow(DirEnum dir) {

			Tuple<int, int> delta = DirEnumTraits.Delta(dir);
			if (delta.Item1 * delta.Item2 != 0) throw new ArgumentException("Can only use orthogonal directions to add a row");

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
					throw new ArgumentException("Can only use orthogonal directions to cut a row");
			}

			// copy old Places into new Places
			PuzzleMap temp = Allocate(nw, nt);
			//Console.Out.WriteLine("   sw="+sw+"  st="+st );
			for (int wx = 0; wx < nw; wx++) {
				for (int tx = 0; tx < nt; tx++) {
					if (wx - sw >= 0 && tx - st >= 0) {
						temp.Places[wx, tx] = Places[wx - sw, tx - st];
					}
				}
			}

			// remove dropped agents
			foreach (var who in Agents.ToList()) {
				who.Where.X += sw;
				who.Where.Y += st;
				//Console.Out.WriteLine("AGENT AT ="+who.Where );
				// new spot is out of bounds
				if (who.Where.X >= nw || who.Where.Y >= nt || who.Where.X < 0 || who.Where.Y < 0) {
					Agents.Remove(who);
				}
			}

			// copy over
			Wide = nw;
			Tall = nt;
			Places = temp.Places;

		}

		//======================================================================================================================

		// format: HeightChar, FlagChar, AgentID(##)
		//static readonly string PART_FORMAT = "%c%c%02d";

		// one 'part' is one 'tile'
		static readonly char STRING_PARTS_SEP = '/';

		// agent ids
		static readonly string NO_AGENT_SYMBOL = "__";
		static readonly string AGENT_ID_FORMAT = "00";

		//======================================================================================================================

		/// <summary>
		/// String representation of the map.
		/// </summary>
		/// <returns></returns>
		List<string> MapAsStrings() {

			StringBuilder buf = new StringBuilder();

			List<string> list = new List<string>();
			for (int row = 0; row < Tall; row++) {
				buf.Clear();
				for (int col = 0; col < Wide; col++) {
					if (col > 0) buf.Append(STRING_PARTS_SEP);

					Place place = Places[col, row];
					buf.Append(HeightEnumTraits.Symbol(place.Height));
					buf.Append(FlagEnumTraits.Symbol(place.Flag));
					if (place.Agent == null) {
						buf.Append(NO_AGENT_SYMBOL);
					}
					else {
						int indexOf = Agents.IndexOf(place.Agent);
						buf.Append(indexOf.ToString(AGENT_ID_FORMAT));
					}
				}
				list.Add(buf.ToString());
			}
			return list;
		}

		/// <summary>
		/// Use strings to reconstruct map.
		/// </summary>
		/// <param name="source"></param>
		public void StringsAsMap(List<string> source) {

			// prepare the tiles
			Places = Create2DArray<Place>(CreatePlace, Wide, Tall);

			for (int row = 0; row < Tall; row++) {

				string line = source[row];
				string[] parts = line.Split(STRING_PARTS_SEP);

				for (int col = 0; col < Wide; col++) {

					Place place = Places[col, row];
					string part = parts[col];

					// parse single location
					place.Height = HeightEnumTraits.FromSymbol(part[0]);
					place.Flag = FlagEnumTraits.FromSymbol(part[1]);

					string symbol = "" + part[2] + part[3];
					if (!symbol.Equals(NO_AGENT_SYMBOL)) {

						int agentId = 0;
						try { agentId = int.Parse(symbol); }
						catch (FormatException ex) { throw new FormatException("Invalid agent symbol [" + symbol + "] at [" + col + "/" + row + "]", ex); }

						if (agentId > Agents.Count)
							throw new FormatException("No agent defined for id=[" + agentId + "] at [" + col + "/" + row + "]");
						place.Agent = Agents[agentId];
					}
				}

			}
		}

	}
}
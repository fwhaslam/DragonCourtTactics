//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	using System;

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	public class RealmFactory {

		static readonly Random random = new Random();

		static public PuzzleMap SimpleTerrain(int w,int t) {

			PuzzleMap map = PuzzleMap.Allocate( w, t );

			map.Places[w/2,t/2].Height = HeightEnum.Pit;
			map.Places[1+w/2,t/2].Height = HeightEnum.Pit;
			map.Places[w/2,1+t/2].Height = HeightEnum.Pit;
			map.Places[1+w/2,1+t/2].Height = HeightEnum.Pit;

			map.AddAgent( AgentType.PEASANT, new Where(w/2-1, t/2), DirEnum.North );

			return map;
		}

		static public PuzzleMap RandomTerrain() {
			return GenerateTerrain(random.Next());
		}

		static public PuzzleMap GenerateTerrain(int seed) {

			Random rnd = new Random(seed);

			int wide = 5 + rnd.Next(11);
			int tall = 5 + rnd.Next(11);

			PuzzleMap map = PuzzleMap.Allocate(wide, tall);

			for (int w = 0; w < map.Wide; w++) for (int t = 0; t < map.Tall; t++) {
				map.Places[w, t].Height = (HeightEnum)rnd.Next(6);
			}

			// === add agents
			int agents = 3 + rnd.Next(12);
			int ax, at;

			for (int n = 0; n < agents; n++) {

				// find empty spot
				do {
					ax = rnd.Next(map.Wide);
					at = rnd.Next(map.Tall);
				} while (map.Places[ax, at].Agent!=null);

				DirEnum face = (DirEnum)rnd.Next(8);
				AgentType type = AgentType.Get(rnd.Next(AgentType.Count()));

				map.AddAgent(type, new Where(ax, at), face);
			}

			// === add flags
			int flags = 3 + rnd.Next(12);
			for (int n = 0; n < flags; n++) {

				// find empty spot
				do {
					ax = rnd.Next(map.Wide);
					at = rnd.Next(map.Tall);
				} while (map.Places[ax, at].Flag != FlagEnum.None);

				FlagEnum type = (FlagEnum)rnd.Next(FlagEnumTraits.Count());

				map.AddFlag(type, ax, at);
			}


			return map;
		}

		/// <summary>
		/// Used for testing agent power.
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		static public PuzzleMap SingleLineTerrain(int length) {

			return PuzzleMap.Allocate( 1, length );
		}
	}

}

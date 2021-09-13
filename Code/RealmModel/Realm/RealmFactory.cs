
namespace Realm {

	using System;

	public class RealmFactory {

		static readonly Random random = new Random();

		static public LevelMap SimpleTerrain(int w,int t) {

			LevelMap map = LevelMap.Allocate( w, t );

			map.HeightLayer[w/2,t/2] = HeightEnum.One;
			map.HeightLayer[1+w/2,t/2] = HeightEnum.One;
			map.HeightLayer[w/2,1+t/2] = HeightEnum.One;
			map.HeightLayer[1+w/2,1+t/2] = HeightEnum.One;

			map.AddAgent( AgentType.PEASANT, w/2, t/2, DirEnum.North );

			return map;
		}

		static public LevelMap RandomTerrain() {
			return GenerateTerrain(random.Next());
		}

		static public LevelMap GenerateTerrain(int seed) {

			Random rnd = new Random(seed);

			int wide = 5 + rnd.Next(11);
			int tall = 5 + rnd.Next(11);

			LevelMap map = LevelMap.Allocate(wide, tall);

			for (int w = 0; w < map.Wide; w++) for (int t = 0; t < map.Tall; t++) {
					map.HeightLayer[w, t] = (HeightEnum)rnd.Next(6);
				}

			// === add agents
			int agents = 3 + rnd.Next(12);
			int ax, at;

			for (int n = 0; n < agents; n++) {

				do {
					ax = rnd.Next(map.Wide);
					at = rnd.Next(map.Tall);
				} while (map.AgentLayer[ax, at] != LevelMap.NO_AGENT);

				DirEnum face = (DirEnum)rnd.Next(8);
				AgentType type = AgentType.Get(rnd.Next(AgentType.Count()));

				map.AddAgent(type, ax, at, face);
			}

			// === add flags
			int flags = 3 + rnd.Next(12);
			for (int n = 0; n < flags; n++) {

				do {
					ax = rnd.Next(map.Wide);
					at = rnd.Next(map.Tall);
				} while (map.FlagLayer[ax, at] != FlagEnum.None);

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
		static public LevelMap SingleLineTerrain(int length) {

			return LevelMap.Allocate( 1, length );
		}
	}

}

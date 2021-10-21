//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using Realm.Enums;
	using Realm.Tools;

	/// <summary>
	/// One square in a LevelMap.
	/// Contains all details for that one location.
	/// I would call this a 'tile', but that could conflict with the game namespace.
	/// </summary>
	public class Place {

		public Place(int x, int y) {
			Where = new Where(x, y);
		}

		public Where Where { get; set; }

		public int X() { return Where.X; }

		public int Y() { return Where.Y; }

		public HeightEnum Height { get; set; } = HeightEnum.One;

		public FlagEnum Flag { get; set; } = FlagEnum.None;

		public Agent Agent { get; set; } = null;

	}
}

//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Enumerable, which returns Enumerator.
	/// Why on earth is this so complex?
	/// </summary>
	public class PlaceLoop : IEnumerable<Place> {

		PuzzleMap map;
		public PlaceLoop(PuzzleMap src) { map = src; }
		public IEnumerator<Place> GetEnumerator() => new PlaceEnumerator(map);
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	}

	/// <summary>
	/// Iterate through all tiles in PuzzleMap as a linear collection. 
	/// </summary>
	public class PlaceEnumerator : IEnumerator<Place> {

		int dw,dt;
		bool done;
		PuzzleMap map;

		public PlaceEnumerator(PuzzleMap src) {
			this.map = src;
			Reset();
		}

		public Place Current() {
			if (done) return null;
			return map.Places[dw,dt];
		}

		public void Dispose() {}

		public bool MoveNext() {
			if (done) return false;
			if (++dw==map.Wide) {
				dw = 0;
				if (++dt==map.Tall) done = true;
			}
			return !done;
		}

		/// <summary>
		/// Has to be placed BEFORE first element.  
		/// Jesus christ who was stupid enough to design it this way?
		/// </summary>
		public void Reset() {
			dw = -1;
			dt = 0;
			done = (map.Wide * map.Tall) < 1;
		}

		Place IEnumerator<Place>.Current => Current();

		object System.Collections.IEnumerator.Current => Current();
	}
}

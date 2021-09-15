//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Tools {

	/// <summary>
	/// Two dimenstional location with integral values.
	/// </summary>
	public class Where {

		public Where() {
			this.X = 0;
			this.Y = 0;
		}
		public Where( int x, int y ) {
			this.X = x;
			this.Y = y;
		}

		public Where( Where src ) {
			this.X = src.X;
			this.Y = src.Y;
		}

		public Where Set( int x, int y ) {
			this.X = x;
			this.Y = y;
			return this;
		}

		public Where Add( int dx, int dy ) {
			this.X += dx;
			this.Y += dy;
			return this;
		}

		public int X { get; set; }
		public int Y { get; set; }

		public int AddX(int dx) { this.X += dx; return this.X; }
		public int AddY(int dy) { this.Y += dy; return this.Y; }

	}
}

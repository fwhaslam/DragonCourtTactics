//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Tools {

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class WhereTest {

		[TestMethod]
		public void constructor_empty() {

			Where result = new Where();

			AreEqual(0, result.X );
			AreEqual( 0, result.Y );
		}

		[TestMethod]
		public void constructor_int_int() {

			Where result = new Where( 3, 5 );

			AreEqual( 3, result.X );
			AreEqual( 5, result.Y );
		}

		[TestMethod]
		public void constructor_Where() {

			Where src = new Where( 3, 5 );

			// invocation
			Where result = new Where(src);

			AreEqual( 3, result.X );
			AreEqual( 5, result.Y );
		}

		public void Set_int_int() {
			
			Where result = new Where();

			// invocation
			result.Set(3,5);

			// assertions
			AreEqual( 3, result.X );
			AreEqual( 5, result.Y );
		}

		public void SetX_GetX() {
			
			Where result = new Where();

			// invocation
			result.X = 3;

			// assertions
			AreEqual( 3, result.X );
			AreEqual( 0, result.Y );
		}

		public void SetY_GetY() {
			
			Where result = new Where();

			// invocation
			result.Y = 5;

			// assertions
			AreEqual( 0, result.X );
			AreEqual( 5, result.Y );
		}
	}
}

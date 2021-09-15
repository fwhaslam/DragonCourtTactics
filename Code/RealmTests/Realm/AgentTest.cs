//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Tools;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	class AgentTest {

		[TestMethod]
		public void constructor_int_int() {

			// invocation
			Agent result = new Agent( 3, 5 );

			// assertions
			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );

			IsNull( result.Type );
			IsNull( result.Face );

		}

		[TestMethod]
		public void constructor_where() {

			Where where = new Where( 3, 5 );

			// invocation
			Agent result = new Agent( where );

			// assertions
			AreNotSame( where, result.Where );
			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
			
			IsNull( result.Type );
			IsNull( result.Face );

		}

		[TestMethod]
		public void constructor_copy() {

			Agent src = new Agent( 3, 5 );
			src.Type = AgentType.PEASANT;
			src.Face = DirEnum.NorthEast;

			// invocation
			Agent result = new Agent( src );

			// assertions
			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
			
			AreEqual( AgentType.PEASANT, result.Type );
			AreEqual( DirEnum.NorthEast, result.Face );

		}
	}
}

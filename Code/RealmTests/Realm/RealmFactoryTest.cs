//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class RealmFactoryTest {

		[TestMethod]
		public void SimpleTerrain() {

			// invocation
			var result = RealmFactory.SimpleTerrain( 10, 12 );

			// assertions
			AreEqual( 10, result.Places.GetLength(0) );
			AreEqual( 12, result.Places.GetLength(1) );

			AreEqual( 10, result.Wide );
			AreEqual( 12, result.Tall );

			IsNull( result.Places[0,0].Agent );
			IsNotNull( result.Places[4,6].Agent );
			AreEqual( 1, result.Agents.Count );

		}

		[TestMethod]
		public void RandomTerrain() {

			// invocation
			var result = RealmFactory.RandomTerrain();

			// assertions
			IsNotNull(result);

			IsTrue(result.Agents.Count >= 1);
			IsNotNull(result.Agents[0]);

		}

		[TestMethod]
		public void GenerateTerrain() {

			// invocation - fixed seed = same result every time
			var result = RealmFactory.GenerateTerrain(1);

			// assertions
			AreEqual(7, result.Wide);
			AreEqual(6, result.Tall);

			IsNotNull( result.Places[0, 0].Agent );

			AreEqual( 8, result.Agents.Count);

			AreEqual("Soldier", result.Agents[0].Type.Name);

		}

		[TestMethod]
		public void SingleLineTerrain() {

			// invocation
			var result = RealmFactory.SingleLineTerrain( 8 );

			// assertions
			AreEqual( 1, result.Wide );
			AreEqual( 8, result.Tall );

			IsNull( result.Places[0,0].Agent );
			AreEqual( 0, result.Agents.Count );

		}

	}

}

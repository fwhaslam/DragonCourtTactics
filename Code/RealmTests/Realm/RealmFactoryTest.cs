
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace Realm {

	[TestClass]
	public class RealmFactoryTest {

		[TestMethod]
		public void SimpleTerrain() {

			// invocation
			var result = RealmFactory.SimpleTerrain( 10, 12 );

			// assertions
			AreEqual( 10, result.HeightLayer.GetLength(0) );
			AreEqual( 12, result.HeightLayer.GetLength(1) );
			AreEqual( 10, result.FlagLayer.GetLength(0) );
			AreEqual( 12, result.FlagLayer.GetLength(1) );
			AreEqual( 10, result.AgentLayer.GetLength(0) );
			AreEqual( 12, result.AgentLayer.GetLength(1) );

			AreEqual( 10, result.Wide );
			AreEqual( 12, result.Tall );

			AreEqual( 0, result.AgentLayer[0,0] );
			AreEqual( 1, result.AgentLayer[5,6] );
			AreEqual( 2, result.Agents.Count );
			IsNull( result.Agents[0] );

		}

		//[TestMethod]
		//public void RandomTerrain() {

		//	// invocation
		//	var result = RealmFactory.RandomTerrain( );

		//	// assertions
		//	IsNotNull( result );

		//	IsTrue( result.Agents.Count >= 1 );
		//	IsNull( result.Agents[0] );

		//}

		//[TestMethod]
		//public void GenerateTerrain() {

		//	// invocation - fixed seed = same result every time
		//	var result = RealmFactory.GenerateTerrain( 1 );

		//	// assertions
		//	AreEqual( 7, result.Wide );
		//	AreEqual( 6, result.Tall );

		//	AreEqual( 5, result.AgentLayer[0,0] );

		//	AreEqual( 9, result.Agents.Count );
		//	IsNull( result.Agents[0] );
		//	AreEqual( "Soldier", result.Agents[1].Type.Name );

		//}

		[TestMethod]
		public void SingleLineTerrain() {

			// invocation
			var result = RealmFactory.SingleLineTerrain( 8 );

			// assertions
			AreEqual( 1, result.Wide );
			AreEqual( 8, result.Tall );

			AreEqual( 0, result.AgentLayer[0,0] );

			AreEqual( 1, result.Agents.Count );
			IsNull( result.Agents[0] );

		}

	}

}

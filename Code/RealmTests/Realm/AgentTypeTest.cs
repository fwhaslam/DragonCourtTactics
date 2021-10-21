//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using Realm.Puzzle;

	[TestClass]
	public class AgentTypeTest {

		readonly int EXPECTED_AGENT_TYPES = 4;

		[TestMethod]
		public void Keys() {

			// invocation
			List<string> result = AgentType.Keys();

			// assertions
			AreEqual( EXPECTED_AGENT_TYPES, result.Count );

			String display = String.Join( "\n", result );
			Assert.AreEqual("Peasant\nGoblin\nSkeleton\nGhost", display );

		}

		[TestMethod]
		public void Peasant_values() {

			// invocation
			AgentType result = AgentType.PEASANT;

			// assertions
			AreEqual("Peasant", result.Name );
			AreEqual( 0, result.Index );

			AreEqual( 3, result.Health );
			AreEqual( 5, result.Steps );
			AreEqual( 1, result.Damage );
			AreEqual( 3, result.Range );
			AreEqual( 0, result.Armor );

			String display = String.Join( "\n", result.Traits.Select( e => e.Key ) );
			AreEqual("Frail\nWeak", display );

		}
	}
}

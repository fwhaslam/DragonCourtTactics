//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class AgentTypeTest {

		[TestMethod]
		public void GetOptions() {

			// invocation
			List<string> result = AgentType.GetOptions();

			// assertions
			String display = String.Join( "\n", result );
			Assert.AreEqual("None\nPeasant\nSoldier\nGuard\nGoblin\nOrc", display );

		}

		[TestMethod]
		public void Peasant_values() {

			// invocation
			AgentType result = AgentType.PEASANT;

			// assertions
			AreEqual("Peasant", result.Name );
			AreEqual( 0, result.Index );

			AreEqual( 5, result.Health );
			AreEqual( 5, result.Steps );
			AreEqual( 2, result.Damage );
			AreEqual( 1, result.Range );
			AreEqual( 0, result.Armor );

			String display = String.Join( "\n", result.Traits.Select( e => e.Key ) );
			AreEqual("Coward", display );

		}
	}
}

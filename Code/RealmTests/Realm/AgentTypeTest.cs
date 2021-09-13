using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm {

	[TestClass]
	public class AgentTypeTest {

		[TestMethod]
		public void GetOptions() {

			// invocation
			List<string> results = AgentType.GetOptions();

			// assertions
			String display = String.Join( "\n", results );
			Assert.AreEqual("None\nPeasant\nSoldier\nGuard\nGoblin\nOrc", display );

		}
	}
}

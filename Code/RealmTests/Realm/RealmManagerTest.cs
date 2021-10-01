using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


	[TestClass]
	public class RealmManagerTest {

		[TestMethod]
		public void DumpLevelMap( ) {

			LevelMap map = RealmFactory.SimpleTerrain(8,8);

			string result = RealmManager.DumpLevelMap( map );
			
			AreEqual( "Title: Empty Map\n" +
			"Wide: 8\n" +
			"Tall: 8\n" +
			"Map:\n" +
			"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.00/P.__/P.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.__/P.__/P.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
			"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
			"Agents:\n" +
			"- Name: Peasant\n" +
			"  Face: North\n" +
			"  Status: Alert\n" +
			"  Faction: 0", result );
		}
	}
}

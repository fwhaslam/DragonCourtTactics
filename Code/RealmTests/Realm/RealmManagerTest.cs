using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Puzzle;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Utility.AssertStringLinesUtility;


	[TestClass]
	public class RealmManagerTest {

		static readonly string CHECK_LEVEL = "Title: Empty Map\n" +
				"Image: pic1.png\n"+
				"Wide: 8\n" +
				"Tall: 8\n" +
				"Agents:\n" +
				"- Name: Peasant\n" +
				"  Face: North\n" +
				"  Status: Alert\n" +
				"  Faction: 0\n" +
				"Map:\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.00/P.__/P.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/P.__/P.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"Text:\n" + 
				"  Start: Some Story\n";

		[TestMethod]
		public void DumpLevelMap( ) {

			PuzzleMap map = RealmFactory.SimpleTerrain(8,8);

			// invocation
			string result = RealmManager.DumpLevelMap( map );

			// assertions
			result = result.Replace( "\r", "" );

			StringLinesAreEqual( CHECK_LEVEL, result );
		}

		[TestMethod]
		public void ParseLevelMap( ) {

			// invocation
			PuzzleMap result = RealmManager.ParseLevelMap( CHECK_LEVEL );

Console.Out.WriteLine( "MAP>>"+RealmManager.DumpLevelMap( result ) );

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( 8, result.Wide );
			AreEqual( 8, result.Tall );

			AreEqual( "Where(0,0)", result.Places[0,0].Where.ToString() );
			AreEqual( "Peasant", result.Places[3,4].Agent.Name );
		}
	}
}

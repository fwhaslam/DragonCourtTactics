//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class LevelMapTest {

		public void DisplayMap(LevelMap map) {
			Console.Out.WriteLine(RealmManager.DumpLevelMap(map));
		}

		public LevelMap CheckMap() {
			var map = LevelMap.Allocate( 5, 5 );

			map.Places[2,2].Height = HeightEnum.Pit;	// hole in the middle
			map.AddAgent(  AgentType.PEASANT, new Where(2,3), DirEnum.North );
			map.AddFlag( FlagEnum.Lever, 3,2 );

			return map;
		}

		[TestMethod]
		public void Allocate() {
			
			// invocation
			var result = LevelMap.Allocate(10,12);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 10, result.Places.GetLength(0) );
			AreEqual( 12, result.Places.GetLength(1) );

			AreEqual( 10, result.Wide );
			AreEqual( 12, result.Tall );

			IsNull( result.Places[0,0].Agent );
			AreEqual( 0, result.Agents.Count );
		}

//======================================================================================================================

		[TestMethod]
		public void CutRow_North() {

			var map = CheckMap();

			// invocation
			map.DropRow( DirEnum.North );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 5, map.Places.GetLength(0) );
			AreEqual( 4, map.Places.GetLength(1) );

			AreEqual( 5, map.Wide );
			AreEqual( 4, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, map.Places[3,2].Flag );

			IsNotNull( map.Places[2,3].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(2,3)", map.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void CutRow_East() {

			var map = CheckMap();

			// invocation
			map.DropRow( DirEnum.East );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 4, map.Places.GetLength(0) );
			AreEqual( 5, map.Places.GetLength(1) );

			AreEqual( 4, map.Wide );
			AreEqual( 5, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, map.Places[3,2].Flag );

			IsNotNull( map.Places[2,3].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(2,3)", map.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void CutRow_South() {

			var map = CheckMap();

			// invocation
			map.DropRow( DirEnum.South );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 5, map.Places.GetLength(0) );
			AreEqual( 4, map.Places.GetLength(1) );

			AreEqual( 5, map.Wide );
			AreEqual( 4, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[2,1].Height );
			AreEqual( FlagEnum.Lever, map.Places[3,1].Flag );

			IsNotNull( map.Places[2,2].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(2,2)", map.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void CutRow_West() {

			var map = CheckMap();

			// invocation
			map.DropRow( DirEnum.West );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 4, map.Places.GetLength(0) );
			AreEqual( 5, map.Places.GetLength(1) );

			AreEqual( 4, map.Wide );
			AreEqual( 5, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[1,2].Height );
			AreEqual( FlagEnum.Lever, map.Places[2,2].Flag );

			IsNotNull( map.Places[1,3].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(1,3)", map.Agents[0].Where.ToString() );
		}

//======================================================================================================================

		[TestMethod]
		public void AddRow_North() {
			
			var map = CheckMap();

			// invocation
			map.AddRow( DirEnum.North );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 5, map.Places.GetLength(0) );
			AreEqual( 6, map.Places.GetLength(1) );

			AreEqual( 5, map.Wide );
			AreEqual( 6, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, map.Places[3,2].Flag );

			IsNotNull( map.Places[2,3].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(2,3)", map.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void AddRow_East() {
			
			var map = CheckMap();

			// invocation
			map.AddRow( DirEnum.East );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 6, map.Places.GetLength(0) );
			AreEqual( 5, map.Places.GetLength(1) );

			AreEqual( 6, map.Wide );
			AreEqual( 5, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, map.Places[3,2].Flag );

			IsNotNull( map.Places[2,3].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(2,3)", map.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void AddRow_South() {
			
			var map = CheckMap();

			// invocation
			map.AddRow( DirEnum.South );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 5, map.Places.GetLength(0) );
			AreEqual( 6, map.Places.GetLength(1) );

			AreEqual( 5, map.Wide );
			AreEqual( 6, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[2,3].Height );
			AreEqual( FlagEnum.Lever, map.Places[3,3].Flag );

			IsNotNull( map.Places[2,4].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(2,4)", map.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void AddRow_West() {
			
			var map = CheckMap();

			// invocation
			map.AddRow( DirEnum.West );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", map.Title );
			AreEqual( "pic1.png", map.Image );

			AreEqual( 6, map.Places.GetLength(0) );
			AreEqual( 5, map.Places.GetLength(1) );

			AreEqual( 6, map.Wide );
			AreEqual( 5, map.Tall );

			AreEqual( HeightEnum.Pit, map.Places[3,2].Height );
			AreEqual( FlagEnum.Lever, map.Places[4,2].Flag );

			IsNotNull( map.Places[3,3].Agent );
			AreEqual( 1, map.Agents.Count );
			AreEqual( "Where(3,3)", map.Agents[0].Where.ToString() );
		}
	}
}

//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//


namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Realm.Enums;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class RealmAnalyzerTest {

		[TestMethod]
		public void Validate() {

			var map = RealmFactory.SimpleTerrain( 10, 10 );
			AreEqual( "No doors for map entry", RealmAnalyzer.Validate(map) );

			// add door
			map.Places[0,0].Flag = FlagEnum.Door;
			AreEqual( "No chests for map completion", RealmAnalyzer.Validate(map) );

			// add chest
			map.Places[0,1].Flag = FlagEnum.Chest;
			IsNull( RealmAnalyzer.Validate(map) );

		}
	}
}



namespace Realm.Enums {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class HeightEnumTraitsTest {

		[TestMethod]
		public void Symbol() { 
		
			AreEqual( 'P', HeightEnumTraits.Symbol( HeightEnum.Pit) );
			AreEqual( '1', HeightEnumTraits.Symbol( HeightEnum.One) );
		}

		[TestMethod]
		public void FromSymbol() { 
		
			AreEqual( HeightEnum.Pit, HeightEnumTraits.FromSymbol( 'P' ) );
			AreEqual( HeightEnum.One, HeightEnumTraits.FromSymbol( '1' ) );
		}


	}
}

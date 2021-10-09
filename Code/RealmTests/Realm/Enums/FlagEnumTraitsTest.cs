

namespace Realm.Enums {
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class FlagEnumTraitsTest {

		[TestMethod]
		public void Symbol() { 
		
			AreEqual( 'X', FlagEnumTraits.Symbol( FlagEnum.Switch) );
			AreEqual( '.', FlagEnumTraits.Symbol( FlagEnum.None) );
		}

		[TestMethod]
		public void FromSymbol() { 
		
			AreEqual( FlagEnum.Switch, FlagEnumTraits.FromSymbol( 'X' ) );
			AreEqual( FlagEnum.None, FlagEnumTraits.FromSymbol( '.' ) );
		}

	}

}

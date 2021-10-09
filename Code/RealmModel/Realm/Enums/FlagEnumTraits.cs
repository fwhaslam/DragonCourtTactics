//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	using System;
	using System.Collections.Generic;

	public class FlagEnumTraits {

		static readonly char[] Symbols = {
			'.', 
			'E','D','H','C','S',
			'L','X','G','P','M',
			'-','+','K','A','B'
		};

		static readonly List<char> SymbolList = new List<char>(Symbols);

		static public int Count() { return Enum.GetNames(typeof(DirEnum)).Length; }

		static public char Symbol(FlagEnum flag) {
			return Symbols[(int)flag];
		}

		static public FlagEnum FromSymbol( char symbol) {
			int value = SymbolList.IndexOf(symbol);
			return (FlagEnum)value;
		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Enums {
	class HeightEnumTraits {

		static readonly char[] Symbols = {'P','1','2','3','4','5','W'};

		static public bool IsDamaging( HeightEnum height ) {
			return (height==HeightEnum.Pit);
		}

		static public bool IsBlocking( HeightEnum height ) {
			return (height==HeightEnum.Wall);
		}

		static public bool IsEnterable( HeightEnum from, HeightEnum to ) {
			if ( IsBlocking(to) ) return false;
			if ( (int)to -(int)from >= 2 ) return false;
			return true;
		}

		static public char Symbol(HeightEnum src ) {
			return Symbols[(int)src];
		}
	}
}

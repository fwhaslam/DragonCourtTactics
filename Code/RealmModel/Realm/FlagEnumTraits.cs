using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm {

	public class FlagEnumTraits {
		static public int Count() { return Enum.GetNames(typeof(DirEnum)).Length; }

	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Realm {
	public class RealmManager {

		static public string DumpLevelMap( LevelMap map ) {
			return new Serializer().Serialize(map);
		}
	}
}

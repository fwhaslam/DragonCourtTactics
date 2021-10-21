using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Realm {
	public class RealmManager {

		static public string DumpLevelMap( PuzzleMap map ) {
			return new Serializer().Serialize(map);
		}

		static public PuzzleMap ParseLevelMap( string content ) {
			return new Deserializer().Deserialize<PuzzleMap>(content);
		}

	}
}

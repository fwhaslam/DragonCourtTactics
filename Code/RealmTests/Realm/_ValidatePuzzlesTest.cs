using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Realm {

	using static Utility.SimpleTools;

	/// <summary>
	/// Load al puzzles from Assets/Puzzles to enesure they all parse.
	/// </summary>
	[TestClass]
	public class _ValidatePuzzlesTest {

		[TestMethod]
		public void ParseAllPuzzles() {

			DirectoryInfo puzzleDir = new DirectoryInfo( Path.Combine( SolutionDir(), @"Assets\Puzzles") );
Console.Out.WriteLine(" FILE PATH = "+puzzleDir.FullName );

			IsTrue( puzzleDir.Exists );

			FileInfo[] files = puzzleDir.GetFiles();
			foreach ( var file in files ) {
				if (!file.Name.EndsWith("_dctpzl.yml")) continue;
				var content = File.ReadAllText( file.FullName );
				Console.Out.WriteLine("Parsing File Named ["+file.Name+"]");
				RealmManager.ParseLevelMap( content );
				Console.Out.WriteLine("Parsing Complete!");
			}

		}

	}
}

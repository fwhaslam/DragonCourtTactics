using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility {

	public class SimpleTools {

		static readonly string BINARY_FOLDER = "bin";
		static readonly string CODE_FOLDER = "Code";

		static public string FixFilePath( string path ) {
			return path.Replace( '/', Path.DirectorySeparatorChar );
		}

		/// <summary>
		/// The directory containing the compiled code.
		/// </summary>
		/// <returns></returns>
		static public string WorkingDir() {
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		/// <summary>
		/// The directory above the 'bin 'folder;
		/// </summary>
		/// <returns></returns>
		static public string ProjectDir() {
			DirectoryInfo info = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			while (!info.Name.Equals(BINARY_FOLDER)) info = info.Parent;
			return info.Parent.FullName;
		}

		/// <summary>
		/// The directory above the 'Code 'folder;
		/// </summary>
		/// <returns></returns>
		static public string SolutionDir() {
			DirectoryInfo info = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			while (!info.Name.Equals(CODE_FOLDER)) info = info.Parent;
			return info.Parent.FullName;
		}

	}
}

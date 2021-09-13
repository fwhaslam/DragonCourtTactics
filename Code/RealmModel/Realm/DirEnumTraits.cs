using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm {

	public class DirEnumTraits {

		static Tuple<int,int>[] delta = { 
			new Tuple<int,int>(0,1),
			new Tuple<int,int>(1,1),
			new Tuple<int,int>(1,0),
			new Tuple<int,int>(1,-1),

			new Tuple<int,int>(0,-1),
			new Tuple<int,int>(-1,-1),
			new Tuple<int,int>(-1,0),
			new Tuple<int,int>(-1,1) };

		static public int Count() { return Enum.GetNames(typeof(DirEnum)).Length; }

		static public int DX(DirEnum dir) { return delta[(int)dir].Item1; }

		static public int DY(DirEnum dir) { return delta[(int)dir].Item2; }

		static public Tuple<int,int> Delta(DirEnum dir) { return delta[(int)dir]; }

	}

}

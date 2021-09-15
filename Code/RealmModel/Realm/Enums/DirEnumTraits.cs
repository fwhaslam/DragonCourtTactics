//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	using System;

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

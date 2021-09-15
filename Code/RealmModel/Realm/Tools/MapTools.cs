//
//	Copyright 2021 Frederick William Haslam born 1962
//


using System;

namespace Realm.Tools {

	public class MapTools {

		/// <summary>
		///  Allocate and fill a 2d array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="wide"></param>
		/// <param name="tall"></param>
		/// <returns></returns>
		static public T[,] Make2DArray<T>( T value, int wide, int tall ) {
			return Fill( value, new T[wide,tall] );
		}
		
		/// <summary>
		/// Create a 2D array, then apply the 'create' function as a transform.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="transform"></param>
		/// <param name="wide"></param>
		/// <param name="tall"></param>
		/// <returns></returns>
		static public T[,] Create2DArray<T>( Func<int,int,T,T> create, int wide, int tall ) {
			return Transform<T>( create, new T[wide,tall] );
		}


		/// <summary>
		/// Fill two dimensional array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="array"></param>
		static public  T[,] Fill<T>( T value, T[,] array ) {

			int wide = array.GetLength(0);
			int tall = array.GetLength(1);
			for ( int wx=0;wx<wide;wx++) {
				for (int tx=0;tx<tall;tx++) {
					array[wx,tx] = value;
				}
			}
			return array;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="transform">Function(x,y,src)>>result</param>
		/// <param name="array"></param>
		/// <returns></returns>
		static public  T[,] Transform<T>( Func<int,int,T,T> transform, T[,] array ) {

			int wide = array.GetLength(0);
			int tall = array.GetLength(1);
			for ( int wx=0;wx<wide;wx++) {
				for (int tx=0;tx<tall;tx++) {
					array[wx,tx] = transform( wx, tx, array[wx,tx] );
				}
			}
			return array;
		}
	}

}

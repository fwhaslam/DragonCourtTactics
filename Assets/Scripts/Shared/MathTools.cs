//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {

	using UnityEngine;

	static public class MathTools {

		static public float PositionToDegrees( float x, float y ) {

			return Mathf.Atan2( y, x ) * Mathf.Rad2Deg;
		}

		static public Vector2 DegreesToPosition( float degrees ) {

			float radians = degrees * Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(radians),Mathf.Sin(radians));
		}
		/// <summary>
		/// 'Good' enough modulus when the expected delta is small
		/// </summary>
		/// <param name="value"></param>
		/// <param name="mod"></param>
		/// <returns></returns>
		static public float Wrap( float value, float mod ) {
			if (value<0f) return value+mod;
			return value % mod;
		}

		/// <summary>
		/// Precise modulus.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="mod"></param>
		/// <returns></returns>
		static public float Modulus( float value, float mod ) {
			return ( value % mod + mod ) % mod;
		}
	}

}

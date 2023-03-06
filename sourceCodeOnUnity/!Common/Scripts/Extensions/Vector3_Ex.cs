using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Common;

namespace Project.Extension
{
	public static class Vector3_Ex
	{
		/// </summary>
		/// 負の数の使う/使わないを統一します。
		/// </summary>
		public static Vector3 GetSignUnified(this Vector3 vector, bool useNegative)
		{
			return new Vector3
			{
				x = unifyFloat(vector.x),
					y = unifyFloat(vector.y),
					z = unifyFloat(vector.z)
			};

			float unifyFloat(float original)
			{
				if (!useNegative)
					if (original < 0) original += 360;
					else if (original > 180) original -= 360;
				return original;
			}
		}

		/// </summary>
		/// コサイン類似度を返します。
		/// </summary>
		public static float CosineSimilarity(Vector3 vector1, Vector3 vector2)
		{
			// コサイン類似度=単位ベクトルの内積
			return Vector3.Dot(vector1.normalized, vector2.normalized);
		}

		/// </summary>
		/// 各軸を絶対値に変換し、返します。
		/// </summary>
		public static Vector3 GetAbsolute(this Vector3 vector)
		{
			//各軸の符号を払う
			return new Vector3
			{
				x = Math.Abs(vector.x),
					y = Math.Abs(vector.y),
					z = Math.Abs(vector.z),
			};
		}

		/// </summary>
		/// 商を求めます。
		/// </summary>
		public static Vector3 Divide(this Vector3 vector1, Vector3 vector2)
		{
			return new Vector3
			{
				x = vector1.x / vector2.x,
					y = vector1.y / vector2.y,
					z = vector1.z / vector2.z
			};
		}
	}
}
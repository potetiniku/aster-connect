using System.Collections;
using System.Collections.Generic;
using Project.Common;
using UnityEngine;

namespace Project.Common
{
	/// <summary>
	/// 汎用クラスを作成するためのクラス。このクラスを継承した場合、static修飾子は不要。
	/// なお、このクラスを使う際は注意が必要。詳しくはソースコードを参照。
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SingletonMonobehaviour<T> : MonoBehaviour
	where T : new()
	{
		public static T Instance;

		private void Awake()
		{
			if (Instance == null)
			{
				// ここで代入されるのが継承先のコンポーネントとは
				// 別のインスタンスであることに注意
				Instance = new T();
				DontDestroyOnLoad(gameObject);
			}
			else
				Destroy(gameObject);
		}
	}
}
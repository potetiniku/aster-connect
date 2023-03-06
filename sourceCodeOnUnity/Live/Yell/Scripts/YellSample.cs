using Unity.Linq;
using UnityEngine;

/// <summary>
/// エール確認画面に表示される見本
/// </summary>
public class YellSample : MonoBehaviour
{
	private void Start()
	{
		const int objectViewLayer = 7;
		gameObject.Children().ForEach(c => c.layer = objectViewLayer);

		// Scaleを標準化
		Vector3 size = GetComponent<Collider>().bounds.size;
		const float targetSize = 0.75f;
		float scaleFactor = targetSize / Mathf.Max(size.x, size.y, size.z);
		transform.localScale *= scaleFactor;

		// オブジェクトの中央をワールド座標の原点に合わせる
		float height = GetComponent<Collider>().bounds.size.y;
		Vector3 center = Vector3.down * height / 2;
		transform.position = center;
	}

	private void Update()
	{
		transform.Rotate(0, 2, 0);
	}
}

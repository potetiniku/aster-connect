using Unity.Linq;
using UnityEngine;

/// <summary>
/// �G�[���m�F��ʂɕ\������錩�{
/// </summary>
public class YellSample : MonoBehaviour
{
	private void Start()
	{
		const int objectViewLayer = 7;
		gameObject.Children().ForEach(c => c.layer = objectViewLayer);

		// Scale��W����
		Vector3 size = GetComponent<Collider>().bounds.size;
		const float targetSize = 0.75f;
		float scaleFactor = targetSize / Mathf.Max(size.x, size.y, size.z);
		transform.localScale *= scaleFactor;

		// �I�u�W�F�N�g�̒��������[���h���W�̌��_�ɍ��킹��
		float height = GetComponent<Collider>().bounds.size.y;
		Vector3 center = Vector3.down * height / 2;
		transform.position = center;
	}

	private void Update()
	{
		transform.Rotate(0, 2, 0);
	}
}

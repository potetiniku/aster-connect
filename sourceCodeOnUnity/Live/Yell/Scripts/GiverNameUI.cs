using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiverNameUI : MonoBehaviour
{
	[SerializeField] private TMP_Text label;

	public void Initialize(string giverName)
	{
		label.text = giverName;
	}

	private void Update()
	{
		transform.LookAt(Camera.main.transform.position);
		Vector3 changed = transform.rotation.eulerAngles;
		changed.y += 180;
		transform.rotation = Quaternion.Euler(changed);
	}
}

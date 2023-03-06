using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEventTrigger : MonoBehaviour
{
	public UnityEvent onAwake = new();

	private void Awake()
	{
		onAwake.Invoke();
		DontDestroyOnLoad(gameObject);
	}
}
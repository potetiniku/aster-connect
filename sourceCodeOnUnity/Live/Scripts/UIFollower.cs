using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace raspberly.ovr
{
	public class UIFollower : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private float followMoveSpeed = 0.1f;
		[SerializeField] private float followRotateSpeed = 0.02f;
		[SerializeField] private float rotateSpeedThreshold = 0.9f;
		[SerializeField] private bool shouldImmediateMove;
		[SerializeField] private bool shouldLockX;
		[SerializeField] private bool shouldLockY;
		[SerializeField] private bool shouldLockZ;
		[SerializeField] private bool shouldLockYPosition;
		[SerializeField] private float lockTargetYPosition;
		private Quaternion rot;
		private Quaternion rotDif;

		private void Start()
		{
			if (!target) target = Camera.main.transform;
		}

		private void LateUpdate()
		{
			if (shouldImmediateMove) transform.position = target.position;
			else
			{
				Vector3 targetPosition = shouldLockYPosition ?
					new Vector3(target.position.x, lockTargetYPosition, target.position.z)
					: target.position;
				transform.position = Vector3.Lerp(transform.position, targetPosition, followMoveSpeed);
			}

			rotDif = target.rotation * Quaternion.Inverse(transform.rotation);
			rot = target.rotation;
			if (shouldLockX) rot.x = 0;
			if (shouldLockY) rot.y = 0;
			if (shouldLockZ) rot.z = 0;
			if (rotDif.w < rotateSpeedThreshold) transform.rotation = Quaternion.Lerp(transform.rotation, rot, followRotateSpeed * 4);
			else transform.rotation = Quaternion.Lerp(transform.rotation, rot, followRotateSpeed);
		}

		//‹­§“I‚É“¯Šú‚³‚¹‚½‚¢Žž
		public void ImmediateSync(Transform targetTransform)
		{
			transform.position = targetTransform.position;
			transform.rotation = targetTransform.rotation;
		}
	}
}
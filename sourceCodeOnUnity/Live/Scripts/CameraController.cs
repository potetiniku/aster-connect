using System.Collections.Generic;
using AsterConnect.Model.MainApp;
using UnityEngine;
using UnityEngine.EventSystems;

// 参考: https://vend9520-lab.net/wp/?p=333
public class CameraController : MonoBehaviour
{
	[SerializeField] private GameObject targetCamera;
	[SerializeField] private float rotateSpeed = 10f;
	[SerializeField] private float pinchSpeed = 1f;
	[SerializeField] private float maxAngle = 60f;
	[SerializeField] private float minAngle = 360f; // 360 - <下向きの角度>
	[SerializeField] private float minDistance = 1f;
	[SerializeField] private float maxDistance = 10f;

	private Vector2 previousPoint;
	private readonly Vector2[] previousPoints = new Vector2[2];
	private bool isPinched;
	private bool locked;

	private void Start()
	{
		if (RoleSwitch.Role != Role.Client) Destroy(this);
	}

	private void Update()
	{
		if (!IsTouched())
		{
			locked = false;
		}

		//1本指でタップした場合は回転
		else if (IsSingleTouched())
		{
			if (GetTouchPhase() == TouchPhase.Began &&
				IsOverUI(Input.mousePosition)) locked = true;
			if (locked) return;

			//押下時のポイントを取得
			if (GetTouchPhase() == TouchPhase.Began)
			{
				previousPoint = Input.mousePosition;
				isPinched = false;
			}

			//スワイプでの継続した入力があった場合、その方向へ回転させる
			else if (GetTouchPhase() == TouchPhase.Moved)
			{
				if (isPinched) return;
				Vector2 currentPoint = Input.mousePosition;

				//水平方向の移動があった場合、水平方向に回転
				if (currentPoint.x - previousPoint.x != 0)
				{
					float horizontalAmount = currentPoint.x - previousPoint.x;
					horizontalAmount *= rotateSpeed * Time.deltaTime;
					transform.Rotate(0, horizontalAmount, 0, Space.World);
				}

				//垂直方向の回転があった場合、垂直方向に回転
				if (currentPoint.y - previousPoint.y != 0)
				{
					float verticalAmount = currentPoint.y - previousPoint.y;
					verticalAmount *= -rotateSpeed * Time.deltaTime;

					float currentX = transform.rotation.eulerAngles.x;

					//上方向に回転
					if (currentX <= maxAngle)
					{
						// 最大角度を超える場合はそこで止める
						if (verticalAmount + currentX > maxAngle)
							verticalAmount = maxAngle - currentX;

						transform.Rotate(verticalAmount, 0, 0, Space.Self);
					}
					// カメラが上側にあるとき、下方向にのみ回転
					else if (currentX <= 180 && verticalAmount < 0)
						transform.Rotate(verticalAmount, 0, 0, Space.Self);

					//下方向に回転
					if (currentX >= minAngle)
					{
						// 最大角度を超える場合はそこで止める
						if (verticalAmount + currentX < minAngle)
							verticalAmount = minAngle - currentX;

						transform.Rotate(verticalAmount, 0, 0, Space.Self);
					}
					// カメラが下側にあるとき、上方向にのみ回転
					else if (currentX > 180 && verticalAmount > 0)
						transform.Rotate(verticalAmount, 0, 0, Space.Self);
				}
				previousPoint = currentPoint;
			}
		}

		//2本指でタップした場合はカメラ移動(ピンチとスワイプ)
		else if (Input.touchCount == 2)
		{
			if (locked) return;
			if (Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				var currentPoints = new Vector2[2];
				currentPoints[0] = Input.GetTouch(0).position;
				currentPoints[1] = Input.GetTouch(1).position;

				//各指の1フレームで移動した分のベクトルを取得
				var diffPoints = new Vector2[2];
				diffPoints[0] = currentPoints[0] - previousPoints[0];
				diffPoints[1] = currentPoints[1] - previousPoints[1];

				//更にそのベクトル間のベクトルを取得(これが各指の移動ベクトルよりも小さいならば各指は同じ方向に移動していると判断し、スワイプと判別)
				Vector2 difference = diffPoints[1] - diffPoints[0];

				bool flg1 = diffPoints[0].magnitude != 0 &&
					difference.magnitude < diffPoints[0].magnitude;

				bool flg2 = diffPoints[1].magnitude != 0 &&
					difference.magnitude < diffPoints[1].magnitude;

				//各指が移動しており、差分が両指の移動ベクトルよりも小さい場合はスワイプと判断
				if (!(flg1 && flg2))
				{
					//ピンチ処理
					float nowPointDistance = Vector2.Distance(currentPoints[0], currentPoints[1]);
					float beforePointDistance = Vector2.Distance(previousPoints[0], previousPoints[1]);
					float zoomAmount = nowPointDistance - beforePointDistance;
					zoomAmount *= pinchSpeed * Time.deltaTime;
					Zoom(zoomAmount);
				}
			}

			previousPoints[0] = Input.GetTouch(0).position;
			previousPoints[1] = Input.GetTouch(1).position;
			isPinched = true;
		}

		if (!IsOverUI(Input.mousePosition))
			Zoom(Input.mouseScrollDelta.y / 2);

		void Zoom(float distance)
		{
			Vector3 cameraPosition = targetCamera.transform.localPosition;
			float goalZ = cameraPosition.z + distance;

			if (goalZ > -minDistance)
			{
				targetCamera.transform.localPosition =
					new Vector3(0, 0, -minDistance);
			}
			else if (goalZ < -maxDistance)
			{
				targetCamera.transform.localPosition =
					new Vector3(0, 0, -maxDistance);
			}
			else
			{
				targetCamera.transform.localPosition =
					new Vector3(0, 0, goalZ);
			}
		}
	}

	private bool IsTouched()
	{
		if (RoleSwitch.Role != Role.Client) return false;
#if UNITY_ANDROID
		return Input.touchCount > 0;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
		return Input.GetMouseButton(0);
#endif
	}

	private bool IsSingleTouched()
	{
		if (RoleSwitch.Role != Role.Client) return false;
#if UNITY_ANDROID
		return Input.touchCount == 1;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
		return Input.GetMouseButton(0);
#endif
	}

	private TouchPhase GetTouchPhase()
	{
		if (RoleSwitch.Role != Role.Client) return default;
#if UNITY_ANDROID
		return Input.GetTouch(0).phase;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) return TouchPhase.Began;
		if (!Input.GetMouseButtonDown(0) && Input.GetMouseButton(0)) return TouchPhase.Moved;
		return TouchPhase.Canceled;
#endif
	}

	private static bool IsOverUI(Vector2 screenPosition)
	{
		PointerEventData eventDataCurrentPosition = new(EventSystem.current)
		{
			position = screenPosition
		};

		List<RaycastResult> raycastResults = new();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, raycastResults);
		return raycastResults.Count > 0;
	}
}
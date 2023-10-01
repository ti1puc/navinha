using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields
	[Header("Movement")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private Vector2 minMaxRangeHorizontal;
	[SerializeField] private Vector2 minMaxRangeVertical;

	private Vector3 initialPosition;
	#endregion

	#region Unity Messages
	private void Awake()
	{
		initialPosition = transform.position;
	}

	private void Update()
	{
		// horizontal calculations
		float horizontal = Input.GetAxis("Horizontal");
		float yRot = horizontal * Time.deltaTime * rotationSpeed;

		// vertical calculations
		float vertical = Input.GetAxis("Vertical");
		float zPos = vertical * Time.deltaTime * moveSpeed;

		// go forward
		transform.position += transform.forward * zPos;

		// rotate according to horizontal axis
		transform.Rotate(0, yRot, 0);

		// clamp position
		float clampedX = Mathf.Clamp(transform.position.x, minMaxRangeHorizontal.x, minMaxRangeHorizontal.y);
		float clampedZ = Mathf.Clamp(transform.position.z, minMaxRangeVertical.x, minMaxRangeVertical.y);
		transform.position = new Vector3(clampedX, initialPosition.y, clampedZ);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}

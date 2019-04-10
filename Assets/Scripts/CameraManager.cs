using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private const int DEGREES_INCREMENT = 2;
	private const int VERTICAL_LOWER_LIMIT = -70;
	private const int VERTICAL_UPPER_LIMIT = 70;

	private float degreesVertical;

	private void Start()
	{
		this.degreesVertical = 0;
	}

	private void Update()
	{
		HandleHorizontalCameraRotation();
		HandleVerticalCameraRotation();
	}

	private void HandleHorizontalCameraRotation()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
			this.transform.Rotate(0, -DEGREES_INCREMENT, 0, Space.World);
		if (Input.GetKey(KeyCode.RightArrow))
			this.transform.Rotate(0, DEGREES_INCREMENT, 0, Space.World);
	}

	private void HandleVerticalCameraRotation()
	{
		if (Input.GetKey(KeyCode.DownArrow) && VERTICAL_LOWER_LIMIT < this.degreesVertical)
		{
			this.degreesVertical -= DEGREES_INCREMENT;
			this.transform.Rotate(-DEGREES_INCREMENT, 0, 0, Space.Self);
		}
		if (Input.GetKey(KeyCode.UpArrow) && this.degreesVertical < VERTICAL_UPPER_LIMIT)
		{
			this.degreesVertical += DEGREES_INCREMENT;
			this.transform.Rotate(DEGREES_INCREMENT, 0, 0, Space.Self);
		}
	}
}

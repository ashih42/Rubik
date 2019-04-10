using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
	public const int DEGREES_INCREMENT = 3;

	private static RotationManager instance;

	public static RotationManager Instance
	{
		get { return RotationManager.instance; }
	}

	private void Awake()
	{
		if (RotationManager.instance == null)
		{
			RotationManager.instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(gameObject);
	}

	public void RotateInstantly(Vector3 eulerAngles)
	{
		GameManager.Instance.IsBusy = true;
		this.transform.Rotate(eulerAngles, Space.World);
		this.UnparentAllChildren();
		GameManager.Instance.IsBusy = false;
	}

	public void RotateGradually(Vector3 eulerAngles)
	{
		StartCoroutine(this.RotateCoroutine(eulerAngles));
	}

	private IEnumerator RotateCoroutine(Vector3 eulerAngles)
	{
		GameManager.Instance.IsBusy = true;
		for (int i = 0; i < 90/DEGREES_INCREMENT; i++)
		{
			this.transform.Rotate(eulerAngles, Space.World);
			yield return new WaitForEndOfFrame();
		}
		this.UnparentAllChildren();
		GameManager.Instance.IsBusy = false;
	}

	private void UnparentAllChildren()
	{
		for (int i = this.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = this.transform.GetChild(i);
			child.GetComponent<Cubie>().Unselect();
			child.transform.SetParent(GameManager.Instance.RubiksCubeGO.transform);
		}
	}
}

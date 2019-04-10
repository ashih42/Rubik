using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSelector : ASelector
{
	protected override void Start()
	{
		base.Start();
	}

	public void SetCubiesParent(Transform parent)
	{
		foreach (GameObject cubieGO in this.cubies)
		{
			cubieGO.GetComponent<Cubie>().Select();
			cubieGO.transform.SetParent(parent);
		}
	}
}

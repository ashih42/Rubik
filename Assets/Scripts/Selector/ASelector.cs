using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASelector : MonoBehaviour
{
	[SerializeField] protected List<GameObject> cubies;

	protected virtual void Start()
	{
		this.cubies = new List<GameObject>();
	}

	public void Reset()
	{
		this.cubies.Clear();
	}

	private void OnTriggerEnter(Collider other)
	{
		this.cubies.Add(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		this.cubies.Remove(other.gameObject);
	}
}

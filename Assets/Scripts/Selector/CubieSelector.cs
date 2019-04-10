using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieSelector : ASelector
{
	protected override void Start()
	{
		base.Start();
	}

	public string getCubieID()
	{
		return this.cubies[0].GetComponent<Cubie>().ID;
	}

}

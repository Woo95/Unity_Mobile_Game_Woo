using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTower : Tower
{
    public Guardian prefabGuardian;

	private void OnMouseDown()
	{
		Debug.Log(">>Guardian UI Call");
	}
}

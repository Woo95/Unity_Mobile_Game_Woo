using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	#region singletone;
	public static UnitManager instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion
	public List<Guardian> unitList = new List<Guardian>();

	public void Init()
	{
		unitList.Clear();
	}

	public void Play()
	{
	}

	public void End()
	{
	}

	public void Add(Guardian guardian)
	{
		if (!unitList.Contains(guardian))
			unitList.Add(guardian);
	}

	public void Remove(Guardian guardian)
	{
		if (unitList.Contains(guardian))
			unitList.Remove(guardian);
	}
}

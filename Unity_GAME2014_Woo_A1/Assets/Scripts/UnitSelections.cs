using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
	#region singletone
	public static UnitSelections Instance;
	private void Awake()
	{
		Instance = this;
	}
	#endregion

	public List<GameObject> unitList = new List<GameObject>();
	public List<GameObject> unitSelected = new List<GameObject>();

	public void ClickSelect(GameObject unitToAdd)
	{
		Debug.Log("Clicked");
		if (!unitSelected.Contains(unitToAdd))
		{
			unitSelected.Add(unitToAdd);
			unitToAdd.GetComponent<SpriteRenderer>().color = Color.gray;
		}
		else
		{
			unitSelected.Remove(unitToAdd);
			unitToAdd.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	public void DeselectAll()
	{
		foreach (var unit in unitSelected)
		{
			unit.GetComponent<SpriteRenderer>().color = Color.white;
		}
		unitSelected.Clear();
	}

	public void Deselect(GameObject unitToDeselect)
	{

	}
}

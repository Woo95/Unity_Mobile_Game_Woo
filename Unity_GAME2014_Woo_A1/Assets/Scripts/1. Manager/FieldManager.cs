using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
	public List<PickUp> pickUpPrefabList = new List<PickUp>();
	public List<PickUp> pickUpSpawnedList = new List<PickUp>();

	public int SPAWN_OBJECT_MAX = 35;

	public Transform topLeft, bottomRight;
	Vector3 p00, p01, p11, p10;
	Vector3 offset = new Vector2(1, 1);
	public void Init()
	{
		p01 = topLeft.position;
		p10 = bottomRight.position;
		p00 = new Vector3(p01.x, p10.y) + offset;
		p11 = new Vector3(p10.x, p01.y) - offset;

		pickUpSpawnedList.Clear();

		PlaceObject(SPAWN_OBJECT_MAX);
	}

	public void PlaceObject(int generateAmount)
	{
		PickUp pickUp;
		Vector3 pos = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		int index;
		for (int i = 0; i < generateAmount; i++)
		{
			pos.x = Random.Range(p00.x, p11.x);
			pos.y = Random.Range(p00.y, p11.y);

			pos = Vector3Int.FloorToInt(pos * 2.0f);	// to around decimal by 0.5
			pos *= 0.5f;

			index = Random.Range(0, pickUpPrefabList.Count);
			pickUp = Instantiate(pickUpPrefabList[index], pos, rotation, transform);

			pickUp.InitData(this);
			pickUpSpawnedList.Add(pickUp);
		}
	}

	public void DestroyPickUp(PickUp destroyObj)
	{
		if (pickUpSpawnedList.Contains(destroyObj))
		{
			pickUpSpawnedList.Remove(destroyObj);
		}
	}

	public void PlaceObject()
	{
		if (pickUpSpawnedList.Count < SPAWN_OBJECT_MAX)
		{
			PlaceObject(SPAWN_OBJECT_MAX - pickUpSpawnedList.Count);
		}
	}

	public void End()
	{

	}
}

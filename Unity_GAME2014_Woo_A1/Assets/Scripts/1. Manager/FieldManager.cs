using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
	public List<PickUp> pickUpPrefabList = new List<PickUp>();
	public List<PickUp> pickUpSpawnedList = new List<PickUp>();

	public int SPAWN_OBJECT_MAX = 100;

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

	public LayerMask layer;
	Vector3 boxSize = new Vector3(1.0f, 1.2f, 0.1f);
	public void PlaceObject(int generateAmount)
	{
		Collider[] colliderList;

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
			
			colliderList = Physics.OverlapBox(pos, boxSize, rotation, layer);
			if (colliderList != null && colliderList.Length >= 1)
			{
				i--;
				continue;
			}

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

	public void ChopTree(PickUp tree, PickUp treeChopped)
	{
		Vector3 pos = tree.transform.position; 
		Quaternion rotation = Quaternion.identity;
		if (pickUpSpawnedList.Contains(tree))
		{
			pickUpSpawnedList.Remove(tree);

			tree = Instantiate(treeChopped, pos, rotation, transform);
			tree.InitData(this);
			pickUpSpawnedList.Add(tree);
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

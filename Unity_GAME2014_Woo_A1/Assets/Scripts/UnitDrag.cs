using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
	Camera myCam;

	public RectTransform boxVisual;

	Rect selectionBox;

	Vector2 startPosition;
	Vector2 endPosition;

	private void Start()
	{
		myCam = Camera.main;
		startPosition = Vector2.zero;
		endPosition = Vector2.zero;
		DrawVisual();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startPosition = Input.mousePosition;
			selectionBox = new Rect();

		}

		if (Input.GetMouseButton(0))
		{
			endPosition = Input.mousePosition;
			DrawVisual();
			DrawSelection();
		}

		if (Input.GetMouseButtonUp(0))
		{
			SelectUnits();
			startPosition = Vector2.zero;
			endPosition = Vector2.zero;
			DrawVisual();
		}
	}

	void DrawVisual()
	{
		Vector2 boxStart = startPosition;
		Vector2 boxEnd = endPosition;

		Vector2 boxCenter = (boxStart + boxEnd) * 0.5f;
		boxVisual.position = boxCenter;

		Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

		boxVisual.sizeDelta = boxSize;
	}

	void DrawSelection()
	{
		if (Input.mousePosition.x < startPosition.x)
		{
			selectionBox.xMin = Input.mousePosition.x;
			selectionBox.xMax = startPosition.x;
		}
		else
		{
			selectionBox.xMin = startPosition.x;
			selectionBox.xMax = Input.mousePosition.x;
		}

		if (Input.mousePosition.y < startPosition.y)
		{
			selectionBox.yMin = Input.mousePosition.y;
			selectionBox.yMax = startPosition.y;
		}
		else
		{
			selectionBox.yMin = startPosition.y;
			selectionBox.yMax = Input.mousePosition.y;
		}
	}

	void SelectUnits()
	{
		foreach (var unit in UnitSelections.Instance.unitList)
		{
			if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
			{
				UnitSelections.Instance.DragSelect(unit);
			}
		}
	}
}

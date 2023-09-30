using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class WaveFunction : MonoBehaviour
{
	public int dimensions;
	public Tile[] tileObjects;
	public List<Cell> gridComponents;
	public Cell cellObj;

	int iterations = 0;

	void Awake()
	{
		gridComponents = new List<Cell>();
		InitializeGridRect();
	}

	#region InitializeGridCircle
	//void InitializeGridCircle()
	//{
	//	int radius = (int)(dimensions * 0.5f);
	//	int radiusSquared = (int)(dimensions * dimensions * 0.25f);

	//	for (int y = -radius; y <= radius; y++)
	//	{
	//		for (int x = -radius; x <= radius; x++)
	//		{
	//			// Check if the current position (x, y) is within the circular area
	//			if (x * x + y * y <= radiusSquared)
	//			{
	//				Vector2 cellPosition = new Vector2(x, y) + (Vector2)transform.position;

	//				Cell newCell = Instantiate(cellObj, cellPosition, Quaternion.identity);
	//				newCell.CreateCell(false, tileObjects);
	//				gridComponents.Add(newCell);
	//			}
	//		}
	//	}
	//	CheckEntropy();
	//}
	#endregion
	void InitializeGridRect()
	{
		int halfDimensions = (int)(dimensions * 0.5f);

		// Create an empty GameObject to serve as the parent
		GameObject cellParent = new GameObject("CellParent");

		for (int y = -halfDimensions; y <= halfDimensions; y++)
		{
			for (int x = -halfDimensions; x <= halfDimensions; x++)
			{
				Vector2 cellPosition = (Vector2)transform.position + new Vector2(x, y);
				Cell newCell = Instantiate(cellObj, cellPosition, Quaternion.identity);
				newCell.CreateCell(false, tileObjects);

				// Make the newly created cell a child of the gridParent
				newCell.transform.parent = cellParent.transform;

				gridComponents.Add(newCell);
			}
		}

		// After creating all cells, make the gridParent a child of the current GameObject (script's GameObject)
		cellParent.transform.parent = transform;

		CheckEntropy();
	}


	void CheckEntropy()
	{
		List<Cell> tempGrid = new List<Cell>(gridComponents);

		tempGrid.RemoveAll(c => c.collapsed);

		tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

		int arrLength = tempGrid[0].tileOptions.Length;
		int stopIndex = default;

		for (int i = 1; i < tempGrid.Count; i++)
		{
			if (tempGrid[i].tileOptions.Length > arrLength)
			{
				stopIndex = i;
				break;
			}
		}

		if (stopIndex > 0)
		{
			tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
		}
		CollapseCell(tempGrid);
	}

	private GameObject tileParent;
	void CollapseCell(List<Cell> tempGrid)
	{
		// Create an empty GameObject to serve as the parent
		if (tileParent == null)
		{
			tileParent = new GameObject("TileParent");
			tileParent.transform.parent = transform;
		}

		int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

		Cell cellToCollapse = tempGrid[randIndex];

		cellToCollapse.collapsed = true;
		Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
		cellToCollapse.tileOptions = new Tile[] { selectedTile };

		Tile foundTile = cellToCollapse.tileOptions[0];
		Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity, tileParent.transform);

		UpdateGeneration();
	}

	void UpdateGeneration()
	{
		List<Cell> newGenerationCell = new List<Cell>(gridComponents);

		for (int y = 0; y < dimensions; y++)
		{
			for (int x = 0; x < dimensions; x++)
			{
				var index = x + y * dimensions;
				if (gridComponents[index].collapsed)
				{
					newGenerationCell[index] = gridComponents[index];
				}
				else
				{
					List<Tile> options = new List<Tile>();
					foreach (Tile t in tileObjects)
					{
						options.Add(t);
					}

					//update above
					if (y > 0)
					{
						Cell up = gridComponents[x + (y - 1) * dimensions];
						List<Tile> validOptions = new List<Tile>();

						foreach (Tile possibleOptions in up.tileOptions)
						{
							var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
							var valid = tileObjects[valOption].upNeighbours;

							validOptions = validOptions.Concat(valid).ToList();
						}

						CheckValidity(options, validOptions);
					}

					//update right
					if (x < dimensions - 1)
					{
						Cell right = gridComponents[x + 1 + y * dimensions];
						List<Tile> validOptions = new List<Tile>();

						foreach (Tile possibleOptions in right.tileOptions)
						{
							var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
							var valid = tileObjects[valOption].leftNeighbours;

							validOptions = validOptions.Concat(valid).ToList();
						}

						CheckValidity(options, validOptions);
					}

					//look down
					if (y < dimensions - 1)
					{
						Cell down = gridComponents[x + (y + 1) * dimensions];
						List<Tile> validOptions = new List<Tile>();

						foreach (Tile possibleOptions in down.tileOptions)
						{
							var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
							var valid = tileObjects[valOption].downNeighbours;

							validOptions = validOptions.Concat(valid).ToList();
						}

						CheckValidity(options, validOptions);
					}

					//look left
					if (x > 0)
					{
						Cell left = gridComponents[x - 1 + y * dimensions];
						List<Tile> validOptions = new List<Tile>();

						foreach (Tile possibleOptions in left.tileOptions)
						{
							var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
							var valid = tileObjects[valOption].rightNeighbours;

							validOptions = validOptions.Concat(valid).ToList();
						}

						CheckValidity(options, validOptions);
					}

					Tile[] newTileList = new Tile[options.Count];

					for (int i = 0; i < options.Count; i++)
					{
						newTileList[i] = options[i];
					}

					newGenerationCell[index].RecreateCell(newTileList);
				}
			}
		}

		gridComponents = newGenerationCell;
		iterations++;

		if (iterations < dimensions * dimensions)
		{
			CheckEntropy();
		}

	}

	void CheckValidity(List<Tile> optionList, List<Tile> validOption)
	{
		for (int x = optionList.Count - 1; x >= 0; x--)
		{
			var element = optionList[x];
			if (!validOption.Contains(element))
			{
				optionList.RemoveAt(x);
			}
		}
	}
}

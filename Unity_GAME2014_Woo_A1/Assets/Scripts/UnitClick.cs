using UnityEngine;

public class UnitClick : MonoBehaviour
{
	private Camera myCam;

	public LayerMask guardians;
	public LayerMask others;

	void Start()
	{
		myCam = Camera.main;
	}


	void Update()
	{
		if (Input.GetMouseButton(0)) 
		{
			Vector2 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);

			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, guardians);

			if (hit.collider != null)
			{
				UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
			}
			else
			{
				// No guardian clicked, check for background click
				hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, others);
				if (hit.collider != null)
				{
					UnitSelections.Instance.DeselectAll();
				}
			}
		}
	}
}
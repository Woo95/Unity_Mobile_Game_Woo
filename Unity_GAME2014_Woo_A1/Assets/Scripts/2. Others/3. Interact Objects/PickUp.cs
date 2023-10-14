using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eResourceType { Gold, Resource }
public class PickUp : MonoBehaviour
{
    FieldManager m_fieldManager;
    public int touchCount;
    public eResourceType type;
    SpriteRenderer m_SpriteRenderer;
    public void InitData(FieldManager fieldManager)
    {
		m_fieldManager = fieldManager;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sortingOrder = (int)(transform.position.y * -100.0f);
		gameObject.SetActive(true);
    }

	private void OnMouseDown()
	{
        touchCount--;

        if (touchCount <= 0)
        {
            m_fieldManager.DestroyPickUp(this);
            Destroy(gameObject);
		}
	}
}

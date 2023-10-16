using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGuardianType { G1, G2, G3 }
public class Guardian : MonoBehaviour
{
    public GuardianData m_GuardianData;
    SpriteRenderer m_SpriteRenderer;
	Transform trans;

	public bool m_isSelected;

	public void SetData()
    {
        gameObject.SetActive(true);
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        trans = transform;
		m_isSelected = false;

		Debug.Log("Add");
        //UnitSelections.Instance.unitList.Add(this.gameObject);
	}

	public void SetSelect(bool isSelected)
	{
		m_isSelected = isSelected;
		m_SpriteRenderer.color = m_isSelected ? Color.gray : Color.white;
	}

	void Update()
    {
        m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
    }

	public void OnMouseDown()
	{
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
		GrabManager.instance.ClickSelect(gameObject);
	}

	private void OnDestroy()
	{
		GrabManager.instance.Deselect(gameObject);
	}
}

[System.Serializable]
public class GuardianData
{
    public eGuardianType guardianType;

	public float    health;
	public float    speed;
	public float    damage;
	public int      cost;
	public float    searchRadius;
	public float    attackRadius;
	public float    ATTACK_TIME;
}

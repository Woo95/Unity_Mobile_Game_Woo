using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGuardianType { G1, G2, G3 }
public class Guardian : MonoBehaviour
{
    public GuardianData m_GuardianData;
    SpriteRenderer m_SpriteRenderer;
	Transform trans;

	public void SetData()
    {
        gameObject.SetActive(true);
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        trans = transform;

        UnitSelections.Instance.unitList.Add(this.gameObject);
	}

	private void Start()
	{
		SetData();
	}

	void Update()
    {
        m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
    }

	private void OnDestroy()
	{
		UnitSelections.Instance.unitList.Remove(this.gameObject);
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

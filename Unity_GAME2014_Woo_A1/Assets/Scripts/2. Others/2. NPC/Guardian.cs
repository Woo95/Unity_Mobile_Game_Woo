using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGuardianType { G1, G2, G3 }
public class Guardian : MonoBehaviour
{
    public eGuardianType m_GuardianType;
    SpriteRenderer m_SpriteRenderer;
	Transform trans;

	public void SetData(eGuardianType guardianType)
    {
        gameObject.SetActive(true);
        m_GuardianType = guardianType;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        trans = transform;

        //UnitSelections.Instance.unitList.Add(this.gameObject);
	}

    void Start() // ¼öÁ¤
    {
        SetData(m_GuardianType);
	}

    void Update()
    {
        m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
    }

    //private void OnDestroy()
    //{
    //    UnitSelections.Instance.unitList.Remove(this.gameObject);
    //}
}

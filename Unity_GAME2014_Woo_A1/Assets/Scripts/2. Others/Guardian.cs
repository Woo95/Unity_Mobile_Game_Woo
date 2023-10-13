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
        m_GuardianType = guardianType;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        trans = transform;
	}

    void Start()
    {
        SetData(eGuardianType.G1);
	}

    void Update()
    {
        m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
    }
}

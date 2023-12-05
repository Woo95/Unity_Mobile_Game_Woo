using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum eButtonActionType
{
	MoveLeft,
	MoveRight,
	Jump
}

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	PlayerController m_Controller;
	void Start()
	{
		m_Controller = PlayerManager.instance.m_Controller;
	}


	public eButtonActionType m_ActionType;
	public void OnPointerDown(PointerEventData eventData)
	{
		m_Controller.HandleButtonAction(m_ActionType, true);
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		m_Controller.HandleButtonAction(m_ActionType, false);
	}
}

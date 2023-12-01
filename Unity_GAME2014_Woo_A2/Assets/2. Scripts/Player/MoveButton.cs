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
	public eButtonActionType m_ActionType;

	public void OnPointerDown(PointerEventData eventData)
	{
		Player.instance.HandleButtonAction(m_ActionType, true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Player.instance.HandleButtonAction(m_ActionType, false);
	}
}

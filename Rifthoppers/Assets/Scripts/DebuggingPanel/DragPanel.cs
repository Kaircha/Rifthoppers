using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
  public RectTransform rect;
  public Canvas canvas;

  public void OnBeginDrag(PointerEventData eventData)
  {
    GameManager.Instance.preventShooting = true;
  }

  public void OnDrag(PointerEventData eventData)
  {
    rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    GameManager.Instance.preventShooting = false;
  }
}

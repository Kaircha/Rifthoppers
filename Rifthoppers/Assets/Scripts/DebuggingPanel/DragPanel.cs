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

  private float timeMulti, timeScale;

  public void OnExpand(){
    timeMulti = RiftManager.Instance.SpeedMultiplier;
    timeScale = Time.timeScale;
    RiftManager.Instance.SpeedMultiplier = Time.timeScale = 0;
  }

  public void OnCollapse(){
    RiftManager.Instance.SpeedMultiplier = timeMulti;
    Time.timeScale = timeScale;
  }
}

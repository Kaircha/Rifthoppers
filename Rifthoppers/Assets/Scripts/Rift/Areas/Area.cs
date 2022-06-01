using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {
  public float Radius = 20f;
  public SpriteRenderer Renderer;

  public void Show() {
    gameObject.SetActive(true);
    RiftManager.Instance.RiftEdge.transform.localScale = 0.05f * Radius * Vector3.one;
    RiftManager.Instance.RiftMask.transform.localScale = 0.05f * Radius * Vector3.one;
  }

  public void Hide() {
    gameObject.SetActive(false);
  }
}
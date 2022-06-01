using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalRoom : MonoBehaviour {
  public CinemachineVirtualCamera VirtualCamera;
  public List<SpriteMask> Masks;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.TryGetComponent(out PlayerEntity entity)) {
      // Doesn't work for Local Co-op, but this is mostly a temporary effect anyway
      VirtualCamera.Priority = 100;
      Masks.ForEach(mask => mask.gameObject.SetActive(false));
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.TryGetComponent(out PlayerEntity entity)) {
      VirtualCamera.Priority = 0;
      Masks.ForEach(mask => mask.gameObject.SetActive(true));
    }
  }

  public void Exit() => VirtualCamera.Priority = 0;
}
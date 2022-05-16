using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalRoom : MonoBehaviour {
  public CinemachineVirtualCamera VirtualCamera;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.TryGetComponent(out PlayerEntity entity)) {
      // Doesn't work for Local Co-op, but this is mostly a temporary effect anyway
      VirtualCamera.Priority = 100;
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.TryGetComponent(out PlayerEntity entity)) {
      VirtualCamera.Priority = 0;
    }
  }

  public void Exit() => VirtualCamera.Priority = 0;
}
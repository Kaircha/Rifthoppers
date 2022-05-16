using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
  public Collider2D Collider;
  public bool IsActive = false;
  public PortalRoom Room;

  public IEnumerator Start() {
    yield return new WaitForSeconds(2f);
    IsActive = true;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (!IsActive) return;
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity) && entity.CompareTag("Player")) {
      // Use nice animations later.
      StartCoroutine(GameManager.Instance.LabToWave());
      Room.Exit();
      IsActive = false;
    } else {
      // Also make sure that everything actually shows up in the Rift.
      collision.attachedRigidbody.gameObject.SetActive(false);
    }
  }
}
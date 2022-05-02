using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.TryGetComponent(out Entity entity)) {
      // Use nice animations later. Also make sure that everything actually shows up in the Rift.
      if (entity.CompareTag("Player")) {
        SceneManager.LoadScene("Rift");

        // Temporary
        foreach (Player player in LobbyManager.Instance.Players) {
          player.Entity.EnterRiftState();
          player.Entity.transform.position = Vector3.zero;
        }

      }
      else entity.gameObject.SetActive(false);
    } 
  }
}
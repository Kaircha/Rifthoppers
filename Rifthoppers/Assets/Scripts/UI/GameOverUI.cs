using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
  public void RetryButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  public void ContinueButton() {
    RiftManager.Instance.Energy.Revive();
    GameObject.FindWithTag("Player").GetComponent<Health>().Revive();
  }
}

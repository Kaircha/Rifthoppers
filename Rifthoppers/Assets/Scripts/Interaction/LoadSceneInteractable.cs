using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneInteractable : MonoBehaviour, IInteractable {
  public int Scene;

  public void ShowHighlight() {
    transform.localScale = 1.5f * Vector3.one;
  }

  public void HideHighlight() {
    transform.localScale = Vector3.one;
  }

  public void Interact(PlayerEntity interactor) {
    SceneManager.LoadScene(Scene);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable {
  public void ShowHighlight() {
    transform.localScale = 1.5f * Vector3.one;
  }

  public void HideHighlight() {
    transform.localScale = Vector3.one;
  }

  public void Interact(PlayerEntity interactor) {
    GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
  }
}

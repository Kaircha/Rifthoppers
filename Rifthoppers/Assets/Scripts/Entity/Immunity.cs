using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : MonoBehaviour {
  public List<Collider2D> Colliders = new();

  public IEnumerator ImmunityRoutine(float duration) {
    Activate();
    yield return new WaitForSeconds(duration);
    Deactivate();
  }

  public void Activate() {
    foreach (Collider2D collider in Colliders) collider.enabled = false;
  }
  public void Deactivate() {
    foreach (Collider2D collider in Colliders) collider.enabled = true;
  }
}
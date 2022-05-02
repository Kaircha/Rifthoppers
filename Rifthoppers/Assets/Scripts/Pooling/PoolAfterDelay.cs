using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAfterDelay : MonoBehaviour, IPoolable {
  public Pool Pool { get; set; }
  public float Delay;

  private void OnEnable() => StartCoroutine(PoolRoutine());
  private void OnDisable() => StopAllCoroutines();
  IEnumerator PoolRoutine() {
    yield return new WaitForSeconds(Delay);
    (this as IPoolable).Release(gameObject);
  }
}
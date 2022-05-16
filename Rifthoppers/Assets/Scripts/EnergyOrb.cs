using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour, IPoolable {
  public float Energy = 30;
  public int Progress = 25;

  public float SpeedMulti = 10;

  public Pool Pool { get; set; }

  private void OnEnable() => RiftManager.Instance.OnWaveEnded += DestroyOnWaveEnded;
  private void OnDisable() => RiftManager.Instance.OnWaveEnded -= DestroyOnWaveEnded;

  public void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody.CompareTag("Player")) {
      StopAllCoroutines();
      StartCoroutine(PickupRoutine(collider.transform));
    }
  }

  IEnumerator PickupRoutine(Transform target) {
    Vector3 origin = transform.position;
    float timer = 0f;

    while (timer < 1f) {
      transform.position = Vector3.Lerp(origin, target.transform.position + Vector3.up, timer);
      timer += Time.deltaTime * SpeedMulti;
      yield return null;
    }

    RiftManager.Instance.Energy.Heal(Energy);
    RiftManager.Instance.Experience.Learn(Progress);

    (this as IPoolable).Release(gameObject);
  }

  public void DestroyOnWaveEnded() => (this as IPoolable).Release(gameObject);
}

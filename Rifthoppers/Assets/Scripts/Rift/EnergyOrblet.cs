using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrblet : MonoBehaviour, IPoolable {
  public float Energy = 3;
  public int Experience = 1;
  public AudioClip AudioClip;
  public float SpeedMulti = 10;

  public Pool Pool { get; set; }

  private void OnEnable() => RiftManager.Instance.OnEncounterEnded += DestroyOnWaveEnded;
  private void OnDisable() => RiftManager.Instance.OnEncounterEnded -= DestroyOnWaveEnded;

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

    Collect();
  }

  public void Collect() {
    PoolManager.Instance.SoundEffects.Objects.Get().GetComponent<SoundEffect>().Play(AudioClip, 0.2f, 0.5f + RiftManager.Instance.Encounter.Progress);

    RiftManager.Instance.EnergyCollected(Energy, false);
    RiftManager.Instance.ExperienceCollected(Experience);
    // Play collection effect

    (this as IPoolable).Release(gameObject);
  }

  public void DestroyOnWaveEnded() => (this as IPoolable).Release(gameObject);
}
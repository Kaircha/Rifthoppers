using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour, IPoolable {
  public float Energy = 30;
  public int Experience = 25;
  public AudioClip AudioClip;

  public Pool Pool { get; set; }

  private void OnEnable() {
    RiftManager.Instance.EnergyOrbSpawned();
    RiftManager.Instance.OnEncounterEnded += DestroyOnWaveEnded;
  }
  private void OnDisable() => RiftManager.Instance.OnEncounterEnded -= DestroyOnWaveEnded;
  private void OnTriggerEnter2D(Collider2D collision) => Collect();

  public void Collect() {
    PoolManager.Instance.SoundEffects.Objects.Get().GetComponent<SoundEffect>().Play(AudioClip, 1f, 0.5f + RiftManager.Instance.Encounter.Progress);

    RiftManager.Instance.Energy.Heal(Energy);
    RiftManager.Instance.EnergyCollected(Energy);
    RiftManager.Instance.ExperienceCollected(Experience);

    (this as IPoolable).Release(gameObject);
  }

  public void DestroyOnWaveEnded() => (this as IPoolable).Release(gameObject);
}
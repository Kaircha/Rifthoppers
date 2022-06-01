using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour, IPoolable {
  public float Energy = 30;
  public int Experience = 25;
  public AudioClip AudioClip;

  public Pool Pool { get; set; }

  private void OnEnable() => RiftManager.Instance.EnergyOrbSpawned();
  private void OnTriggerEnter2D(Collider2D collision) => Collect();

  public void Collect() {
    PoolManager.Instance.SoundEffects.Objects.Get().GetComponent<SoundEffect>().Play(AudioClip, 1f, 0.5f + RiftManager.Instance.Encounter.Progress);

    RiftManager.Instance.EnergyCollected(Energy, true);
    RiftManager.Instance.ExperienceCollected(Experience);

    (this as IPoolable).Release(gameObject);
  }
}
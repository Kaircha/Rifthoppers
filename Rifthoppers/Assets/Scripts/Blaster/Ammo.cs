using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour, IPoolable {
  [HideInInspector] public Rigidbody2D Rigidbody;
  [HideInInspector] public Collider2D Collider;
  [HideInInspector] public Entity Entity;
  [HideInInspector] public AmmoStats Stats;

  public Pool Pool { get; set; }

  public void Initialize(Entity entity, AmmoStats stats) {
    Entity = entity;
    Stats = stats;
  }


  public void OnEnable() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Collider = GetComponent<Collider2D>();
    RiftManager.Instance.OnEncounterEnded += DestroyOnWaveEnded;
  }
  private void OnDisable() => RiftManager.Instance.OnEncounterEnded -= DestroyOnWaveEnded;
  public void DestroyOnWaveEnded() => (this as IPoolable).Release(gameObject);

  public void FixedUpdate() => Homing();

  public abstract void Homing();
  public abstract void Reflect(Collision2D collision);
  public abstract void Pierce(Collider2D collider);
  public abstract void Split(Collider2D collider);
}
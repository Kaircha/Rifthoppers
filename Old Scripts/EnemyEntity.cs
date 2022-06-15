using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity, IPoolable {
  public int minOrbs, maxOrbs;
  public float BaseHP = 40;
  public float Cost;
  public event Action OnSpawn;

  public Pool Pool { get; set; }

  public void HandleSpawn() {
    (Health as Health).Maximum = BaseHP;
    Health.Revive();
    RemoveEffects();
    OnSpawn?.Invoke();
  }

  public override void OnDeath(Entity entity) {
    int nr = UnityEngine.Random.Range(minOrbs, maxOrbs + 1);

    while (nr-- > 0) SpawnOrblet();
  }
  
  private void SpawnOrblet() {
    GameObject orblet = PoolManager.Instance.EnergyOrblets.Objects.Get();
    orblet.transform.position = transform.position;
    orblet.GetComponent<Rigidbody2D>().AddForce(5f * UnityEngine.Random.insideUnitCircle, ForceMode2D.Impulse);
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody != null && collider.attachedRigidbody.CompareTag("Player") && collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
      entity.Health.Hurt(this, entity, 5f, false);
      collider.attachedRigidbody.AddForce(50f * Direction, ForceMode2D.Impulse);
    }
  }
}
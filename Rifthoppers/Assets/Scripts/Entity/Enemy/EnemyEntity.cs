using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity, IPoolable {

  public int minOrbs, maxOrbs;
  public float BaseHP = 40;
  public event Action OnSpawn;
  public GameObject Orb;

  public Pool Pool { get; set; }

  public void HandleSpawn() {
    (Health as Health).Maximum = BaseHP * RiftManager.Instance.DifficultyMultiplier;
    Health.Revive();
    RemoveEffects();
    OnSpawn?.Invoke();
  }

  public override void OnDeath(Entity entity) {
    //RiftManager.Instance.Experience.Learn((int)Experience);
    int nr = UnityEngine.Random.Range(minOrbs, maxOrbs + 1);

    while (nr-- > 0) SpawnOrb();
  }
  
  private void SpawnOrb()
  {
    Instantiate(Orb, transform.position, Quaternion.identity);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.transform.CompareTag("Player") && collision.gameObject.TryGetComponent(out Entity entity)) {
      entity.Health.Hurt(this, entity, 10f * RiftManager.Instance.DifficultyMultiplier, false);
      collision.rigidbody.AddForce(50f * Direction, ForceMode2D.Impulse);
    }
  }
}
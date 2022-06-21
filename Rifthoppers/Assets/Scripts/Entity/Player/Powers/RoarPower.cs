using Cinemachine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoarPower : Power {
  public ParticleSystem RoarSystem;
  public AudioSource RoarSource;
  private CinemachineImpulseSource ImpulseSource;

  private void Awake() {
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
  }

  public override void Press() {
    RoarSystem.Play();
    RoarSource.Play();
    ImpulseSource.GenerateImpulse(0.5f);

    Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, Range, LayerMask.GetMask("Enemy"));
    List<Entity> enemies = new();
    foreach (Collider2D collider in nearbyEnemies) {
      if (!collider.isTrigger) continue;
      if (collider.attachedRigidbody == null) continue;
      if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) enemies.Add(entity);
    }
    foreach (Entity enemy in enemies.Distinct()) {
      Vector2 direction = (enemy.transform.position - transform.position).normalized;
      enemy.Rigidbody.AddForce(3f * enemy.Rigidbody.mass * Strength * direction, ForceMode2D.Impulse);
      enemy.AddEffect(new StunEffect(2f));
    }
  }

  public override void Hold() { }

  public override void Release() { }
}

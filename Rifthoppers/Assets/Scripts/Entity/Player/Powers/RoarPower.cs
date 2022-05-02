using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoarPower : Power {
  public LayerMask TargetMask;
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

    foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, Range, TargetMask)) {
      if (!collider.isTrigger) continue;
      if (collider.TryGetComponent(out Rigidbody2D rigidbody)) {
        Vector2 direction = (collider.transform.position - transform.position).normalized;
        rigidbody.AddForce(10 * Strength * direction, ForceMode2D.Impulse);
      }
    }
  }

  public override void Hold() { }

  public override void Release() { }
}

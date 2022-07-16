using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour, IPoolable {
  public CircleCollider2D Collider;
  public SpriteRenderer Renderer;
  public bool DoRotation = true;
  public bool DoBehavior = true;
  public float Angle = 0f;

  public event Action<Entity> OnOrbitalCollide;
  public Pool Pool { get; set; }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (!DoBehavior) return;
    if (collider.attachedRigidbody == null) return;
    if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
      OnOrbitalCollide?.Invoke(entity);
    }
  }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour, IPoolable {
  public CircleCollider2D Collider;
  public SpriteRenderer Renderer;
  public bool KeepUpright = true;

  public event Action<Entity> OnOrbitalCollide;
  public Pool Pool { get; set; }

  public void Initialize(float radius, Sprite sprite) {
    Collider.radius = radius;
    Renderer.sprite = sprite;
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody == null) return;
    if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
      OnOrbitalCollide?.Invoke(entity);
    }
  }
}
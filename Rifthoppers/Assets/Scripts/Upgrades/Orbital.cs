using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour{
  public event Action<Entity> OnOrbitalCollide;
  private float Speed = 100;
  private float Radius = 0.3f;

  private void Start() => transform.position = new Vector3(0, Radius, 0);

  public void Reinitialize(float speed, float radius) {
    Speed = speed;
    Radius = radius;
    transform.position = new Vector3(0, Radius, 0);
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody != null && collider.attachedRigidbody.TryGetComponent(out Entity entity) && !(entity is PlayerEntity))
      OnOrbitalCollide?.Invoke(entity);
  }
  private void FixedUpdate() {
    transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), Speed * Time.fixedDeltaTime);
    transform.rotation = Quaternion.identity;
  }
}
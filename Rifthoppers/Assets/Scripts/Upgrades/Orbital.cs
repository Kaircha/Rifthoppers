using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour{

  public event Action<Entity> onOrbitalCollide;
  private float speed = 100;
  private float radius = 0.3f;

  private void Start() => transform.position = new Vector3(0, radius, 0);

  private void OnTriggerStay2D(Collider2D collider){
    if (collider.attachedRigidbody.gameObject.TryGetComponent(out Entity entity) && !(entity is PlayerEntity))
      onOrbitalCollide?.Invoke(entity);
  }
  private void FixedUpdate(){
    transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), speed * Time.fixedDeltaTime);
    transform.rotation = Quaternion.identity;
  }
}

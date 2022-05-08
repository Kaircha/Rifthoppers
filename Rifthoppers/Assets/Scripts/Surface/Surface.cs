using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Surface : MonoBehaviour {
  public List<Entity> Entities = new();
  // Sound made by walking on the surface

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      Entities.Add(entity);
      // Set Entity shader to be partially submerged
      entity.OnSurfaceWalked += SurfaceEffect;
    }
  }
  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      Entities.Remove(entity);
      // Set Entity shader to be no longer submerged
      entity.OnSurfaceWalked -= SurfaceEffect;
    } 
  }

  public void Update() => Entities.ForEach(entity => entity.SurfaceWalked(entity, this));

  public abstract void SurfaceEffect(Entity entity, Surface surface);
}
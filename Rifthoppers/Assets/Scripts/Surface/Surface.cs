using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Surface : MonoBehaviour {
  public List<Entity> Entities = new();
  public float Depth;
  // Sound made by walking on the surface

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      Entities.Add(entity);
      entity.Sprite.material.SetFloat("_Submersion", Depth);
      entity.OnSurfaceWalked += SurfaceEffect;
    }
  }
  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      Entities.Remove(entity);
      entity.Sprite.material.SetFloat("_Submersion", 0);
      entity.OnSurfaceWalked -= SurfaceEffect;
    } 
  }

  public void Update() => Entities.ForEach(entity => entity.SurfaceWalked(entity, this));
  
  public abstract void SurfaceEffect(Entity entity, Surface surface);
}
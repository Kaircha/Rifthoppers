using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Surface : MonoBehaviour {
  public List<Entity> Entities = new();
  public float Depth;
  public bool CanHurtFlying = false;
  // Sound made by walking on the surface

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity) && (CanHurtFlying || !entity.IsFlying)) {
      Entities.Add(entity);
      entity.Sprite.material.SetFloat("_Submersion", Depth);
      entity.OnSurfaceWalked += SurfaceEffect;
    }
  }
  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity) && Entities.Contains(entity)) {
      Entities.Remove(entity);
      entity.Sprite.material.SetFloat("_Submersion", 0);
      entity.OnSurfaceWalked -= SurfaceEffect;
    } 
  }
  private void OnDisable() {
    foreach (Entity entity in Entities) {
      entity.Sprite.material.SetFloat("_Submersion", 0);
      entity.OnSurfaceWalked -= SurfaceEffect;
    }
    Entities = new();
  }

  public void Update() => Entities.ForEach(entity => entity.SurfaceWalked(entity, this));
  
  public abstract void SurfaceEffect(Entity entity, Surface surface);
}
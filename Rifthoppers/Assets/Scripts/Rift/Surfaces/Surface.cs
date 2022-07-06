using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Surface : MonoBehaviour {
  public List<Entity> Entities = new();
  public float Depth;
  public LayerMask Mask;
  // Sound made by walking on the surface

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity)) {
      if (((1 << entity.gameObject.layer) & Mask) == 0) return;
      Entities.Add(entity);
      SurfaceEnter(entity);
      entity.Sprite.material.SetFloat("_Submersion", Depth);
    }
  }
  public void Update() => Entities.ForEach(entity => SurfaceStay(entity));
  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.attachedRigidbody.TryGetComponent(out Entity entity) && Entities.Contains(entity)) {
      Entities.Remove(entity);
      SurfaceExit(entity);
      entity.Sprite.material.SetFloat("_Submersion", 0);
    } 
  }
  private void OnDisable() {
    foreach (Entity entity in Entities) {
      SurfaceExit(entity);
      entity.Sprite.material.SetFloat("_Submersion", 0);
    }
    Entities = new();
  }


  public abstract void SurfaceEnter(Entity entity);
  public abstract void SurfaceStay(Entity entity);
  public abstract void SurfaceExit(Entity entity);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSurface : Surface {

  public float damage = 1;
  
  public override void SurfaceEffect(Entity entity, Surface surface) {
    Debug.Log($"{entity.name} walked on {surface.name}.");
  }

  public new void Update(){
    base.Update();
    foreach (Entity entity in Entities){
      Debug.Log("damaging " + entity.name);
      entity.Hurt(null, entity, damage * Time.deltaTime, true);
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSurface : Surface {
  public float Damage = 3f;
  
  public override void SurfaceEffect(Entity entity, Surface surface) {

    if(!(entity is PlayerEntity) || !entity.Stats.isFlying)
      entity.Health.Hurt(null, entity, Damage * Time.deltaTime, true);
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSurface : Surface {
  public float Damage = 3f;

  public override void SurfaceEnter(Entity entity) { }

  public override void SurfaceStay(Entity entity) {
    entity.Health.Hurt(null, entity, Damage * Time.deltaTime, true);
  }

  public override void SurfaceExit(Entity entity) { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSurface : Surface {
  public override void SurfaceEffect(Entity entity, Surface surface) {
    Debug.Log($"{entity.name} walked on {surface.name}.");
  }
}
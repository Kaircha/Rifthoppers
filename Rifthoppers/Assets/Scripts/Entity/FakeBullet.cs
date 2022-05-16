using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBullet : Surface {
  public float Size = 0;
  public float Damage = 0;
  public Color color;

  public void AfterShoot(){
    Size = 0;
    transform.localScale = Vector3.zero;
  }

  private new void Update(){
    base.Update();

    if (transform.localScale.x < Size) {
      transform.localScale += Time.deltaTime * (Size - transform.localScale.x) * 6 * Vector3.one;
      if (transform.localScale.x > Size) transform.localScale = Size * Vector3.one;
    }
    else if (transform.localScale.x > Size){
      transform.localScale -= Time.deltaTime * (transform.localScale.x - Size) * 6 * Vector3.one;
      if (transform.localScale.x < Size) transform.localScale = Size * Vector3.one;
    }
  }
  public override void SurfaceEffect(Entity entity, Surface surface){
    if (entity is PlayerEntity) return;
      entity.Health.Hurt(null, entity, Damage * Time.deltaTime, true);
  }
}

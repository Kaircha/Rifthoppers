using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBullet : Surface
{
  public float Size = 0;
  public float Damage = 0;
  public Color color;

  private SpriteRenderer rend;

  private void Awake()
  {
    rend = GetComponent<SpriteRenderer>();
  }

  private new void Update(){
    base.Update();
    transform.localScale = Size * Vector3.one;
    rend.color = color;
  }
  public override void SurfaceEffect(Entity entity, Surface surface){

    if (entity is PlayerEntity) return;
    entity.Health.Hurt(null, entity, Damage * Time.deltaTime, true);
  }
}

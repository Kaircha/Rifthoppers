using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Should not be a Surface!
public class FakeBullet : Surface {
  public float Size = 0;
  public float Damage = 0;
  public SpriteRenderer Sprite;

  public void BeforeShoot(Color color) {
    Size = 0;
    transform.localScale = Vector3.zero;
    Sprite.color = color;
    gameObject.SetActive(true);
  }

  public void AfterShoot() {
    gameObject.SetActive(false);
  }

  private new void Update() {
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

  public override void SurfaceEnter(Entity entity) { }
  public override void SurfaceStay(Entity entity) {
    entity.Health.Hurt(null, entity, Damage * Time.deltaTime, true);
  }
  public override void SurfaceExit(Entity entity) { }
}

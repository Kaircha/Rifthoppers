using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedWeapon : Weapon
{
  private int charges = 0;
  private int maxCharges = 1;

  private Transform FakeBullet;

  public ChargedWeapon(Entity entity, Transform origin, int max){
    Entity = entity;
    ShootOrigin = origin;
    maxCharges = max;
  }

  public override void Shoot(){
    ++charges;
    Entity.Stats.FakeBullet.Size = GetScale() * Entity.Stats.ProjectileSizeMulti;
    if (charges == maxCharges)
      ShootBullet();
  }

  public void ShootBullet(){

    Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
    projectile.transform.position = ShootOrigin.position;
    projectile.transform.right = ShootOrigin.right;
    projectile.GetComponent<SpriteRenderer>().color = Entity.Stats.ProjectileColor;
    projectile.GetComponent<Projectile>().Shoot(Entity, charges, GetScale());
    charges = 0;

    Entity.Stats.FakeBullet.AfterShoot();
  }

  public override void ShootingStarted(){}
  public override void ShootingStopped() => ShootBullet();

  private float GetScale() => charges * 0.4f;
}
 
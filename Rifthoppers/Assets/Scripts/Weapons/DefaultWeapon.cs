using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon {
  public DefaultWeapon(Entity entity, Transform origin) {
    Entity = entity;
    ShootOrigin = origin;
  }

  public override void Shoot() {
    Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
    projectile.transform.position = ShootOrigin.position;
    projectile.transform.right = ShootOrigin.right;
    projectile.GetComponent<SpriteRenderer>().color = Entity.Stats.ProjectileColor;
    projectile.GetComponent<Projectile>().Shoot(Entity);
  }

  public override void ShootingStarted() { }
  public override void ShootingStopped() { }
}
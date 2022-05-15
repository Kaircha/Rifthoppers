using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Weapons/Default")]
public class DefaultWeapon : Weapon {
  public override void Shoot() {
    Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
    projectile.transform.position = Barrel.Origin.position;
    projectile.transform.right = Barrel.Origin.right;
    projectile.GetComponent<SpriteRenderer>().color = Entity.Stats.ProjectileColor;
    projectile.GetComponent<Projectile>().Shoot(Entity);

    Barrel.Rigidbody.AddForce(-10f * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right, ForceMode2D.Impulse);
    Barrel.ImpulseSource.GenerateImpulse(0.15f * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right);
    if (ShootSFX != null) {
      Barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      Barrel.AudioSource.PlayOneShot(ShootSFX);
    }
  }
  public override void ShootingStarted() { }
  public override void ShootingUpdated() => Shoot();
  public override void ShootingStopped() { }
}
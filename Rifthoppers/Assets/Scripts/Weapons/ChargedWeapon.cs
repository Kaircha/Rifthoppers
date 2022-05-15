using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charged Weapon", menuName = "Weapons/Charged")]
public class ChargedWeapon : Weapon {
  private int Charges = 0;
  public int MaxCharges = 10;

  private Transform FakeBullet;

  public override void Shoot() {
    Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
    projectile.transform.position = Barrel.Origin.position;
    projectile.transform.right = Barrel.Origin.right;
    projectile.GetComponent<SpriteRenderer>().color = Entity.Stats.ProjectileColor;
    projectile.GetComponent<Projectile>().Shoot(Entity, Charges, GetScale());

    Barrel.Rigidbody.AddForce(-10f * GetScale() * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right, ForceMode2D.Impulse);
    Barrel.ImpulseSource.GenerateImpulse(0.15f * GetScale() * Entity.Stats.ProjectileSizeMulti * Barrel.Origin.right);
    if (ShootSFX != null) {
      Barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      Barrel.AudioSource.PlayOneShot(ShootSFX);
    }

    Charges = 0;

    Entity.Stats.FakeBullet.AfterShoot();
  }
  public override void ShootingStarted() { }
  public override void ShootingUpdated() {
    ++Charges;
    Entity.Stats.FakeBullet.Size = GetScale() * Entity.Stats.ProjectileSizeMulti;
    if (Charges == MaxCharges) Shoot();
  }
  public override void ShootingStopped() => Shoot();

  private float GetScale() => Charges * 0.4f;
}
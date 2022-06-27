using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charged Weapon", menuName = "Weapons/Charged")]
public class ChargedWeapon : Weapon {
  public int MaxCharges = 10;

  public void Shoot(Entity entity, AmmoStats ammo, Barrel barrel) {
    GameObject defaultAmmo = PoolManager.Instance.Bullets.Objects.Get();
    // Setup bullet
    defaultAmmo.layer = entity.gameObject.layer;
    defaultAmmo.transform.position = barrel.Origin.position;
    defaultAmmo.transform.right = barrel.Origin.right;
    defaultAmmo.GetComponent<SpriteRenderer>().color = ammo.AmmoColor;
    defaultAmmo.GetComponent<DefaultAmmo>().Initialize(entity, ammo);
    defaultAmmo.transform.localScale = ammo.AmmoSize * GetScale(barrel.Charges) * Vector3.one;
    defaultAmmo.GetComponent<Rigidbody2D>().velocity = ammo.AmmoSpeed * defaultAmmo.transform.right;

    barrel.Rigidbody.AddForce(-10f * GetScale(barrel.Charges) * ammo.AmmoSize * barrel.Origin.right, ForceMode2D.Impulse);
    barrel.ImpulseSource.GenerateImpulse(0.15f * GetScale(barrel.Charges) * ammo.AmmoSize * barrel.Origin.right);
    if (ShootSFX != null) {
      barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      barrel.AudioSource.PlayOneShot(ShootSFX);
    }

    barrel.Charges = 0;
    barrel.FakeBullet.AfterShoot();
  }
  public override void ShootingStarted(Entity entity, AmmoStats ammo, Barrel barrel) {
    barrel.Charges = 0;
    barrel.FakeBullet.BeforeShoot(ammo.AmmoColor);
  }

  public override void ShootingUpdated(Entity entity, AmmoStats ammo, Barrel barrel) {
    barrel.Charges++;
    barrel.FakeBullet.Size = GetScale(barrel.Charges) * ammo.AmmoSize;
    if (barrel.Charges == MaxCharges) Shoot(entity, ammo, barrel);
  }
  public override void ShootingStopped(Entity entity, AmmoStats ammo, Barrel barrel) {
    if (barrel.Charges > 0) Shoot(entity, ammo, barrel);
  }

  private float GetScale(int charges) => 0.6f + charges * 0.4f;
}
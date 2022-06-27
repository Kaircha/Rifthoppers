using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Worm Weapon", menuName = "Weapons/Worm")]
public class WormWeapon : Weapon {
  public float OrbChance = 10f;

  public void Shoot(Entity entity, AmmoStats ammo, Barrel barrel) {
    if (Chance.Percent(OrbChance)) {
      GameObject orb = PoolManager.Instance.EnergyOrbs.Objects.Get();
      orb.transform.position = barrel.Origin.position;
      orb.transform.right = barrel.Origin.right;
      orb.transform.localScale = ammo.AmmoSize * Vector3.one;
      orb.GetComponent<Rigidbody2D>().velocity = ammo.AmmoSpeed * orb.transform.right;
    } else {
      GameObject bullet = PoolManager.Instance.Bullets.Objects.Get();
      bullet.layer = entity.gameObject.layer;
      bullet.transform.position = barrel.Origin.position;
      bullet.transform.right = barrel.Origin.right;
      bullet.GetComponent<SpriteRenderer>().color = ammo.AmmoColor;
      bullet.GetComponent<DefaultAmmo>().Initialize(entity, ammo);
      bullet.transform.localScale = ammo.AmmoSize * Vector3.one;
      bullet.GetComponent<Rigidbody2D>().velocity = ammo.AmmoSpeed * bullet.transform.right;
    }

    barrel.Rigidbody.AddForce(-10f * ammo.AmmoSize * barrel.Origin.right, ForceMode2D.Impulse);
    barrel.ImpulseSource.GenerateImpulse(0.15f * ammo.AmmoSize * barrel.Origin.right);
    if (ShootSFX != null) {
      barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      barrel.AudioSource.PlayOneShot(ShootSFX);
    }
  }

  public override void ShootingStarted(Entity entity, AmmoStats ammo, Barrel barrel) { }
  public override void ShootingUpdated(Entity entity, AmmoStats ammo, Barrel barrel) => Shoot(entity, ammo, barrel);
  public override void ShootingStopped(Entity entity, AmmoStats ammo, Barrel barrel) { }
}
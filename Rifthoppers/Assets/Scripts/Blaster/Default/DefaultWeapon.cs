using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Weapons/Default")]
public class DefaultWeapon : Weapon {
  public void Shoot(Entity entity, AmmoStats ammo, Barrel barrel) {
    GameObject defaultAmmo = PoolManager.Instance.Bullets.Objects.Get();
    // Setup bullet
    defaultAmmo.layer = ammo.gameObject.layer;
    defaultAmmo.transform.position = barrel.Origin.position;
    defaultAmmo.transform.right = barrel.Origin.right;
    defaultAmmo.GetComponent<SpriteRenderer>().color = ammo.AmmoColor;
    defaultAmmo.GetComponent<DefaultAmmo>().Initialize(entity, ammo);
    defaultAmmo.transform.localScale = ammo.AmmoSize * Vector3.one;
    defaultAmmo.GetComponent<Rigidbody2D>().velocity = ammo.AmmoSpeed * defaultAmmo.transform.right;

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Weapons/Default")]
public class DefaultWeapon : Weapon {
  public void Shoot(PlayerBrain brain, Barrel barrel) {
    GameObject bullet = PoolManager.Instance.Bullets.Objects.Get();
    // Setup bullet
    bullet.transform.position = barrel.Origin.position;
    bullet.transform.right = barrel.Origin.right;
    bullet.GetComponent<SpriteRenderer>().color = brain.Stats.AmmoColor;
    bullet.GetComponent<DefaultAmmo>().Brain = brain;
    // Shoot bullet

    barrel.Rigidbody.AddForce(-10f * brain.Stats.AmmoSize * barrel.Origin.right, ForceMode2D.Impulse);
    barrel.ImpulseSource.GenerateImpulse(0.15f * brain.Stats.AmmoSize * barrel.Origin.right);
    if (ShootSFX != null) {
      barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      barrel.AudioSource.PlayOneShot(ShootSFX);
    }
  }
  public override void ShootingStarted(PlayerBrain brain, Barrel barrel) { }
  public override void ShootingUpdated(PlayerBrain brain, Barrel barrel) => Shoot(brain, barrel);
  public override void ShootingStopped(PlayerBrain brain, Barrel barrel) { }
}
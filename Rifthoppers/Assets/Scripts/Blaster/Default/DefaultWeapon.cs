using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Weapons/Default")]
public class DefaultWeapon : Weapon {
  public void Shoot(PlayerBrain brain, Barrel barrel) {
    GameObject defaultAmmo = PoolManager.Instance.Bullets.Objects.Get();
    // Setup bullet
    defaultAmmo.layer = brain.gameObject.layer;
    defaultAmmo.transform.position = barrel.Origin.position;
    defaultAmmo.transform.right = barrel.Origin.right;
    defaultAmmo.GetComponent<SpriteRenderer>().color = brain.Stats.AmmoColor;
    defaultAmmo.GetComponent<DefaultAmmo>().Brain = brain;
    defaultAmmo.GetComponent<Rigidbody2D>().velocity = brain.Stats.AmmoSpeed * defaultAmmo.transform.right;

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
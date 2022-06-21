using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charged Weapon", menuName = "Weapons/Charged")]
public class ChargedWeapon : Weapon {
  private int Charges = 0;
  public int MaxCharges = 10;

  public void Shoot(PlayerBrain brain, Barrel barrel) {
    Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
    projectile.transform.position = barrel.Origin.position;
    projectile.transform.right = barrel.Origin.right;
    projectile.GetComponent<SpriteRenderer>().color = brain.Stats.AmmoColor;
    projectile.GetComponent<Projectile>().Shoot(brain, Charges, GetScale());

    barrel.Rigidbody.AddForce(-10f * GetScale() * brain.Stats.AmmoSize * barrel.Origin.right, ForceMode2D.Impulse);
    barrel.ImpulseSource.GenerateImpulse(0.15f * GetScale() * brain.Stats.AmmoSize * barrel.Origin.right);
    if (ShootSFX != null) {
      barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      barrel.AudioSource.PlayOneShot(ShootSFX);
    }

    Charges = 0;
    brain.Stats.FakeBullet.AfterShoot();
  }
  public override void ShootingStarted(PlayerBrain brain, Barrel barrel) => Charges = 0;
  public override void ShootingUpdated(PlayerBrain brain, Barrel barrel) {
    Charges++;
    brain.Stats.FakeBullet.Size = GetScale() * brain.Stats.AmmoSize;
    if (Charges == MaxCharges) Shoot(brain, barrel);
  }
  public override void ShootingStopped(PlayerBrain brain, Barrel barrel) {
    if (Charges > 0) Shoot(brain, barrel);
  }

  private float GetScale() => 0.6f + Charges * 0.4f;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Worm Nova Weapon", menuName = "Weapons/WormNova")]
public class WormNovaWeapon : Weapon {
  public int MaxCharges = 10;
  public int Amount = 16;
  public float OrbChance = 10f;

  public void Shoot(Entity entity, AmmoStats stats, Barrel barrel) {
    GameObject defaultAmmo = PoolManager.Instance.Bullets.Objects.Get();
    // Setup bullet
    defaultAmmo.layer = entity.gameObject.layer;
    defaultAmmo.transform.position = barrel.Origin.position;
    defaultAmmo.transform.right = barrel.Origin.right;
    defaultAmmo.GetComponent<SpriteRenderer>().color = stats.AmmoColor;
    defaultAmmo.GetComponent<DefaultAmmo>().Initialize(entity, stats);
    defaultAmmo.transform.localScale = stats.AmmoSize * GetScale(barrel.Charges) * Vector3.one;
    defaultAmmo.GetComponent<Rigidbody2D>().velocity = 1.5f * stats.AmmoSpeed * defaultAmmo.transform.right;

    barrel.Rigidbody.AddForce(-10f * GetScale(barrel.Charges) * stats.AmmoSize * barrel.Origin.right, ForceMode2D.Impulse);
    barrel.ImpulseSource.GenerateImpulse(0.15f * GetScale(barrel.Charges) * stats.AmmoSize * barrel.Origin.right);
    if (ShootSFX != null) {
      barrel.AudioSource.pitch = Random.Range(0.7f, 1.3f);
      barrel.AudioSource.PlayOneShot(ShootSFX);
    }

    barrel.Charges = 0;
    barrel.FakeBullet.AfterShoot();
    barrel.StartCoroutine(NovaRoutine(defaultAmmo, entity, stats));
  }
  public override void ShootingStarted(Entity entity, AmmoStats stats, Barrel barrel) {
    barrel.Charges = 0;
    barrel.FakeBullet.BeforeShoot(stats.AmmoColor);
  }

  public override void ShootingUpdated(Entity entity, AmmoStats stats, Barrel barrel) {
    barrel.Charges++;
    barrel.FakeBullet.Size = GetScale(barrel.Charges) * stats.AmmoSize;
    if (barrel.Charges == MaxCharges) Shoot(entity, stats, barrel);
  }
  public override void ShootingStopped(Entity entity, AmmoStats stats, Barrel barrel) {
    if (barrel.Charges > 0) Shoot(entity, stats, barrel);
  }

  private float GetScale(int charges) => 0.6f + charges * 0.4f;

  public IEnumerator NovaRoutine(GameObject ammo, Entity entity, AmmoStats stats) {
    yield return new WaitForSeconds(1f);
    ammo.transform.localScale *= 0.5f;
    foreach (Vector3 direction in Vector3.zero.Nova(Amount)) SubBullets(ammo, entity, stats, direction);
    
    yield return new WaitForSeconds(1f);
    Destroy(ammo);
    foreach (Vector3 direction in Vector3.zero.Nova(Amount)) SubBullets(ammo, entity, stats, direction);
  }

  public void SubBullets(GameObject ammo, Entity entity, AmmoStats stats, Vector3 direction) {
    if (Chance.Percent(OrbChance)) {
      GameObject orb = PoolManager.Instance.EnergyOrbs.Objects.Get();
      orb.transform.position = ammo.transform.position + 0.5f * direction;
      orb.transform.right = direction;
      orb.transform.localScale = stats.AmmoSize * Vector3.one;
      orb.GetComponent<Rigidbody2D>().velocity = stats.AmmoSpeed * orb.transform.right;
    } else {
      GameObject bullet = PoolManager.Instance.Bullets.Objects.Get();
      bullet.layer = ammo.layer;
      bullet.transform.position = ammo.transform.position + 0.5f * direction;
      bullet.transform.right = direction;
      bullet.GetComponent<SpriteRenderer>().color = stats.AmmoColor;
      bullet.GetComponent<DefaultAmmo>().Initialize(entity, stats);
      bullet.transform.localScale = stats.AmmoSize * Vector3.one;
      bullet.GetComponent<Rigidbody2D>().velocity = stats.AmmoSpeed * bullet.transform.right;
    }
  }
}
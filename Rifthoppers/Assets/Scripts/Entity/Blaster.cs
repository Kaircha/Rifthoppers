using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {
  public Entity Entity;
  public SpriteRenderer Sprite;
  public AudioSource ShootSFX;
  public Transform BulletOrigin;
  public Rigidbody2D Rigidbody;
  private CinemachineImpulseSource ImpulseSource;
  private Stats Stats => Entity.Stats;

  public event Action OnShoot;

  public void Shoot() {
    // Safety fallback
    if (OnShoot == null) OnShoot += DefaultWeapon;
    OnShoot?.Invoke();
  }

  private void Awake() {
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
  }

  public void DefaultWeapon() {
    if (Stats.ProjectileCount > 1) {
      float angle = 60f;
      float angleStart = -angle / 2;
      float angleIncrease = angle / (Stats.ProjectileCount - 1f);

      for (int i = 0; i < Stats.ProjectileCount; i++) {
        Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
        projectile.transform.position = BulletOrigin.position;
        projectile.transform.right = BulletOrigin.right;
        projectile.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
        projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, Stats.ProjectileSpeed, Stats.ProjectileDamage, Stats.ProjectileHoming, Stats.ProjectileForks, Stats.ProjectileChains, Stats.ProjectileSizeMulti);
      }
    } else {
      Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
      projectile.transform.position = BulletOrigin.position;
      projectile.transform.right = BulletOrigin.right;
      projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, Stats.ProjectileSpeed, Stats.ProjectileDamage, Stats.ProjectileHoming, Stats.ProjectileForks, Stats.ProjectileChains, Stats.ProjectileSizeMulti);
    }

    Rigidbody.AddForce(-10f * Stats.ProjectileSizeMulti * BulletOrigin.right, ForceMode2D.Impulse);
    ImpulseSource.GenerateImpulse(0.15f * Stats.ProjectileSizeMulti * BulletOrigin.right);
    ShootSFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
    ShootSFX.Play();
  }
}

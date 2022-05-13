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
  public event Action OnStartShoot;
  public event Action OnStopShoot;
  public bool IsShooting = false;

  private FakeBullet fakeBullet;

  public void SwitchWeapon() {
    switch (Entity.Stats.Weapon) {
      case 0: OnStartShoot = DefaultWeaponStart; OnStopShoot = DefaultWeaponStop; break;
    }
  }

  public void StartShooting() {
    IsShooting = true;
    OnStartShoot?.Invoke();
  }
  public void StopShooting() {
    IsShooting = false;
    OnStopShoot?.Invoke();
  }

  private void Awake() {
    fakeBullet = GetComponentInChildren<FakeBullet>(true);
    OnStartShoot = DefaultWeaponStart;
    OnStopShoot = DefaultWeaponStop;
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
  }

  public void DefaultWeaponStart() {
    ++ChargeCount;
    fakeBullet.Size = CalcSize();
    fakeBullet.Damage = CalcDamage() * 0.75f;
    fakeBullet.color = Stats.ProjectileColor;
    if (ChargeCount == Stats.MaxCharges || !IsShooting) ShootBullet(); 
  }
  public void DefaultWeaponStop(){
    if (ChargeCount != 0) ShootBullet();
  }

  public void ShootBullet() {
    if (Stats.ProjectileCount > 1) {
      float angle = 60f;
      float angleStart = -angle / 2;
      float angleIncrease = angle / (Stats.ProjectileCount - 1f);

      for (int i = 0; i < Stats.ProjectileCount; i++) {
        Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
        projectile.GetComponent<SpriteRenderer>().color = Stats.ProjectileColor;
        projectile.transform.position = BulletOrigin.position;
        projectile.transform.right = BulletOrigin.right;
        projectile.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
        projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, Stats.ProjectileSpeed, CalcDamage(), Stats.ProjectileHoming, Stats.ProjectileForks, Stats.ProjectileChains, CalcSize());
      }
    } else {
      Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
      projectile.GetComponent<SpriteRenderer>().color = Stats.ProjectileColor;
      projectile.transform.position = BulletOrigin.position;
      projectile.transform.right = BulletOrigin.right;
      projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, Stats.ProjectileSpeed, CalcDamage(), Stats.ProjectileHoming, Stats.ProjectileForks, Stats.ProjectileChains, CalcSize());
    }

    fakeBullet.Size = fakeBullet.Damage = ChargeCount = 0;

    Rigidbody.AddForce(-10f * Stats.ProjectileSizeMulti * BulletOrigin.right, ForceMode2D.Impulse);
    ImpulseSource.GenerateImpulse(0.15f * Stats.ProjectileSizeMulti * BulletOrigin.right);
    ShootSFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
    ShootSFX.Play();
    OnShoot?.Invoke();
  }

  private int ChargeCount = 0;

  private float CalcSize() => Stats.ProjectileSizeMulti + 0.3f * ChargeCount;
  private float CalcDamage() => Stats.ProjectileDamage * (ChargeCount + 1);
}
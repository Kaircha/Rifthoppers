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
  public bool shooting = false;

  public void SwitchWeapon()
  {
    switch (Entity.Stats.Weapon){
      case 0: OnShoot = DefaultWeapon; break;
      case 1: OnShoot = SuperCharger; break;
    }
  }

  public void Shoot() {
    OnShoot?.Invoke();
  }

  private void Awake() {
    OnShoot = DefaultWeapon;
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

  private int ChargeCount = 0;

  public void SuperCharger(){

    ++ChargeCount;
    if (ChargeCount == 1) StartCoroutine(Charging());
  }

  private IEnumerator Charging(){

    yield return new WaitUntil(() => !shooting || ChargeCount == Stats.MaxCharges);
    ShootCharge();
    ChargeCount = 0;
  }

  public void ShootCharge(){

    if (Stats.ProjectileCount > 1)
    {
      float angle = 60f;
      float angleStart = -angle / 2;
      float angleIncrease = angle / (Stats.ProjectileCount - 1f);

      for (int i = 0; i < Stats.ProjectileCount; i++)
      {
        Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
        projectile.transform.position = BulletOrigin.position;
        projectile.transform.right = BulletOrigin.right;
        projectile.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
        projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, Stats.ProjectileSpeed, Stats.ProjectileDamage * ChargeCount, Stats.ProjectileHoming, Stats.ProjectileForks, Stats.ProjectileChains, Stats.ProjectileSizeMulti * 0.8f * ChargeCount);
      }
    }
    else
    {
      Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
      projectile.transform.position = BulletOrigin.position;
      projectile.transform.right = BulletOrigin.right;
      projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, Stats.ProjectileSpeed, Stats.ProjectileDamage * ChargeCount, Stats.ProjectileHoming, Stats.ProjectileForks, Stats.ProjectileChains, Stats.ProjectileSizeMulti * 0.8f * ChargeCount);
    }

    Rigidbody.AddForce(-10f * Stats.ProjectileSizeMulti * BulletOrigin.right, ForceMode2D.Impulse);
    ImpulseSource.GenerateImpulse(0.15f * Stats.ProjectileSizeMulti * BulletOrigin.right);
    ShootSFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
    ShootSFX.Play();
  }
}

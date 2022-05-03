using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
  public Entity Entity;
  public SpriteRenderer Sprite;
  public AudioSource ShootSFX;
  public Transform BulletOrigin;
  public Rigidbody2D Rigidbody;
  private CinemachineImpulseSource ImpulseSource;
  private StatManager Stats => StatManager.Instance;

  private void Awake() {
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
  }

  public void Shoot() {
    int projectileCount = Mathf.Max((int)Stats.Get(StatType.ProjectileCount), 1);
    float projectileSpeed = Mathf.Max(10 * Stats.Get(StatType.ProjectileSpeedMulti), 1);
    float projectileDamage = Mathf.Max(Stats.Get(StatType.ProjectileDamage) * Stats.Get(StatType.ProjectileDamageMulti), 1);
    float projectileHoming = Stats.Get(StatType.ProjectileHoming);
    int projectileForks = (int)Stats.Get(StatType.ProjectileForks);
    int projectilePierces = (int)Stats.Get(StatType.ProjectilePierces);
    int projectileChains = (int)Stats.Get(StatType.ProjectileChains);
    float projectileExplosion = Stats.Get(StatType.ProjectileExplosion);
    float projectileSizeMulti = Mathf.Max(Stats.Get(StatType.ProjectileSizeMulti), 0.1f);

    if (projectileCount > 1) {
      float angle = 60f;
      float angleStart = -angle / 2;
      float angleIncrease = angle / ((float)projectileCount - 1f);

      for (int i = 0; i < projectileCount; i++) {
        Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
        projectile.transform.position = BulletOrigin.position;
        projectile.transform.right = BulletOrigin.right;
        projectile.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
        projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, projectileSpeed, projectileDamage, projectileHoming, projectileForks, projectilePierces, projectileChains, projectileExplosion, projectileSizeMulti);
      }
    } else {
      Transform projectile = PoolManager.Instance.Bullets.Objects.Get().transform;
      projectile.transform.position = BulletOrigin.position;
      projectile.transform.right = BulletOrigin.right;
      projectile.GetComponent<Projectile>().Shoot(gameObject.layer, Entity, projectileSpeed, projectileDamage, projectileHoming, projectileForks, projectilePierces, projectileChains, projectileExplosion, projectileSizeMulti);
    }

    Rigidbody.AddForce(-10f * projectileSizeMulti * BulletOrigin.right, ForceMode2D.Impulse);
    ImpulseSource.GenerateImpulse(0.15f * projectileSizeMulti * BulletOrigin.right);
    ShootSFX.pitch = Random.Range(0.7f, 1.3f);
    ShootSFX.Play();
  }
}

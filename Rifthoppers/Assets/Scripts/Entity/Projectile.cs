using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour, IPoolable {
  private Rigidbody2D Rigidbody;
  private Collider2D Collider;
  private AudioSource HitAudio;
  private List<GameObject> Targets;
  private Entity Owner;
  private LayerMask Ignore;
  private float Speed;
  private float Damage;
  private float Homing;
  private int Forks;
  private int Pierces;
  private int Chains;
  private float Explosion;
  private float SizeMulti;

  public Pool Pool { get; set; }
  private bool IsArmed = false;

  private void OnEnable() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Collider = GetComponent<Collider2D>();
    HitAudio = GetComponent<AudioSource>();
    Speed = 0;
    Damage = 0;
    RiftManager.Instance.OnWaveEnded += Disarm;
  }
  private void OnDisable() => RiftManager.Instance.OnWaveEnded -= Disarm;


  // Temporarily disabled until the RiftManager is finished
  public void FixedUpdate() {
    // Homing; Flies towards target
    Transform target = Targets.Count > 0 ? Targets[0].transform : null;
    if (target == null || Homing <= 0) return;
    Vector3 toTarget = (target.position - transform.position + 0.5f * Vector3.up).normalized;
    transform.right = Vector3.Lerp(transform.right, toTarget, Homing * Time.deltaTime);
    Rigidbody.velocity = Speed * transform.right;
  }

  public void Shoot(Entity owner) => Shoot(owner, owner.gameObject.layer, owner.Stats.ProjectileSpeed * owner.Stats.ProjectileSpeedMulti, owner.Stats.ProjectileDamage * owner.Stats.ProjectileDamageMulti, owner.Stats.ProjectileHoming, owner.Stats.ProjectileForks, owner.Stats.ProjectileChains, owner.Stats.ProjectileSizeMulti);
  public void Shoot(Entity owner, float dmgMulti, float sizeMulti) => Shoot(owner, owner.gameObject.layer, owner.Stats.ProjectileSpeed * owner.Stats.ProjectileSpeedMulti, owner.Stats.ProjectileDamage * owner.Stats.ProjectileDamageMulti * dmgMulti, owner.Stats.ProjectileHoming, owner.Stats.ProjectileForks, owner.Stats.ProjectileChains, owner.Stats.ProjectileSizeMulti * sizeMulti);
  public void Shoot(Entity owner, LayerMask ignore, float speed, float damage, float homing = 0, int forks = 0, int chains = 0, float sizeMulti = 1) {
    Owner = owner;
    Ignore = ignore;
    Speed = speed;
    Damage = damage;
    Homing = homing;
    Forks = forks;
    Chains = chains;
    SizeMulti = sizeMulti;
    Rigidbody.velocity = Speed * transform.right;
    transform.localScale = SizeMulti * Vector3.one;
    IsArmed = true;
    Targets = Physics2D.OverlapCircleAll(transform.position, 20f, ~gameObject.layer).OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).Select(x => x.gameObject).ToList();
  }

  public void OnTriggerEnter2D(Collider2D collider) {
    if (!IsArmed) return;
    if (collider.gameObject.layer == Ignore) {
      Physics2D.IgnoreCollision(Collider, collider);
      return;
    }

    // Dealing damage
    if (collider.gameObject.TryGetComponent(out Entity entity)) {
      collider.GetComponent<Rigidbody2D>().AddForce(40f * SizeMulti * transform.right, ForceMode2D.Impulse);
      GameObject impact = PoolManager.Instance.BulletImpact.Objects.Get();
      impact.transform.position = transform.position;
      impact.transform.right = transform.right;
      impact.GetComponent<ParticleSystem>().Play();
      impact.GetComponent<AudioSource>().Play();

      entity.Health.Hurt(Owner, entity, Damage, false);
      //ExplodeAndDamage();
    }

    // Piercing; Passes through target
    if (Pierces >= 1) {
      Physics2D.IgnoreCollision(Collider, collider);
      Pierces--;
      return;
    }

    // Forking; Splits into 2 or more
    if (Forks > 1) {
      float angle = 90f; // Base this value on the distance between player and mouse
      float angleStart = -angle / 2;
      float angleIncrease = angle / ((float)Forks - 1f);

      for (int i = 0; i < Forks; i++) {
        Projectile projectile = PoolManager.Instance.Bullets.Objects.Get().GetComponent<Projectile>();
        projectile.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        projectile.transform.position = transform.position;
        projectile.transform.right = transform.right;
        projectile.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
        projectile.Shoot(Owner, Ignore, Speed, Damage / 2, Homing, 0, Chains, SizeMulti * 0.7f);
        Physics2D.IgnoreCollision(projectile.Collider, collider);
      }
      Forks = 0;

      Disarm();
      return;
    }

    // Chaining; Bounces to next closest target
    if (Chains >= 1) {
      Physics2D.IgnoreCollision(Collider, collider);
      Chains--;
      Transform target = Targets.Count > 1 ? Targets[1].transform : null;
      if (target != null) {
        transform.right = (target.position - transform.position + 0.5f * Vector3.up).normalized;
        Rigidbody.velocity = Speed * transform.right;
        return;
      }
    }

    Disarm();
  }

  public void ExplodeAndDamage() {
    foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, transform.lossyScale.x)){
      if (collider.attachedRigidbody != null && collider.attachedRigidbody.TryGetComponent<Entity>(out Entity entity)){
        if (entity is EnemyEntity) entity.Health.Hurt(Owner, entity, Damage, false, Explosion, transform.position);
      }
    }
  }

  public void Disarm() {
    IsArmed = false;
    (this as IPoolable).Release(gameObject);
  }
}
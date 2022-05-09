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
  private LayerMask Ignore;
  private Entity Owner;
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

  public void OnEnable() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Collider = GetComponent<Collider2D>();
    HitAudio = GetComponent<AudioSource>();
    Speed = 0;
    Damage = 0;
  }

  // Temporarily disabled until the RiftManager is finished
  public void FixedUpdate() {
    // Homing; Flies towards target
    Transform target = Targets.Count > 0 ? Targets[0].transform : null;
    if (target == null || Homing <= 0) return;
    Vector3 toTarget = (target.position - transform.position + 0.5f * Vector3.up).normalized;
    float dotAngle = Vector3.Dot(transform.right, toTarget);
    if (dotAngle > 0) {
      transform.right = Vector3.Lerp(transform.right, toTarget, Homing * Time.deltaTime);
      Rigidbody.velocity = Speed * transform.right;
    }
  }

  public void Shoot(LayerMask ignore, Entity owner, float speed, float damage, float homing = 0, int forks = 0, int chains = 0, float sizeMulti = 1) {
    Ignore = ignore;
    Owner = owner;
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

      //entity.Health.Hurt(Owner, entity, Damage, false);
      //moved dealing damage to ExplodeAndDamage()
      //Explode();
      ExplodeAndDamge();
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
        projectile.Shoot(Ignore, Owner, Speed, Damage / 2, Homing, 0, Chains, SizeMulti * 0.7f);
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

  public void ExplodeAndDamge(){

    foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, transform.lossyScale.x)){
      if (collider.attachedRigidbody != null && collider.attachedRigidbody.TryGetComponent<Entity>(out Entity entity)){
        if(entity is EnemyEntity)
        entity.Health.Hurt(Owner, entity, Damage, false, Explosion, transform.position);
      }
    }
  }

  // Not great
  public void Explode() {

      if (Explosion <= 0) return;
      foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, Explosion)) {
        if (collider.TryGetComponent(out Rigidbody2D rigidbody)) {
          Vector3 direction = (transform.position - collider.transform.position).normalized;
          rigidbody.AddForce(10 * Explosion * direction);
        }
      }
    }

  public void Disarm() {
    IsArmed = false;
    (this as IPoolable).Release(gameObject);
  }
}
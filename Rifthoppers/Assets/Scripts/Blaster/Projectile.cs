using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour, IPoolable {
  [HideInInspector] public Rigidbody2D Rigidbody;
  private Collider2D Collider;
  private AudioSource HitAudio;
  private List<GameObject> Targets;
  [HideInInspector] public PlayerBrain Owner;
  private LayerMask Ignore;
  [HideInInspector] public float Speed;
  private float Damage;
  private float Homing;
  private int Forks;
  private int Bounces;
  private bool CanPierce => Owner.PlayerStats.AmmoStats.AmmoPierces > 0;
  private float SizeMulti;

  public Pool Pool { get; set; }
  private bool IsArmed = false;

  private void OnEnable() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Collider = GetComponent<Collider2D>();
    HitAudio = GetComponent<AudioSource>();
    Speed = 0;
    Damage = 0;
    RiftManager.Instance.OnEncounterEnded += Disarm;
  }
  private void OnDisable() => RiftManager.Instance.OnEncounterEnded -= Disarm;

  // Temporarily disabled until the RiftManager is finished
  public void FixedUpdate() {
    // Homing; Flies towards target
    Transform target = Targets.Count > 0 ? Targets[0].transform : null;
    if (target == null || Homing <= 0) return;
    Vector3 toTarget = (target.position - transform.position + 0.5f * Vector3.up).normalized;
    transform.right = Vector3.Lerp(transform.right, toTarget, Homing * Time.deltaTime);
    Rigidbody.velocity = Speed * transform.right;
  }

  public void Shoot(PlayerBrain owner) => Shoot(owner, owner.gameObject.layer, owner.PlayerStats.AmmoStats.AmmoSpeed * owner.PlayerStats.AmmoStats.AmmoSpeedMulti, owner.PlayerStats.AmmoStats.AmmoDamage * owner.PlayerStats.AmmoStats.AmmoDamageMulti, owner.PlayerStats.AmmoStats.AmmoHoming, owner.PlayerStats.AmmoStats.AmmoSplits, owner.PlayerStats.AmmoStats.AmmoSize);
  public void Shoot(PlayerBrain owner, float dmgMulti, float sizeMulti) => Shoot(owner, owner.gameObject.layer, owner.PlayerStats.AmmoStats.AmmoSpeed * owner.PlayerStats.AmmoStats.AmmoSpeedMulti, owner.PlayerStats.AmmoStats.AmmoDamage * owner.PlayerStats.AmmoStats.AmmoDamageMulti * dmgMulti, owner.PlayerStats.AmmoStats.AmmoHoming, owner.PlayerStats.AmmoStats.AmmoSplits, owner.PlayerStats.AmmoStats.AmmoSize * sizeMulti);
  public void Shoot(PlayerBrain owner, LayerMask ignore, float speed, float damage, float homing = 0, int forks = 0, float sizeMulti = 1) {
    Owner = owner;
    Ignore = ignore;
    Speed = speed;
    Damage = damage;
    Homing = homing;
    Forks = forks;
    Bounces = owner.PlayerStats.AmmoStats.AmmoReflects;
    SizeMulti = sizeMulti;
    Rigidbody.velocity = Speed * transform.right;
    transform.localScale = SizeMulti * Vector3.one;
    IsArmed = true;
    Targets = Physics2D.OverlapCircleAll(transform.position, 20f, ~gameObject.layer).OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).Select(x => x.gameObject).ToList();
  }

  public void OnCollisionEnter2D(Collision2D collision) {
    if (!IsArmed) return;
    if (collision.gameObject.layer == Ignore) {
      Physics2D.IgnoreCollision(Collider, collision.collider);
      return;
    }

    if (Owner.PlayerStats.AmmoStats.AmmoReflects > 0 && collision.gameObject.layer == 0) {
      transform.right = Vector2.Reflect(transform.right, collision.GetContact(0).normal);
      Rigidbody.velocity = Speed * transform.right;
    } else Disarm();
  }

  public void OnTriggerEnter2D(Collider2D collider) {
    if (!IsArmed) return;
    if (collider.gameObject.layer == Ignore) {
      Physics2D.IgnoreCollision(Collider, collider);
      return;
    }

    float excessDamage = Impact(collider);
    if (Forks > 1) Fork(collider);
    if (excessDamage > 0 && CanPierce) Pierce(collider, excessDamage);
    else Disarm();
  }

  public float Impact(Collider2D collider) {
    if (collider.gameObject.TryGetComponent(out Entity entity)) {
      collider.GetComponent<Rigidbody2D>().AddForce(40f * SizeMulti * transform.right, ForceMode2D.Impulse);
      GameObject impact = PoolManager.Instance.BulletImpacts.Objects.Get();
      impact.transform.position = transform.position;
      impact.transform.right = transform.right;
      impact.GetComponent<ParticleSystem>().Play();
      impact.GetComponent<AudioSource>().Play();

      if (Chance.Percent(Owner.PlayerStats.PoisonChance)) entity.AddEffect(new PoisonEffect(Damage, 4f));
      if (Chance.Percent(Owner.PlayerStats.IgniteChance)) entity.AddEffect(new IgniteEffect(Damage, 4f));

      return entity.Health.Hurt(Owner.Entity, entity, Damage, false);
    }
    return Damage;
  }

  public void Fork(Collider2D collider) {
    float angle = 90f; // Base this value on the distance between player and mouse
    float angleStart = -angle / 2;
    float angleIncrease = angle / ((float)Forks - 1f);

    for (int i = 0; i < Forks; i++) {
      Projectile projectile = PoolManager.Instance.Bullets.Objects.Get().GetComponent<Projectile>();
      projectile.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
      projectile.transform.position = transform.position;
      projectile.transform.right = transform.right;
      projectile.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
      projectile.Shoot(Owner, Ignore, Speed, Damage / 2, Homing, 0, Mathf.Max(SizeMulti / 2, 0.6f));
      Physics2D.IgnoreCollision(projectile.Collider, collider);
    }
  }

  public void Pierce(Collider2D collider, float excessDamage) {
    Physics2D.IgnoreCollision(Collider, collider);
    SizeMulti = Mathf.Max(SizeMulti / Damage * excessDamage, 0.6f);
    transform.localScale = SizeMulti * Vector3.one;
    Damage = excessDamage;
  }

  public void Disarm() {
    IsArmed = false;
    (this as IPoolable).Release(gameObject);
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAmmo : Ammo {
  public void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody == null) return;
    if (collider.TryGetComponent(out Damager _)) return;
    if (collider.TryGetComponent(out Orbital _)) return;
    if (collider.attachedRigidbody.TryGetComponent(out Entity target)) {
      collider.attachedRigidbody.AddForce(40f * Stats.AmmoSize * transform.right, ForceMode2D.Impulse);
      GameObject impact = PoolManager.Instance.BulletImpacts.Objects.Get();
      impact.transform.position = transform.position;
      impact.transform.right = transform.right;
      impact.GetComponent<ParticleSystem>().Play();
      impact.GetComponent<AudioSource>().Play();

      //if (Chance.Percent(Brain.PlayerStats.PoisonChance)) target.AddEffect(new PoisonEffect(Brain.AmmoStats.AmmoDamage, 4f));
      //if (Chance.Percent(Brain.PlayerStats.IgniteChance)) target.AddEffect(new IgniteEffect(Brain.AmmoStats.AmmoDamage, 4f));

      if (Stats.AmmoSplits > 0) Split(collider);

      target.Health.Hurt(Entity, target, Stats.AmmoDamage, false);
      (this as IPoolable).Release(gameObject);
    }
  }

  public override void Homing() { }

  public override void Pierce(Collider2D collider) {
    throw new System.NotImplementedException();
  }

  public override void Reflect(Collision2D collision) {
    throw new System.NotImplementedException();
  }

  public override void Split(Collider2D collider) {
    float angle = 90f; // Base this value on the distance between player and mouse
    float angleStart = -angle / 2;
    float angleIncrease = angle / Stats.AmmoSplits;

    AmmoStats newAmmo = new AmmoStats(Stats);
    newAmmo.AmmoSplits = 0;

    for (int i = 0; i < Stats.AmmoSplits; i++) {
      GameObject defaultAmmo = PoolManager.Instance.Bullets.Objects.Get();

      // Needs to be changed!!!
      Physics2D.IgnoreCollision(collider, defaultAmmo.GetComponent<Collider2D>());


      // Setup bullet

      defaultAmmo.layer = Entity.gameObject.layer;
      defaultAmmo.transform.position = transform.position;
      defaultAmmo.transform.right = transform.right;
      defaultAmmo.transform.Rotate(Vector3.forward, angleStart + angleIncrease * i);
      defaultAmmo.GetComponent<SpriteRenderer>().color = Stats.AmmoColor;
      defaultAmmo.GetComponent<DefaultAmmo>().Initialize(Entity, newAmmo);
      defaultAmmo.transform.localScale = Stats.AmmoSize * Vector3.one;
      defaultAmmo.GetComponent<Rigidbody2D>().velocity = Stats.AmmoSpeed * defaultAmmo.transform.right;
    }
  }
}
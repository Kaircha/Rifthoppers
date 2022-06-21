using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAmmo : Ammo {
  public void OnCollisionEnter2D(Collision2D collision) {
    if (Brain.Stats.AmmoReflects > 0) {
      transform.right = Vector2.Reflect(transform.right, collision.GetContact(0).normal);
      Rigidbody.velocity = Brain.Stats.AmmoSpeed * transform.right;
    } else {
      (this as IPoolable).Release(gameObject);
    }
  }

  public void OnTriggerEnter2D(Collider2D collider) {
    if (collider.attachedRigidbody == null) return;
    if (collider.TryGetComponent(out Damager _)) return;
    if (collider.attachedRigidbody.TryGetComponent(out Entity entity)) {
      collider.GetComponent<Rigidbody2D>().AddForce(40f * Brain.Stats.AmmoSize * transform.right, ForceMode2D.Impulse);
      GameObject impact = PoolManager.Instance.BulletImpacts.Objects.Get();
      impact.transform.position = transform.position;
      impact.transform.right = transform.right;
      impact.GetComponent<ParticleSystem>().Play();
      impact.GetComponent<AudioSource>().Play();

      if (Chance.Percent(Brain.Stats.PoisonChance)) entity.AddEffect(new PoisonEffect(Brain.Stats.AmmoDamage, 4f));
      if (Chance.Percent(Brain.Stats.IgniteChance)) entity.AddEffect(new IgniteEffect(Brain.Stats.AmmoDamage, 4f));

      entity.Health.Hurt(Brain.Entity, entity, Brain.Stats.AmmoDamage, false);
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
    throw new System.NotImplementedException();
  }
}
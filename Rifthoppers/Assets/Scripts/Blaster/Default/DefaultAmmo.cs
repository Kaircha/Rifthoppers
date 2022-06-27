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
    throw new System.NotImplementedException();
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteEffect : Effect {
  public float Damage;
  public float Duration;

  public IgniteEffect(float damage, float duration) {
    Damage = Mathf.Max(damage, 0);
    Duration = Mathf.Max(duration, 0);
  }

  public override void Add(Effect effect) {
    if (effect is not IgniteEffect) return;
    IgniteEffect ignite = effect as IgniteEffect;

    if (Damage <= 0 || Duration <= 0) {
      Damage = ignite.Damage;
      Duration = ignite.Duration;
      return;
    }

    if (Damage < ignite.Damage) {
      (Damage, ignite.Damage) = (ignite.Damage, Damage);
      (Duration, ignite.Duration) = (ignite.Duration, Duration);
    }

    Duration += ignite.Duration * ignite.Damage / Damage;
  }

  public override IEnumerator EffectRoutine() {
    // Add VFX

    while (Duration > 0) {
      Entity.Health.Hurt(null, Entity, Damage * Time.deltaTime, true);
      Duration -= Time.deltaTime;
      yield return null;
    }

    Entity.RemoveEffect(this);
    // Remove VFX
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {

}

public class IgniteEffect : Effect {
  public float Damage;
  public float Duration;

  public IgniteEffect(float damage, float duration) {
    Damage = Mathf.Max(damage, 0);
    Duration = Mathf.Max(duration, 0);
  }

  public void Add(IgniteEffect ignite) {
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
}

public class PoisonEffect : Effect {
  public float Damage;
  public float Duration;

  public PoisonEffect(float damage, float duration) {
    Damage = Mathf.Max(damage, 0);
    Duration = Mathf.Max(duration, 0);
  }
}
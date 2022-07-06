using System;
using System.Collections;
using UnityEngine;

public class ParentHealthConduit : MonoBehaviour, IHealth {
  private Health _health;
  public Health Health {
    get {
      if (_health == null) _health = GetComponentInParent<Health>();
      return _health;
    }
  }
  public bool IsDead => Health.IsDead;
  public float Percentage => Health.Percentage;

  public event Action<Entity, Entity, float, bool> OnDamageTaken;
  public event Action<Entity> OnDeath;

  public void Revive() => Health.Revive();
  public void Heal() => Health.Heal();
  public void Heal(float amount) => Health.Heal(amount);
  public float Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    OnDamageTaken?.Invoke(dealer, receiver, 0, isDoT);
    return Health.Hurt(dealer, receiver, amount, isDoT);
  }

  public void Kill() {
    Health.Kill();
    OnDeath?.Invoke(null);
  }
}

public enum HealthTarget {
  Energy,
  Parent,
}
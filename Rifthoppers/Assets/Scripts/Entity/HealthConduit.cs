using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class HealthConduit : MonoBehaviour, IHealth {
  public HealthTarget Target;
  private IHealth _health;
  public IHealth Health {
    get {
      if (_health == null) {
        switch (Target) {
          case HealthTarget.Energy: _health = RiftManager.Instance.Energy; break;
          case HealthTarget.Parent: _health = GetComponentInParent<IHealth>(); break;
        }
      }
      return _health;
    }
  }
  public bool IsDead => Health.IsDead;
  public float Percentage => Health.Percentage;

  public event Action<Entity, Entity, float, bool> OnDamageTaken;
  public event Action<Entity> OnDeath;

  private CinemachineImpulseSource ImpulseSource; // Health shouldn't contain this
  private Immunity Immunity; // Health shouldn't contain this

  private void Awake() {
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
    Immunity = GetComponentInChildren<Immunity>();
  }

  public void Revive() => Health.Revive();

  public void Heal() => Health.Heal();

  public void Heal(float amount) => Health.Heal(amount);

  public float Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    // Health shouldn't generate impulses!
    if (!isDoT) {
      ImpulseSource.GenerateImpulse(0.5f);
      StartCoroutine(Immunity.ImmunityRoutine(0.2f));
    }

    OnDamageTaken?.Invoke(dealer, receiver, 0, isDoT);
    return Health.Hurt(dealer, receiver, amount, isDoT);
  }
  public float Hurt(Entity dealer, Entity receiver, float amount, bool isDoT, float knockback, Vector3 pos) => Hurt(dealer, receiver, amount, isDoT);

  public void Kill() {
    Health.Kill();
    OnDeath?.Invoke(null);
  }
}

public enum HealthTarget {
  Energy,
  Parent,
}
using System;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour, IHealth {
  public float Percentage => Current / Maximum;

  public bool IsDead { get; set; }

  public float Current;
  public float Maximum;
  public event Action OnHeal;
  public event Action<Entity, Entity, float, bool> OnDamageTaken;
  public event Action<Entity> OnDeath;

  public void Revive() {
    Current = Maximum;
    IsDead = false;
    OnHeal?.Invoke();
  }

  public void Heal() {
    if (IsDead) return;
    Current = Maximum;
    OnHeal?.Invoke();
  }

  public float Heal(float amount) {
    if (amount <= 0 || IsDead) return 0f;
    float excess = 0f;

    Current += amount;
    if (Current > Maximum) {
      excess = Maximum - Current;
      Current = Maximum;
    }
    OnHeal?.Invoke();
    return excess;
  }

  public float Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (amount <= 0 || IsDead) return 0f;
    float excess = 0f;
    Current -= amount;
    OnDamageTaken?.Invoke(dealer, receiver, amount, isDoT);
    if (dealer != null) dealer.HasDealtDamage(receiver, amount, isDoT);
    if (Current <= 0) {
      OnDamageTaken?.Invoke(dealer, receiver, amount, isDoT);
      excess = -Current;
      Current = 0;
      IsDead = true;
      OnDeath?.Invoke(dealer);
      if (dealer != null && dealer is PlayerEntity) RiftManager.Instance.HasKilled(dealer as PlayerEntity);
    }
    return excess;
  }
  public float Hurt(Entity dealer, Entity receiver, float amount, bool isDoT, float knockback, Vector3 pos) {
    Vector3 direction = (pos - transform.position).normalized;
    GetComponent<Rigidbody2D>().AddForce(10 * knockback * direction);

    return Hurt(dealer, receiver, amount, isDoT);
  }

  // needs work !!!
  public void Kill() {
    if (IsDead) return;
    Current = 0;
    IsDead = true;
    OnDeath?.Invoke(null);
  }
}
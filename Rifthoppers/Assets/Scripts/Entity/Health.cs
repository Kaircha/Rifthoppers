using System;
using UnityEngine;

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

  public void Heal(float amount) {
    if (amount <= 0 || IsDead) return;
    Current += amount;
    if (Current > Maximum) Current = Maximum;
    OnHeal?.Invoke();
  }

  public void Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (amount <= 0 || IsDead) return;
    Current -= amount;
    OnDamageTaken?.Invoke(dealer, receiver, amount, isDoT);
    if (dealer != null) dealer.HasDealtDamage(receiver, amount, isDoT);
    if (Current <= 0) {
      Current = 0;
      IsDead = true;
      OnDeath?.Invoke(dealer);
      if (dealer != null && dealer is PlayerEntity) RiftManager.Instance.HasKilled(dealer as PlayerEntity);
    }
  }

  public void Kill() {
    if (IsDead) return;
    Current = 0;
    IsDead = true;
    OnDeath?.Invoke(null);
  }
}
using System;
using UnityEngine;

public class Energy : MonoBehaviour, IHealth {
  public float StaticPercentage => Mathf.Clamp01(Static / Maximum);
  public float Percentage => Mathf.Clamp01(Dynamic / Maximum);

  public bool IsDead { get; set; }

  public float Static;
  public float Dynamic;
  public float Maximum;
  public event Action OnHeal;
  public event Action<Entity, Entity, float, bool> OnDamageTaken;
  public event Action<Entity> OnDeath;

  public void Update() {
    if (IsDead) return;
    if (Dynamic > Static) Dynamic -= 0.15f * Maximum * Time.deltaTime;
    if (Dynamic < Static) Dynamic = Static;
    if (Dynamic <= 0) {
      IsDead = true;
      OnDeath?.Invoke(null);
    }
  }

  public void Revive() {
    Static = Maximum;
    Dynamic = Maximum;
    IsDead = false;
    OnHeal?.Invoke();
  }

  public void Heal() {
    if (IsDead) return;
    Static = Maximum;
    Dynamic = Maximum;
    OnHeal?.Invoke();
  }

  public void Heal(float amount) {
    if (amount <= 0 || IsDead) return;
    Static += amount;
    Dynamic += amount;
    if (Static > Maximum) Static = Maximum;
    if (Dynamic > Maximum) Dynamic = Maximum;
    OnHeal?.Invoke();
  }

  public void Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (amount <= 0 || IsDead) return;
    Static -= amount;
    OnDamageTaken?.Invoke(dealer, null, amount, isDoT);
    if (dealer != null) dealer.HasDealtDamage(receiver, amount, isDoT);
    if (isDoT) Dynamic -= amount;
    if (Static <= 0) Static = 0;
    if (Dynamic <= 0) {
      Dynamic = 0;
      IsDead = true;
      OnDeath?.Invoke(dealer);
    }
  }

  public void Kill() {
    if (IsDead) return;
    Static = 0;
    Dynamic = 0;
    OnDeath?.Invoke(null);
  }
}
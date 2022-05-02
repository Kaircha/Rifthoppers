using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour, IHealth {
  public Energy Energy => RiftManager.Instance.Energy;

  public bool IsDead { get => Energy.IsDead; set { } }

  public float Percentage => Energy.Percentage;

  public event Action<Entity, Entity, float, bool> OnDamageTaken;
  public event Action<Entity> OnDeath;
  private bool IsHurtable = true;
  private CinemachineImpulseSource ImpulseSource;

  private void Start() {
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
  }

  public void Revive() => Energy.Revive();

  public void Heal() => Energy.Heal();

  public void Heal(float amount) => Energy.Heal(amount);

  public void Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    // Hurtable needs to be replaced with proper IFrames
    if (!IsHurtable) return;
    // The player doesn't take damage, but instead redirects this damage to the Rift's energy
    OnDamageTaken?.Invoke(dealer, receiver, 0, isDoT);
    Energy.Hurt(dealer, receiver, amount, isDoT);
    StartCoroutine(HurtRoutine());
  }

  // HurtRoutine needs to be on the Entity instead?
  public IEnumerator HurtRoutine() {
    IsHurtable = false;
    ImpulseSource.GenerateImpulse(0.5f);
    yield return new WaitForSeconds(0.2f);
    IsHurtable = true;
  }

  public void Kill() {
    Energy.Kill();
    OnDeath?.Invoke(null);
  }
}
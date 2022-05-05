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

  private CinemachineImpulseSource ImpulseSource;
  private Immunity Immunity;

  private void Awake() {
    ImpulseSource = GetComponent<CinemachineImpulseSource>();
    Immunity = GetComponentInChildren<Immunity>();
  }

  public void Revive() => Energy.Revive();

  public void Heal() => Energy.Heal();

  public void Heal(float amount) => Energy.Heal(amount);

  public void Hurt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    StartCoroutine(Immunity.ImmunityRoutine(0.2f));
    ImpulseSource.GenerateImpulse(0.5f);

    // The player doesn't take damage, but instead redirects this damage to the Rift's energy
    OnDamageTaken?.Invoke(dealer, receiver, 0, isDoT);
    Energy.Hurt(dealer, receiver, amount, isDoT);
  }

  public void Kill() {
    Energy.Kill();
    OnDeath?.Invoke(null);
  }
}
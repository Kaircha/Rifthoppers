using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IHealth))]
public class Entity : MonoBehaviour {
  [HideInInspector] public Rigidbody2D Rigidbody;
  [HideInInspector] public Animator Animator;
  [HideInInspector] public IHealth Health;
  [HideInInspector] public Vector2 Direction;
  public SpriteRenderer Sprite;
  public AudioSource HurtAudio;
  public List<Effect> Effects = new();

  // Dealer, Receiver, Amount, isDoT
  public event Action<Entity, Entity, float, bool> OnDamageDealt;
  public void DealtDamage(Entity receiver, float amount, bool isDoT) => OnDamageDealt?.Invoke(this, receiver, amount, isDoT);
  public event Action<Entity, Surface> OnSurfaceWalked;
  public void SurfaceWalked(Entity entity, Surface surface) => OnSurfaceWalked?.Invoke(this, surface);

  public void Awake() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Animator = GetComponent<Animator>();
    Health = GetComponent<IHealth>();
  }

  private void OnEnable() => Health.OnDamageTaken += OnDamageTaken;
  private void OnDisable() => Health.OnDamageTaken -= OnDamageTaken;

  private void FixedUpdate() {
    Rigidbody.AddForce(Direction, ForceMode2D.Impulse);
    Animator.SetBool("IsMoving", Direction.magnitude > 0);
  }

  private Coroutine HurtRoutine;
  public virtual void OnDamageTaken(Entity dealer, Entity target, float amount, bool isDoT) {
    if (isDoT) return;
    if (HurtRoutine != null) StopCoroutine(HurtRoutine);
    HurtRoutine = StartCoroutine(OnDamageTakenRoutine());
  }
  public IEnumerator OnDamageTakenRoutine() {
    if (HurtAudio != null && HurtAudio.clip != null) HurtAudio.Play();
    Sprite.material.SetInt("_IsHurt", 1);
    yield return new WaitForSeconds(0.2f);
    Sprite.material.SetInt("_IsHurt", 0);
  }

  #region Effects
  public void AddEffect(Effect effect) {
    foreach (Effect e in Effects) {
      if (e.GetType() == effect.GetType()) {
        e.Add(effect);
        return;
      }
    }

    effect.Entity = this;
    effect.Coroutine = StartCoroutine(effect.EffectRoutine());
    Effects.Add(effect);
  }

  public void RemoveEffect(Effect effect) {
    if (Effects.Contains(effect)) {
      Effects.Remove(effect);
      StopCoroutine(effect.Coroutine);
    }
  }

  public void RemoveEffects() {
    foreach (Effect effect in Effects) {
      StopCoroutine(effect.Coroutine);
    }
    Effects = new();
  }
  #endregion
}
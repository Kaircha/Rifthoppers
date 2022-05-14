using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IHealth))]
public class Entity : MonoBehaviour {
  [HideInInspector] public Rigidbody2D Rigidbody;
  [HideInInspector] public Animator Animator;
  [HideInInspector] public IHealth Health;
  [HideInInspector] public Stats Stats;
  [HideInInspector] public ApplyUpgradeVFX UpgradeVFX;
  [HideInInspector] public InputDevice Device;
  [HideInInspector] public InputData Input;
  [HideInInspector] public Vector2 Direction;
  [HideInInspector] public bool IsMoving;
  public SpriteRenderer Sprite;
  public AudioSource HurtAudio;
  public Transform Target;
  public float Speed;
  public float AISpeed;
  public bool IsFlying;
  public List<Upgrade> Upgrades = new();
  public List<Effect> Effects = new();
  public Blaster Blaster;

  // Dealer, Receiver, Amount, isDoT
  public event Action<Entity, Entity, float, bool> OnDamageDealt;
  public void HasDealtDamage(Entity receiver, float amount, bool isDoT) => OnDamageDealt?.Invoke(this, receiver, amount, isDoT);
  public event Action OnAttacked;
  public void HasAttacked() => OnAttacked?.Invoke();
  public event Action<Entity, Surface> OnSurfaceWalked;
  public void SurfaceWalked(Entity entity, Surface surface) => OnSurfaceWalked?.Invoke(this, surface);

  public virtual void Awake() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Animator = GetComponent<Animator>();
    Health = GetComponent<IHealth>();
    Stats = GetComponent<Stats>();
    UpgradeVFX = GetComponent<ApplyUpgradeVFX>();
    Blaster = GetComponentInChildren<Blaster>(true);
  }

  private void OnEnable() {
    Health.OnDeath += OnDeath;
    Health.OnDamageTaken += OnDamageTaken;
  }

  private void OnDisable() {
    Health.OnDeath -= OnDeath;
  }

  private void Update() => Looking();
  private void FixedUpdate() => Moving();

  public virtual void Moving() {
    Rigidbody.AddForce(Speed * Direction, ForceMode2D.Impulse);
    IsMoving = Direction.magnitude > 0;
  }

  public virtual void Looking() {
    Sprite.flipX = transform.position.x > Target.position.x;
    Animator.SetFloat("Direction", (Target.position - transform.position).y > 0.6f ? 1 : -1);
    Animator.SetBool("IsMoving", IsMoving);
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


  public virtual void OnDeath(Entity entity) { }


  public void AddEffect(Effect effect) {
    foreach (Effect e in Effects) {
      if (e.GetType() == effect.GetType()) {
        e.Add(effect);
        return;
      }
    }

    effect.Entity = this;
    StartCoroutine(effect.EffectRoutine());
  }

  public void RemoveEffects() {
    Effects.ForEach(effect => StopCoroutine(effect.EffectRoutine()));
    Effects = new();
  }
}
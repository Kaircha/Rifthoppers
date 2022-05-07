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
  [HideInInspector] public InputDevice Device;
  [HideInInspector] public InputData Input;
  [HideInInspector] public Vector2 Direction;
  [HideInInspector] public bool IsMoving;
  public SpriteRenderer Sprite;
  public AudioSource HurtAudio;
  public Transform Target;
  public float Speed;
  public float AISpeed;

  // Effects
  public List<PoisonEffect> Poisons = new();
  public IgniteEffect Ignite = new(0, 0);

  // Dealer, Receiver, Amount, isDoT
  public event Action<Entity, Entity, float, bool> OnDamageDealt;
  public void HasDealtDamage(Entity receiver, float amount, bool isDoT) => OnDamageDealt?.Invoke(this, receiver, amount, isDoT);
  public event Action OnAttacked;
  public void HasAttacked() => OnAttacked?.Invoke();

  private void Awake() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Animator = GetComponent<Animator>();
    Health = GetComponent<IHealth>();
    Stats = GetComponent<Stats>();
  }

  private void OnEnable() {
    Health.OnDeath += Death;
    Health.OnDamageTaken += Hurt;
  }

  private void OnDisable() {
    Health.OnDeath -= Death;
  }

  private void Update() {
    Looking();
    Effects();
  }

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
  public virtual void Hurt(Entity dealer, Entity target, float amount, bool isDoT) {
    if (HurtAudio != null && HurtAudio.clip != null) HurtAudio.Play();
  }
  public virtual void Death(Entity entity) { }
  
  public void Effects() {
    if (Ignite.Duration > 0) {
      Health.Hurt(null, this, Ignite.Damage, true);
      Ignite.Duration -= Time.deltaTime;
    }

    if (Poisons.Count > 0) {
      Health.Hurt(null, this, Poisons.Sum(x => x.Damage), true);
      foreach (PoisonEffect poison in Poisons) poison.Duration -= Time.deltaTime;
      Poisons.RemoveAll(x => x.Duration <= 0);
    } 
  }
}
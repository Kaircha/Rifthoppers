using System;
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
  //[HideInInspector] public StateMachine Machine;
  [HideInInspector] public Rigidbody2D Rigidbody;
  [HideInInspector] public Animator Animator;
  [HideInInspector] public IHealth Health;
  [HideInInspector] public InputDevice Device;
  [HideInInspector] public InputData Input;
  [HideInInspector] public Vector2 Direction;
  [HideInInspector] public bool IsMoving;
  public SpriteRenderer Sprite;
  public Transform Target;
  public float Speed;

  public event Action<Entity, Entity, float, bool> OnDamageDealt;
  public void HasDealtDamage(Entity receiver, float amount, bool isDoT) => OnDamageDealt?.Invoke(this, receiver, amount, isDoT);
  public event Action OnAttacked;
  public void HasAttacked() => OnAttacked?.Invoke();

  private void Awake() {
    //Machine = GetComponent<StateMachine>();
    Rigidbody = GetComponent<Rigidbody2D>();
    Animator = GetComponent<Animator>();
    Health = GetComponent<IHealth>();
  }

  private void OnEnable() => Health.OnDeath += Death;
  private void OnDisable() => Health.OnDeath -= Death;
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

  public virtual void Death(Entity entity) { }
}
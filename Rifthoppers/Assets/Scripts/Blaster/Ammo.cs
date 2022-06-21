using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour, IPoolable {
  [HideInInspector] public Rigidbody2D Rigidbody;
  [HideInInspector] public Collider2D Collider;
  [HideInInspector] public PlayerBrain Brain;

  public Pool Pool { get; set; }

  public void OnEnable() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Collider = GetComponent<Collider2D>();
  }

  public void FixedUpdate() => Homing();

  public abstract void Homing();
  public abstract void Reflect(Collision2D collision);
  public abstract void Pierce(Collider2D collider);
  public abstract void Split(Collider2D collider);
}
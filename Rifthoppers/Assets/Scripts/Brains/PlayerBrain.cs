using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : Brain {
  [HideInInspector] public InputDevice Device; // Set by The LobbyManager
  [HideInInspector] public InputData Input; // Set by The LobbyManager
  [HideInInspector] public PlayerStats Stats;
  [HideInInspector] public Blaster Blaster;
  [HideInInspector] public Power Power;
  [HideInInspector] public Dodge Dodge;
  [HideInInspector] public Immunity Immunity;
  [HideInInspector] public Orbitals Orbitals;
  [HideInInspector] public Upgrades Upgrades;

  public override void Awake() {
    base.Awake();
    Stats = GetComponent<PlayerStats>();
    Blaster = GetComponentInChildren<Blaster>(true);
    Power = GetComponentInChildren<Power>(true);
    Dodge = GetComponentInChildren<Dodge>(true);
    Immunity = GetComponentInChildren<Immunity>(true);
    Orbitals = GetComponentInChildren<Orbitals>(true);
    Upgrades = GetComponentInChildren<Upgrades>(true);
    Upgrades.Brain = this;
  }

  public void Update() {
    if (Target == null) return;
    Entity.Sprite.flipX = transform.position.x > Target.position.x;
    Entity.Animator.SetFloat("Direction", (Target.position - transform.position).y > 0.6f ? 1 : -1);
  }

  public void EnterAIState() => Machine.ChangeState(new PlayerAIState(this));
  public void EnterInteractState() => Machine.ChangeState(new PlayerInteractState(this));
  public void EnterCombatState() => Machine.ChangeState(new PlayerCombatState(this));
}
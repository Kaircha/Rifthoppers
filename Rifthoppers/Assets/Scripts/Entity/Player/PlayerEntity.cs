using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : Entity {
  public StateMachine Machine;
  public PlayerStateType stateType;

  public delegate void Delegate();
  public Delegate AttackHit;

  public void Start() {
    Machine = GetComponent<StateMachine>();
    switch (stateType) {
      case PlayerStateType.AI: EnterAIState(); break;
      case PlayerStateType.Hub: EnterHubState(); break;
      case PlayerStateType.Rift: EnterRiftState(); break;
    }
  }

  public void EnterAIState() => Machine.State = new PlayerAIState(this);
  public void EnterHubState() => Machine.State = new PlayerHubState(this);
  public void EnterRiftState() => Machine.State = new PlayerRiftState(this);

  public void AddCallback(Delegate callbackMethod) {
    AttackHit += callbackMethod;
  }
  public void RemoveCallback(Delegate callbackMethod) {
    AttackHit -= callbackMethod;
  }
}

// TEMPORARY!
public enum PlayerStateType { 
  AI,
  Hub,
  Rift
}
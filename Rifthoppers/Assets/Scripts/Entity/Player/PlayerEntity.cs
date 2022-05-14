using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : Entity {
  public StateMachine Machine;

  public override void Awake() {
    base.Awake();
    Machine = GetComponent<StateMachine>();
  }

  public void EnterAIState() => Machine.ChangeState(new PlayerAIState(this));
  public void EnterLabState() => Machine.ChangeState(new PlayerLabState(this));
  public void EnterRiftState() => Machine.ChangeState(new PlayerRiftState(this));
}
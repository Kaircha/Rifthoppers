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

  public void EnterAIState() => Machine.State = new PlayerAIState(this);
  public void EnterLabState() => Machine.State = new PlayerLabState(this);
  public void EnterRiftState() => Machine.State = new PlayerRiftState(this);
}
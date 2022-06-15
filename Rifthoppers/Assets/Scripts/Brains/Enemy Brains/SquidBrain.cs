using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBrain : EnemyBrain {
  public override State EntryState => ChaseState;

  public EnemyChaseState ChaseState;
  public EnemyDeathState DeathState;

  public override void InitializeTransitions() {
    ChaseState = new(this);
    DeathState = new(this);

    Transitions = new List<Transition> {
      new(DeathState, new WaveEndedCondition()),
      new(DeathState, new DeathCondition(Entity)),
    };
  }
}
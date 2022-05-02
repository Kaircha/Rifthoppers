using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBrain : Brain {
  public override State EntryState => ChaseState;

  public EnemyChaseState ChaseState;
  public EnemyDeathState DeathState;

  public override void Initialize() {
    ChaseState = new(Entity);
    DeathState = new(Entity);

    Transitions = new List<Transition> {
      new(DeathState, new WaveEndedCondition()),
      new(DeathState, new DeathCondition(Entity)),
    };
  }
}
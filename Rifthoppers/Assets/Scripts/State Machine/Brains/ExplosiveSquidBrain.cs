using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSquidBrain : Brain {
  public override State EntryState => ChaseState;

  public EnemyChaseState ChaseState;
  public ExplosiveEnemyDeathState DeathState;

  public override void Initialize() {
    ChaseState = new(Entity);
    DeathState = new(Entity);

    Transitions = new List<Transition> {
      new(DeathState, new WaveEndedCondition()),
      new(DeathState, new DeathCondition(Entity)),
    };
  }
}

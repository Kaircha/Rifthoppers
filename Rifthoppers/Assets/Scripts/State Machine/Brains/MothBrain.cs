using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothBrain : Brain {
  public override State EntryState => PoolManager.Instance.EnergyOrbs.Objects.CountActive > 0 ? OrbState : ChaseState;

  public EnemyOrbState OrbState;
  public EnemyChaseState ChaseState;
  public EnemyDeathState DeathState;

  public override void Initialize() {
    OrbState = new(Entity);
    ChaseState = new(Entity);
    DeathState = new(Entity);

    Transitions = new List<Transition> {
      new(OrbState, ChaseState, new EnergyCollectedCondition(1)),
      new(ChaseState, OrbState, new EnergyOrbSpawnedCondition(1)),
      new(DeathState, new WaveEndedCondition()),
      new(DeathState, new DeathCondition(Entity)),
    };
  }
}
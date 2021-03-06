using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidmotherBrain : EnemyBrain {
  public EnemySpawnData SpawnData;
  public Transform SpawnOrigin;

  public override State EntryState => ChaseState;

  public EnemyChaseState ChaseState;
  public EnemyFleeState FleeState;
  public EnemySpawnState SpawnState;
  public EnemyDeathState DeathState;

  public override void InitializeTransitions() {
    ChaseState = new(this);
    FleeState  = new(this);
    SpawnState = new(this, SpawnData, SpawnOrigin);
    DeathState = new(this);

    Transitions = new List<Transition> {
      new(SpawnState, FleeState, new DistanceCondition(Entity, TargetType.ClosestPlayer, 8, ComparisonType.Less)),
      new(FleeState, SpawnState, new DistanceCondition(Entity, TargetType.ClosestPlayer, 8, ComparisonType.Greater)),
      new(ChaseState, SpawnState, new DistanceCondition(Entity, TargetType.ClosestPlayer, 16, ComparisonType.Less)),
      new(SpawnState, ChaseState, new DistanceCondition(Entity, TargetType.ClosestPlayer, 16, ComparisonType.Greater)),
      new(DeathState, new WaveEndedCondition()),
      new(DeathState, new DeathCondition(Entity)),
    };
  }

  private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, 8f);
    Gizmos.DrawWireSphere(transform.position, 16f);
  }
}
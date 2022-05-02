using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State {
  public Entity Entity;

  public EnemyIdleState(Entity entity) => Entity = entity;

  public override IEnumerator Execute() {
    yield return null;
  }
}

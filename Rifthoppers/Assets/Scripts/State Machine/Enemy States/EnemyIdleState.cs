using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State {
  public EnemyBrain Brain;

  public EnemyIdleState(EnemyBrain brain) => Brain = brain;

  public override IEnumerator Execute() {
    Brain.Entity.Direction = Vector2.zero;
    yield return null;
  }
}

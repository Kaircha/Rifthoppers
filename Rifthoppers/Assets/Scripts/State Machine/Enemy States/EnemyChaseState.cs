using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : State {
  public EnemyBrain Brain;

  public EnemyChaseState(EnemyBrain brain) => Brain = brain;

  public override void Enter() {
    Brain.Target = LobbyManager.Instance.GetClosest(Brain.transform.position).transform;
  }

  public override IEnumerator Execute() {
    while (true) {
      Vector2 direction = (Brain.Target.position - Brain.transform.position).normalized;
      Brain.Entity.Direction = Brain.Stats.Speed * direction;
      
      yield return null;
    }
  }
}
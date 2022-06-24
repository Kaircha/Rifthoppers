using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeState : State {
  public EnemyBrain Brain;

  public EnemyFleeState(EnemyBrain brain) => Brain = brain;


  public override IEnumerator Execute() {
    while (true) {
      Brain.Target = LobbyManager.Instance.GetClosest(Brain.transform.position).transform;

      Vector2 direction = (Brain.transform.position - Brain.Target.position).normalized;
      Brain.Entity.Direction = Brain.Stats.Speed * direction;
      
      yield return null;
    }
  }
}
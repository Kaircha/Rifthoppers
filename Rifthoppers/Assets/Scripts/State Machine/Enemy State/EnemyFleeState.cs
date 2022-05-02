using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeState : State {
  public Entity Entity;

  public EnemyFleeState(Entity entity) => Entity = entity;


  public override IEnumerator Execute() {
    while (true) {
      Entity.Target = LobbyManager.Instance.GetClosest(Entity.transform.position).transform;
      Entity.Direction = (Entity.transform.position - Entity.Target.position).normalized;
      
      yield return null;
    }
  }
}
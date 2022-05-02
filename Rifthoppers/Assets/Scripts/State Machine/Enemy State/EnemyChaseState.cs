using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : State {
  public Entity Entity;

  public EnemyChaseState(Entity entity) => Entity = entity;

  public override void Enter() {
    Entity.Target = LobbyManager.Instance.GetClosest(Entity.transform.position).transform;
  }

  public override IEnumerator Execute() {
    while (true) {
      Entity.Direction = (Entity.Target.position - Entity.transform.position).normalized;
      
      yield return null;
    }
  }
}
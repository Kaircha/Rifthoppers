using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyChaseState : State
{
  public Entity Entity;

  public ExplosiveEnemyChaseState(Entity entity) => Entity = entity;

  public override void Enter()
  {
    Entity.Target = LobbyManager.Instance.GetClosest(Entity.transform.position).transform;
  }

  public override IEnumerator Execute()
  {
    while (true)
    {
      Entity.Direction = (Entity.Target.position - Entity.transform.position).normalized;

      yield return null;
    }
  }
}

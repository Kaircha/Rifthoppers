using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnState : State {
  public EnemyBrain Brain;
  public EnemySpawnData SpawnData;
  public Transform Origin;

  public EnemySpawnState(EnemyBrain brain, EnemySpawnData spawnData, Transform origin) {
    Brain = brain;
    SpawnData = spawnData;
    SpawnData.Pool = PoolManager.Instance.Squidlings;
    Origin = origin;
  }

  public override void Enter() {
    Brain.Target = LobbyManager.Instance.GetClosest(Brain.transform.position).transform;
    Brain.Entity.Direction = Vector2.zero;
    Machine.StartCoroutine(SpawnRoutine());
  }

  public IEnumerator SpawnRoutine() {
    if (SpawnData == null || Origin == null) yield break;
    if (SpawnData.Rate <= 0 || SpawnData.Maximum <= 0) yield break;

    while (true) {
      float delay = 1 / SpawnData.Rate;

      yield return new WaitForSeconds(delay);
      yield return new WaitUntil(() => SpawnData.Pool.Objects.CountActive <= SpawnData.Maximum);
      GameObject enemy = SpawnData.Pool.Objects.Get();
      enemy.transform.position = Origin.position;
      enemy.GetComponent<EnemyBrain>().InitializeEntity();
    }
  }
}
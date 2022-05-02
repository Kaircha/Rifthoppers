using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnState : State {
  public Entity Entity;
  public EnemySpawnData SpawnData;
  public Transform Origin;

  public EnemySpawnState(Entity entity, EnemySpawnData spawnData, Transform origin) {
    Entity = entity;
    SpawnData = spawnData;
    SpawnData.Pool = PoolManager.Instance.SquidlingPool;
    Origin = origin;
  }

  public override void Enter() {
    Entity.Target = LobbyManager.Instance.GetClosest(Entity.transform.position).transform;
    Entity.Direction = Vector2.zero;
    Machine.StartCoroutine(SpawnRoutine());
  }

  public IEnumerator SpawnRoutine() {
    if (SpawnData == null || Origin == null) yield break;
    if (RiftManager.Instance.Experience.Level < SpawnData.Threshold || SpawnData.Rate <= 0 || SpawnData.Maximum <= 0) yield break;

    while (true) {
      float delay = 1 / (RiftManager.Instance.DifficultyMultiplier * RiftManager.Instance.DifficultyMultiplier * SpawnData.Rate);

      yield return new WaitForSeconds(delay);
      yield return new WaitUntil(() => SpawnData.Pool.Objects.CountActive <= SpawnData.Maximum);
      GameObject enemy = SpawnData.Pool.Objects.Get();
      enemy.transform.position = Origin.position;
      enemy.GetComponent<EnemyEntity>().HandleSpawn();
    }
  }
}
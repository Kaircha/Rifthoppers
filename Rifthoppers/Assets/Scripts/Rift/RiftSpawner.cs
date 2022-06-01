using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftSpawner : MonoBehaviour {
  public bool DebugPreventSpawning = false;
  public List<EnemySpawnData> SpawnData = new();

  public void StartSpawning() {
    StopSpawning();

    if (DebugPreventSpawning) return;

    foreach (EnemySpawnData spawnData in SpawnData) {
      StartCoroutine(SpawnRoutine(spawnData));
    }
    // Temporary; References break upon scene load due to Singleton
    SpawnData[0].Pool = PoolManager.Instance.Squids;
    SpawnData[1].Pool = PoolManager.Instance.Squidmothers;
    SpawnData[2].Pool = PoolManager.Instance.Squidlings;
    SpawnData[3].Pool = PoolManager.Instance.Moths;
    SpawnData[4].Pool = PoolManager.Instance.ExplosiveSquids;
  }

  public void StopSpawning() {
    StopAllCoroutines();
  }

  public IEnumerator SpawnRoutine(EnemySpawnData spawnData) {
    if ( spawnData.Rate <= 0 || spawnData.Maximum <= 0) yield break;

    float delay = 1 / spawnData.Rate;

    while (true) {
      yield return new WaitForSeconds(delay);
      yield return new WaitUntil(() => spawnData.Pool.Objects.CountActive <= spawnData.Maximum);
      GameObject enemy = spawnData.Pool.Objects.Get();
      enemy.transform.position = SpawnPosition();
      enemy.GetComponent<EnemyEntity>().HandleSpawn();
    }
  }

  Vector3 SpawnPosition() => 20f * Random.insideUnitCircle.normalized;
}

[System.Serializable]
public class EnemySpawnData {
  public float Rate;
  public int Threshold;
  public int Maximum;
  public Pool Pool;
}
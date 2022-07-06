using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  public List<EnemyCostPair> Enemies = new();
  public Queue<EnemyCostPair> Queue = new();
  public int Memory = 3;
  public float Points;
  public float Remaining;
  public float Maximum = 100f;
  private float EnergyMultiplier => RiftManager.Instance.EnergyMultiplier;
  private float EnemyMultiplier => Mathf.Clamp01(Remaining / Maximum);
  private float PointMultiplier => EnemyMultiplier * EnergyMultiplier;

  public void Start() {
    Remaining = Maximum;
    for (int i = 0; i < Memory; i++) Queue.Enqueue(RandomEnemy());
  }

  public IEnumerator StartSpawning() {
    Points += Time.deltaTime * PointMultiplier;
    if (Points >= Queue.Peek().Cost) {
      StartCoroutine(SpawnEnemy(Queue.Dequeue()));
      Queue.Enqueue(RandomEnemy());
    }
    yield return null;
  }

  public IEnumerator SpawnEnemy(EnemyCostPair enemy) {
    EnemyBrain brain = enemy.Pool.Objects.Get().GetComponent<EnemyBrain>();
    brain.transform.position = SpawnPosition();
    brain.Initialize();

    Remaining -= enemy.Cost;
    yield return new WaitUntil(() => brain.Entity.Health.IsDead);
    Remaining += enemy.Cost;
  }
  private Vector3 SpawnPosition() => 20f * Random.insideUnitCircle.normalized;

  public EnemyCostPair RandomEnemy() {
    List<EnemyCostPair> Options = Enemies.ToList();
    // Remove unfit options from the list
    return Options[Random.Range(0, Options.Count)];
  }
}

public class EnemyCostPair {
  public Pool Pool;
  public int Cost;
}
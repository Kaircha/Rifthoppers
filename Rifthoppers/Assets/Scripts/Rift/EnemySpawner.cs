using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class EnemySpawner : MonoBehaviour {
//  public List<Gan> Enemies = new();
//  public Queue<EnemyEntity> Queue = new();
//  public int Memory = 3;
//  public float Points;
//  public float Remaining;
//  public float Maximum = 100f;
//  private float EnergyMultiplier => RiftManager.Instance.EnergyMultiplier;
//  private float EnemyMultiplier => Mathf.Clamp01(Remaining / Maximum);
//  private float PointMultiplier => EnemyMultiplier * EnergyMultiplier;

//  public void Start() {
//    Remaining = Maximum;
//    for (int i = 0; i < Memory; i++) Queue.Enqueue(RandomEnemy());
//  }

//  public void Update() {
//    Points += Time.deltaTime * PointMultiplier;
//    if (Points >= Queue.Peek().Cost) {
//      SpawnEnemy(Queue.Dequeue());
//      Queue.Enqueue(RandomEnemy());
//    }
//  }

//  public void SpawnEnemy(EnemyEntity entity) {
//    GameObject enemy = entity.Pool.Objects.Get();
//    enemy.transform.position = SpawnPosition();
//    enemy.GetComponent<EnemyEntity>().HandleSpawn();

//    Remaining -= entity.Cost;
//    entity.Health.OnDeath += EnemyDeath;
//  }
//  private Vector3 SpawnPosition() => 20f * Random.insideUnitCircle.normalized;

//  public void EnemyDeath(Entity entity) {
//    Remaining += (entity as EnemyEntity).Cost;
//    entity.Health.OnDeath -= EnemyDeath;
//  }

//  public EnemyEntity RandomEnemy() {
//    List<EnemyEntity> Options = Enemies.ToList();
//    // Remove unfit options from the list
//    return Options[Random.Range(0, Options.Count)];
//  }
//}
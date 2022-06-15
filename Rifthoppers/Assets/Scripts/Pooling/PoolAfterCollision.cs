using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAfterCollision : MonoBehaviour, IPoolable {
  public Pool Pool { get; set; }

  private void OnTriggerEnter2D(Collider2D collision) => (this as IPoolable).Release(gameObject);
  private void OnCollisionEnter2D(Collision2D collision) => (this as IPoolable).Release(gameObject);
}
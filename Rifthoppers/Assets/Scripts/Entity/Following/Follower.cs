using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
  [HideInInspector] public Rigidbody2D Rigidbody;
  private Vector3 Target;
  private Vector3 Previous;

  public void Awake() {
    Rigidbody = GetComponent<Rigidbody2D>();
  }

  public void Movement(float distance) {
    Rigidbody.MovePosition(Vector3.Lerp(Previous, Target, distance));
  }

  public void LookAt(Vector3 position) {
    // Jittery
    transform.right = (position - transform.position).normalized;
  }

  public void SetTarget(Vector3 position) {
    if (position == Target) return;
    Previous = Target;
    Target = position;
  }
}
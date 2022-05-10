using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
  private Vector3 Target;
  private Vector3 Previous;

  public void Movement(float distance) {
    transform.position = Vector3.Lerp(Previous, Target, distance);
  }

  public void LookAt(Vector3 position) {
    transform.right = (position - transform.position).normalized;
  }

  public void SetTarget(Vector3 position) {
    if (position == Target) return;
    Previous = Target;
    Target = position;
  }
}
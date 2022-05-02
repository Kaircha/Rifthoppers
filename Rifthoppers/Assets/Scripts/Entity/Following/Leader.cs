using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
  public List<Vector2> Points = new();
  public List<Follower> Followers = new();
  public float Radius;

  // Temporary until Entity State Integration
  public Transform Target;

  private void Awake() {
    //Points.Add(transform.position);
    for (int i = 0; i < Followers.Count; i++) {
      Points.Add(new Vector2(i * -Radius, 0));
    }
  }

  private void Update() {
    float distance = Vector2.Distance(transform.position, Points[0]);
    
    if (distance >= Radius) {
      Points = Points.Prepend(transform.position).ToList();
      distance = Vector2.Distance(transform.position, Points[0]);
    }

    while (Points.Count > Followers.Count) {
      Points.RemoveAt(Points.Count - 1);
    }

    for (int i = 0; i < Followers.Count; i++) {
      if (i >= Points.Count) continue;
      Followers[i].SetTarget(Points[i]);
      Followers[i].Movement(distance / Radius);
      if (i == 0) Followers[i].LookAt(transform.position);
      else Followers[i].LookAt(Followers[i - 1].transform.position);
    }

    Movement();
  }

  private void Movement() {
    transform.position = Target.position;
  }

  public void OnDrawGizmos() {
    if (Points.Count == 0) return;
    Gizmos.color = Color.red;
    foreach (Vector2 point in Points) {
      Gizmos.DrawSphere(point, 0.5f);
    }
  }
}
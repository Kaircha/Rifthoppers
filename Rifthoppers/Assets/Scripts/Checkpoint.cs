using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
  public float Boundary;
  public float Distance;

  private void OnTriggerEnter2D(Collider2D collision) {
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Experience.Learn(24);
    transform.position = GetRandomPos(collision.transform.position);
    RiftManager.Instance.EnergyCollected();
  }

  Vector3 GetRandomPos(Vector3 point) {
    if (Distance >= Boundary) return Vector3.zero;
    Vector3 position = Boundary * Random.insideUnitCircle;
    if (Vector3.Distance(point, position) < Distance) return GetRandomPos(point);
    else return position;
  }
}
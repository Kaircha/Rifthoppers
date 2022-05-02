using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
  public float Boundary;
  public float Distance;
  public Transform Player;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.TryGetComponent(out PlayerEntity entity)) {
      Player = entity.transform;
    }
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Experience.Learn(24);
    transform.position = GetRandomPos();
  }

  Vector3 GetRandomPos() {
    if (Distance >= Boundary) return Vector3.zero;
    Vector3 position = Boundary * Random.insideUnitCircle;
    if (Vector3.Distance(Player.position, position) < Distance) return GetRandomPos();
    else return position;
  }
}
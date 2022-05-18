using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
  public float Boundary;
  public float Distance;
  public float Energy = 30;
  public int Progress = 25;
  public AudioSource AudioSource;

  private void OnTriggerEnter2D(Collider2D collision) {
    AudioSource.pitch = 0.5f + RiftManager.Instance.Experience.Progress;
    AudioSource.Play();

    RiftManager.Instance.Energy.Heal(Energy);
    RiftManager.Instance.Experience.Learn(Progress);
    transform.position = GetRandomPos(collision.transform.position);
    RiftManager.Instance.EnergyCollected();

  }

  public Vector3 GetRandomPos(Vector3 point) {
    if (Distance >= Boundary) return Vector3.zero;
    Vector3 position = Boundary * Random.insideUnitCircle;
    if (Vector3.Distance(point, position) < Distance) return GetRandomPos(point);
    else return position;
  }
}
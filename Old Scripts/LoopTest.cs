using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTest : MonoBehaviour {
  public float Speed;
  public float Radius;
  private float Angle;

  private void Update() {
    Angle += Speed * Time.deltaTime;
    transform.position = Radius * new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle));
  }

  private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(Vector3.zero, Radius);
  }
}
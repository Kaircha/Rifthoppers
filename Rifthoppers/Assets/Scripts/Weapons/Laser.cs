using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {
  public LineRenderer Line;
  public EdgeCollider2D Collider;

  public int Amount;
  public float Distance;
  public Vector2[] Points;
  public Vector2[] Destination;

  public Vector2 Direction;

  public void Start() {
    Points = new Vector2[Amount];
    Destination = new Vector2[Amount];
    Line.positionCount = Amount;
    Collider.points = new Vector2[Amount];

    for (int i = 0; i < Amount; i++) {
      Destination[i] = i * Distance * (transform.rotation * Vector2.right).normalized;
      Line.SetPosition(i, (Vector2)transform.position + Destination[i]);
      Points[i] = Destination[i];
    }
    Collider.points = Destination;
  }

  public void Update() => DrawLaser();

  public void DrawLaser() {
    for (int i = 0; i < Amount; i++) {
      Destination[i] = i * Distance * (transform.rotation * Vector2.right).normalized;
      Points[i] = Vector2.Lerp(Points[i], Destination[i], (Amount - 0.5f * i) * Time.deltaTime);
      Line.SetPosition(i, (Vector2)transform.position + Points[i]);
    }
    Collider.points = Points; 
  }
}
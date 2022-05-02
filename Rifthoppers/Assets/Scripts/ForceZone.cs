using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceZone : MonoBehaviour {
  public Vector2 Direction;
  public float Power;

  public void OnTriggerStay2D(Collider2D collision) {
    collision.attachedRigidbody.AddForce(Power * Direction, ForceMode2D.Impulse);
  }

  public void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(transform.position, 0.2f);
    Gizmos.DrawLine(transform.position, transform.position + (Vector3)Direction);
  }
}
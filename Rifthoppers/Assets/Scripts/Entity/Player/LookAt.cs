using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
  public Transform Target;
  public Vector3 Offset;
  public float Speed = 20f;
  private Vector3 Direction;

  private void OnEnable() {
    if (Target == null) Target = GameObject.FindWithTag("Player").transform;
  }

  private void Update() {
    Direction = (Target.position - transform.position + Offset).normalized;
    // Introduces a tiny bias to make Quaternion.Lerp rotate along the bottom when going between extremes -180 and 180.
    // This bias only occurs when using a joystick/gamepad, and makes the gun's rotation look better.
    float angle = Mathf.Atan2(Direction.y - 0.001f, Direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Speed * Time.deltaTime);
  }
}
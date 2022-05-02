using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBetween : MonoBehaviour {
  public Transform A;
  public Transform B;
  [Range(0, 1)] public float Bias = 0.5f;

  void Update() {
    if (A == null || B == null) return;
    transform.position = (1 - Bias) * A.position + Bias * B.position;
  }
}

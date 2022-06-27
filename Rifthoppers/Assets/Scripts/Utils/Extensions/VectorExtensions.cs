using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions {
  public static List<Vector3> Cone(this Vector3 vec, int amount, float start, float spread) {
    if (amount <= 0 || spread == 0) return null;
    List<Vector3> vecs = new();
    start %= 2f * Mathf.PI;
    spread %= 2f * Mathf.PI;
    float A = start - 0.5f * spread;
    float B = start + 0.5f * spread;
    for (float i = 0; i < amount; i++) {
      float t = amount <= 1 ? 0.5f : i / (amount - 1);
      float angle = Mathf.Lerp(A, B, t);
      vecs.Add(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));
    }
    return vecs;
  }

  public static List<Vector3> Nova(this Vector3 vec, int amount) {
    if (amount <= 0) return null;
    List<Vector3> vecs = new();
    for (float i = 0; i < amount; i++) {
      float angle = 2f * Mathf.PI * i / amount;
      vecs.Add(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));
    }
    return vecs;
  }
}
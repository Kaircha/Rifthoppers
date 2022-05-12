using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColliderGenerator : MonoBehaviour {
  public float Radius = 1f;
  public int Resolution = 32;
  public bool PolygonOrEdge = true;

  [ContextMenu("Generate")]
  public void Generate() {
    if (Radius <= 0 || Resolution <= 0) return;
    if (TryGetComponent(out Collider2D old)) DestroyImmediate(old);

    if (PolygonOrEdge) {
      Vector2[] points = new Vector2[Resolution];

      for (int i = 0; i < Resolution; i++) {
        float angle = i * 2 * Mathf.PI / Resolution;
        points[i] = Radius * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
      }

      PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
      collider.points = points;
    } else {
      Vector2[] points = new Vector2[Resolution + 1];

      for (int i = 0; i < Resolution; i++) {
        float angle = i * 2 * Mathf.PI / Resolution;
        points[i] = Radius * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
      }
      points[Resolution] = points[0];

      EdgeCollider2D collider = gameObject.AddComponent<EdgeCollider2D>();
      collider.points = points;
    }
  }
}
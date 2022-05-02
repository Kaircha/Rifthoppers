using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolyToEdgeCollider : MonoBehaviour {
  // Techincally imperfect. Doesn't create a full loop

  [ContextMenu("Convert")]
  public void Convert() {
    PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
    EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
    edge.points = poly.points;
    DestroyImmediate(poly);
    DestroyImmediate(this);
  }
}

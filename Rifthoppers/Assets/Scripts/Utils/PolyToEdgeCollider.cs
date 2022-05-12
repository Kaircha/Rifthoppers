using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolyToEdgeCollider : MonoBehaviour {
  [ContextMenu("Convert")]
  public void Convert() {
    PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
    EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();

    List<Vector2> points = poly.points.ToList();
    points.Add(points[0]);

    edge.points = points.ToArray();
    
    DestroyImmediate(poly);
    DestroyImmediate(this);
  }
}

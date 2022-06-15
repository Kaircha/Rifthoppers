using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFollow : MonoBehaviour {
  public LineRenderer LineRenderer;
  public Transform A;
  public Transform B;

  private void Update() => Reposition();

  [ContextMenu("Update")]
  public void Reposition() {
    LineRenderer.SetPosition(0, A.position);
    LineRenderer.SetPosition(1, B.position);     
  }
}
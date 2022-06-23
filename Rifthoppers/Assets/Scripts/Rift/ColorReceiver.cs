using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReceiver : MonoBehaviour {
  public SpriteRenderer Grass;
  public SpriteRenderer Leaves;
  public SpriteRenderer Primary;

  public void Assign(Color grassColor, Color leavesColor, Color primaryColor) {
    if (Grass) Grass.color = grassColor;
    if (Leaves) Leaves.color = leavesColor;
    if (Primary) Primary.color = primaryColor;
  }

  [ContextMenu("Get References")]
  public void GetReferences() {
    foreach (Transform child in transform) {
      if (child.name == "Grass") Grass = child.GetComponent<SpriteRenderer>();
      if (child.name == "Leaves") Leaves = child.GetComponent<SpriteRenderer>();
      if (child.name == "Primary") Primary = child.GetComponent<SpriteRenderer>();
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoHandler : MonoBehaviour
{
  // temporary script for adding gizmos to objects for testing 

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, 5.5f);
  }
}

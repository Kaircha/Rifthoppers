using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Chance {
  public static bool Percent(float percent) => percent > Random.Range(0f, 100f);
}
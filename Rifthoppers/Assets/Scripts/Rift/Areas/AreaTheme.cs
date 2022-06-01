using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Area Theme", menuName = "Area Theme")]
public class AreaTheme : ScriptableObject {
  // A class for decorations where each has
  // A hard radius for its size, to prevent overlap
  // A soft radius for spreading, to prevent clustering
  public List<GameObject> Objects;
  public List<GameObject> Decorations;

  public List<Sprite> Backgrounds = new();
  public Gradient Gradient;
}
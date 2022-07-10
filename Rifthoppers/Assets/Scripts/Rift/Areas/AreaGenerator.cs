using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGenerator : MonoBehaviour {
  public Area BaseArea;
  public List<AreaTheme> Themes = new();

  public Area EncounterArea(int index) {
    AreaTheme theme = Themes[index];
    Area area = Instantiate(BaseArea);
    
    // Insert Seed here!
    Sprite sprite = theme.Backgrounds[Random.Range(0, theme.Backgrounds.Count)];
    Color color = theme.Gradient.Evaluate(Random.value);

    area.Renderer.sprite = sprite;
    area.Renderer.color = color;
    area.Radius = 20f;

    for (int i = 0; i < 20; i++) {
      SpriteRenderer decoration = Instantiate(theme.Decorations[Random.Range(0, theme.Decorations.Count)], area.transform).GetComponent<SpriteRenderer>();
      decoration.transform.position = 20f * Random.insideUnitCircle;
      decoration.flipX = Random.value > 0.5f;
      decoration.color = color;
    }

    area.Hide();
    return area;
  }
}
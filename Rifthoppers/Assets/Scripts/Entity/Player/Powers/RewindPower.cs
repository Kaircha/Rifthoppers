using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RewindPower : Power {
  public ParticleSystem RewindSystem;
  public AudioSource RewindSource;
  public SpriteRenderer MainRenderer;
  private SpriteRenderer RewindRenderer;
  Vector3 RewindPosition;

  public override void Press() {
    RewindPosition = Entity.transform.position;
    
    RewindRenderer = new GameObject().AddComponent<SpriteRenderer>();
    RewindRenderer.sprite = MainRenderer.sprite;
    RewindRenderer.flipX = MainRenderer.flipX;
    RewindRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    RewindRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
    RewindRenderer.transform.position = RewindPosition;
  }

  public override void Hold() { }

  public override void Release() {
    Entity.transform.position = RewindPosition;

    Destroy(RewindRenderer.gameObject);
  }
}
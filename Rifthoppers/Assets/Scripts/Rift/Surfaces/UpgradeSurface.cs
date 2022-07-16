using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSurface : Surface {
  private UpgradeObject UpgradeObject;
  public SpriteRenderer Renderer;
  // VFX
  public Coroutine Routine;
  public Transform Background;

  public void Initialize(UpgradeObject upgradeObject) {
    UpgradeObject = upgradeObject;
    Renderer.sprite = upgradeObject.Sprite;
  }

  public override void SurfaceEnter(Entity entity) {
    if (entity.TryGetComponent(out PlayerBrain brain)) {
      if (Routine != null) StopCoroutine(Routine);
      Routine = StartCoroutine(VFXRoutine(1.15f, 0.1f));
      RewardManager.Instance.UpgradePairs.Add(new UpgradePair(brain.Upgrades, UpgradeObject.Upgrade()));
    }
  }

  public override void SurfaceStay(Entity entity) { }

  public override void SurfaceExit(Entity entity) {
    if (entity.TryGetComponent(out PlayerBrain brain)) {
      if (Entities.Count == 0) {
        if (Routine != null) StopCoroutine(Routine);
        Routine = StartCoroutine(VFXRoutine(1f, 0.05f));
      }
      RewardManager.Instance.UpgradePairs.RemoveAll(x => x.Upgrades == brain.Upgrades);

      //List<UpgradePair> toRemove = new();

      //foreach (UpgradePair pair in RewardManager.Instance.UpgradePairs)
      //  if (pair.Upgrades == brain.Upgrades)
      //    toRemove.Add(pair);

      //foreach (UpgradePair pair in toRemove)
      //  RewardManager.Instance.UpgradePairs.Remove(pair);

      //toRemove.Clear();
    }
  }

  public IEnumerator VFXRoutine(float size, float alpha) {
    Vector3 startVec = Background.localScale;
    Vector3 endVec = size * 24 * Vector3.one;
    SpriteRenderer renderer = Background.GetComponent<SpriteRenderer>();
    Color startCol = renderer.color;
    Color endCol = new Color(startCol.r, startCol.g, startCol.b, alpha);
    float timer = 0;
    while (timer <= 1) {
      Background.localScale = Vector3.Lerp(startVec, endVec, timer);
      renderer.color = Color.Lerp(startCol, endCol, timer);
      timer += 5f * Time.deltaTime;
      yield return null;
    }
    Background.localScale = endVec;
    renderer.color = endCol;
  }
}

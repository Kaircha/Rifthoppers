using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftLoader : MonoBehaviour {
  // Temporary until a second rift is needed
  public List<GameObject> Waves = new List<GameObject>();
  public GameObject Upgrade;
  public GameObject Dead;
  public Material FadeInMaterial;
  public Material FadeOutMaterial;

  public GameObject CurrentArea;
  private GameObject TempArea;

  // These should play an animation for the load
  public void LoadDead() {
    StopAllCoroutines();
    TempArea = Instantiate(Dead, CurrentArea.transform);
  }

  public void LoadWave() {
    StopAllCoroutines();
    StartCoroutine(Load(Waves[Random.Range(0, Waves.Count)]));
  }

  public void LoadUpgrade() {
    StopAllCoroutines();
    TempArea = Instantiate(Upgrade, CurrentArea.transform);
  }

  public IEnumerator Load(GameObject ToSpawn) {
    if (TempArea != null) Destroy(TempArea);
    GameObject newArea = Instantiate(ToSpawn, transform);

    FadeOutMaterial.SetFloat("_Alpha", 0f);

    SetAreaMaterial(newArea, FadeInMaterial);
    SetAreaMaterial(CurrentArea, FadeOutMaterial);

    float timer = 0f;
    while (timer < 1f) {
      FadeInMaterial.SetFloat("_Alpha", timer);
      FadeOutMaterial.SetFloat("_Alpha", timer);
      timer += Time.deltaTime;
      yield return null;
    }
    timer = 1f;
    FadeInMaterial.SetFloat("_Alpha", timer);
    FadeOutMaterial.SetFloat("_Alpha", timer);

    Destroy(CurrentArea);
    CurrentArea = newArea;
  }

  public void SetAreaMaterial(GameObject area, Material material) {
    if (area.TryGetComponent(out SpriteRenderer mainRenderer)) {
      mainRenderer.material = material;
    }
    foreach (Transform child in area.transform) {
      if (child.TryGetComponent(out SpriteRenderer childRenderer)) {
        childRenderer.material = material;
      }
    }
  }
}
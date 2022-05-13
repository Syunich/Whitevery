using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

public class TitleMoving : MonoBehaviour
{
  [SerializeField] private CanvasGroup TitleCG;
  [SerializeField] private Text TitleShadeL;
  [SerializeField] private Text TitleShadeR;
  [SerializeField] private float IndicateTime;
  [SerializeField] private ParticleSystem particle;
  [SerializeField] private Text PlayText;
  [SerializeField] private Text TutorialText;
  [SerializeField] private GameObject[] TitleTiles;
  [SerializeField] private MeshRenderer[] TitleMeshes;
  [SerializeField] private CanvasGroup fadeCG;
  public IEnumerator IndicateTitle()
  {
    TitleCG.alpha = 1;
    Instantiate(particle);
    Sequence seq = DOTween.Sequence();
    seq.Append(TitleShadeL.transform.DOLocalMoveX(-150, IndicateTime).SetEase(Ease.OutQuart));
    seq.Join(TitleShadeR.transform.DOLocalMoveX(150, IndicateTime).SetEase(Ease.OutQuart));
    seq.Join(TitleShadeL.DOFade(0, IndicateTime));
    seq.Join(TitleShadeR.DOFade(0, IndicateTime));

    yield return seq.Play().WaitForCompletion();
    Sequence seq_text = DOTween.Sequence();
    seq_text.Append(PlayText.transform.DOLocalMoveX(PlayText.transform.localPosition.x - 500, 1.5f).From()
      .SetEase(Ease.OutQuad));
    seq_text.Join(TutorialText.transform.DOLocalMoveX(TutorialText.transform.localPosition.x - 500, 1.5f).From()
      .SetEase(Ease.OutQuad));
    seq_text.Join(fadeCG.DOFade(1, 1.5f));
    yield return seq_text.Play().WaitForCompletion();
    yield return StartCoroutine(FadeInTile(1.0f));
  }

  private IEnumerator FadeInTile(float time)
  {
    foreach (var meshRenderer in TitleMeshes)
    {
     meshRenderer.materials[1].color = new Color(meshRenderer.materials[1].color.r, meshRenderer.materials[1].color.g, meshRenderer.materials[1].color.b, 0);
    }
    foreach (var titleTile in TitleTiles)
    {
      titleTile.SetActive(true);
    }
    
    Sequence seq = DOTween.Sequence();
    foreach (var meshRenderer in TitleMeshes)
    {
      seq.Join(meshRenderer.materials[1].DOFade(1, time)).SetEase(Ease.InQuad);
    }
    yield return seq.Play().WaitForCompletion();
  }
  
}
  

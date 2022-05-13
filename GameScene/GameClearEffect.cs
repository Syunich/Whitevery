using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameClearEffect : MonoBehaviour
{
   [SerializeField] private ParticleSystem ClearParticle;
   [SerializeField] private CanvasGroup ClearCG;
   [SerializeField] private Image whiteBackGround;
   [SerializeField] private Image ClearUnderLine;
   [SerializeField] private GameObject[] ClearTiles;
   [SerializeField] private MeshRenderer[] ClearMeshes;
   [SerializeField] private TweetTileManager tweettile;
   public IEnumerator GameClearUIMoving()
   {
      Instantiate(ClearParticle);
      whiteBackGround.enabled = true;
      yield return new WaitForSeconds(0.8f);
      AudioManager.Instance.PlaySE(5);
      StartCoroutine(FadeInTile(1.5f));
      StartCoroutine(tweettile.FadeInTweetTile(1.5f));
      ClearUnderLine.transform.DOScaleX(10, 1.5f).SetEase(Ease.OutQuart).Play();
      yield return StartCoroutine(UpAndFadeIn(ClearCG, 1.5f));
      ClearCG.interactable = true;
   }

   private IEnumerator UpAndFadeIn(CanvasGroup CG, float time)
   {
      CG.DOFade(1, time).SetEase(Ease.OutQuart).Play();
      yield return CG.transform.DOLocalMoveY(transform.localPosition.y + 40, time).SetEase(Ease.OutQuart).Play()
         .WaitForCompletion();
   }

   private IEnumerator FadeInTile(float time)
   {
      foreach (var meshRenderer in ClearMeshes)
      {
         meshRenderer.materials[1].color = new Color(meshRenderer.materials[1].color.r, meshRenderer.materials[1].color.g, meshRenderer.materials[1].color.b, 0);
      }
      
      foreach (var clearTile in ClearTiles)
      {
         clearTile.SetActive(true);
      }

      Sequence seq = DOTween.Sequence();
      foreach (var meshRenderer in ClearMeshes)
      {
         seq.Join(meshRenderer.materials[1].DOFade(1, time)).SetEase(Ease.InQuad);
      }

      yield return seq.Play().WaitForCompletion();
   }
   
}

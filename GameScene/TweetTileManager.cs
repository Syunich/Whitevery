using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TweetTileManager : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] private bool IsStageTile;
  [SerializeField] private TileView view;
  [SerializeField] private ParticleSystem TweetParticle;
  [SerializeField] private SpriteRenderer OmoteIcon;
  [SerializeField] private SpriteRenderer UraIcon;
  [SerializeField] private MeshRenderer Mesh;
  private bool IsMoving = false;

  public void OnPointerClick(PointerEventData eventData)
  {
    if (!IsMoving)
    {
      IsMoving = true;
      if (IsStageTile)
      {
        StartCoroutine(TweetStageClear());
      }
      else
      {
        StartCoroutine(TweetGameClear());
      }
    }
  }

  IEnumerator TweetStageClear()
  {
    yield return StartCoroutine(view.Reverse());
    AudioManager.Instance.PlaySE(4);
    Instantiate(TweetParticle, view.gameObject.transform.position, Quaternion.identity);
    naichilab.UnityRoomTweet.Tweet("whitevery", $"ステージ{Info.StageNum}をクリアしました！", "WHITEVERY");
    IsMoving = false;
  }

  IEnumerator TweetGameClear()
  {
    yield return StartCoroutine(view.Reverse());
    AudioManager.Instance.PlaySE(4);
    Instantiate(TweetParticle, view.gameObject.transform.position, Quaternion.identity);
    naichilab.UnityRoomTweet.Tweet("whitevery", "全てを真っ白にしました！", "WHITEVERY");
    IsMoving = false;
  }

  public IEnumerator FadeInTweetTile(float sec)
  {
    this.gameObject.SetActive(true);
    Sequence seq = DOTween.Sequence();
    seq.Join(OmoteIcon.DOFade(1, sec).SetEase(Ease.InQuad));

    foreach (var VARIABLE in Mesh.materials)
    {
      seq.Join(VARIABLE.DOFade(1, sec).SetEase(Ease.InQuad));
    }

    yield return seq.Play().WaitForCompletion();
    UraIcon.DOFade(1, 0f).Play();
  }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AllClearUIMoving : MonoBehaviour
{
   [SerializeField] private CanvasGroup clearCG;
   [SerializeField] private TweetTileManager tweetTile;

   public IEnumerator Indicate()
   {
      yield return clearCG.DOFade(1, 1.5f).SetEase(Ease.OutQuart).Play().WaitForCompletion();
      yield return StartCoroutine(tweetTile.FadeInTweetTile(1.5f));
   }
}
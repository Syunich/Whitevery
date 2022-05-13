using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SyunichTool;

public class TutorialUIManager : MonoBehaviour
{
   [SerializeField] private ParticleSystem ClearParticle;
   [SerializeField] private CanvasGroup ClearCG;
   [SerializeField] private Image whiteBackGround;
   [SerializeField] private Image ClearUnderLine;

   //チュートリアル進捗具合テキスト
   [SerializeField] private TextMesh ProgressText;

   //チュートリアルの説明文テキスト
   [SerializeField] private TextMesh AttentionText;

   private Dictionary<int, string> TutorialInfodic = new Dictionary<int, string>()
   {
      {101, "【Click】 : Reverse tile"},
      {102, "【Z】 + 【Click】 : Reverse tiles in a cross"},
      {103, "【X】 + 【Click】 : Reverse tiles in a square"}
   };

   private void Awake()
   {
      int NowProgress = TutorialManager.TutorialNumber - 100;
      ProgressText.text = $"{NowProgress} / 3";
      AttentionText.text = TutorialInfodic[TutorialManager.TutorialNumber];
   }

   public IEnumerator TutorialClearUIMoving()
   {
      Instantiate(ClearParticle);
      whiteBackGround.enabled = true;
      yield return new WaitForSeconds(0.8f);
      AudioManager.Instance.PlaySE(5);
      ClearUnderLine.transform.DOScaleX(10, 1.5f).SetEase(Ease.OutQuart).Play();
      yield return StartCoroutine(UpAndFadeIn(ClearCG, 1.5f));
      yield return new WaitForSeconds(1.0f);
      ClearCG.interactable = true;
      //ちょっと待ったあとにシーン変更
      if (TutorialManager.TutorialNumber <= 103)
      {
         SceneChanger.Instance.ChangeScene("TutorialScene", 0);
      }
      else
      {
         SceneChanger.Instance.ChangeScene("TitleScene", 0);
         TutorialManager.TutorialNumber = 101;
      }
   }

   private IEnumerator UpAndFadeIn(CanvasGroup CG, float time)
   {
      CG.DOFade(1, time).SetEase(Ease.OutQuart).Play();
      yield return CG.transform.DOLocalMoveY(transform.localPosition.y + 40, time).SetEase(Ease.OutQuart).Play()
         .WaitForCompletion();
   }
}
   

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

/// <summary>
/// 現在ステージの情報など、共有ステータス
/// </summary>
public static class Info
{
   public static int StageNum = 1;
   public static bool IsSceneChanging = false;
   public static List<int> ClearStageNum = new List<int>() { };
   public static int CanReturnNum = 3;
   public static bool IsFirstLoaded = false;
   public static bool IsTutorial = false;
   
   public static bool IsAllCleared()
   {
       if (ClearStageNum.Count == 15)
       {
           return true;
       }

       return false;

   }
}



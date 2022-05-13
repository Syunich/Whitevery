using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainTextManager : MonoBehaviour
{
   [SerializeField] private TextMesh remaintext;

   public void IndicateRemain(int num)
   {
      if (num == 0)
      {
          remaintext.color = new Color(1, 0.2f, 0.2f, 1);
      }
      remaintext.text = num.ToString();
   }
}

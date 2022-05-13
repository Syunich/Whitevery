using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTextManager : MonoBehaviour
{
   [SerializeField] private GameObject Texts;
   [SerializeField] private TilesManager TM;
   private void Start()
   {
      float y = ((TM.ylength / 2) - TM.ylength - 1) * TM.LengthBetweenTile -
                (TM.LengthBetweenTile / 2.0f) * (1 - (TM.ylength) % 2) + 0.6f;
      Texts.transform.position = new Vector3(Texts.transform.position.x, y, Texts.transform.position.x);
   }
}

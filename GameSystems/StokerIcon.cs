using System;
using UnityEngine;
using UnityEngine.UI;

public class StokerIcon : MonoBehaviour
{
   [SerializeField] private SpriteRenderer IconSprite;
   [SerializeField] private Sprite CrossImage;
   [SerializeField] private Sprite SquareImage;

   private Vector3 MoucePosition;
   private Vector3 WorldPosition;
   private void Update()
   {
      if ((Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X)) || (!Input.GetKey(KeyCode.X) && !(Input.GetKey(KeyCode.Z))))
      {
         IconSprite.enabled = false;
         return;
      }

      IconSprite.enabled = true;
      if (Input.GetKey(KeyCode.Z))
      {
         IconSprite.sprite = CrossImage;
      }
      else if(Input.GetKey(KeyCode.X))
      {
         IconSprite.sprite = SquareImage;
      }

      MoucePosition = Input.mousePosition;
      MoucePosition.z = 10;
      WorldPosition = Camera.main.ScreenToWorldPoint(MoucePosition);
      WorldPosition += new Vector3(0.5f, 0.3f, 0);
      IconSprite.gameObject.transform.position = WorldPosition;


   }
}

using UnityEngine;

public class BackGroundRotate : MonoBehaviour
{
   [SerializeField] private float RotateAmount;

   private void FixedUpdate()
   {
       transform.Rotate(0,0,RotateAmount);
   }
}

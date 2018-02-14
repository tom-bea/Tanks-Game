using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimePill : Pill
{
   public delegate void IntFloatDelegate(int num, float x);
   public static event IntFloatDelegate ActivateStoppedTime;

   [SerializeField]
   private float m_LengthOfEffect = 3f;

   private void OnCollisionEnter(Collision collision)
   {
      TankMovement tank = collision.collider.GetComponent<TankMovement>();
      if (tank != null)
      {
         if (ActivateStoppedTime != null)
            ActivateStoppedTime(tank.m_PlayerNumber, m_LengthOfEffect);
      }
   }
}

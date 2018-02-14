using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPill : Pill
{
   [SerializeField]
   private float m_HealthToAdd = 10;

   private void OnCollisionEnter(Collision collision)
   {
      TankHealth tank = collision.collider.GetComponent<TankHealth>();
      if (tank != null)
      {
         tank.IncreaseHealth(m_HealthToAdd);
      }
   }
}

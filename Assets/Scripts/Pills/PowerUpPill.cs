using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPill : Pill
{
   private void OnCollisionEnter(Collision collision)
   {
      TankShooting tank = collision.collider.GetComponent<TankShooting>();
      if (tank != null)
      {
         tank.ActivatePowerUp();
      }
   }
}

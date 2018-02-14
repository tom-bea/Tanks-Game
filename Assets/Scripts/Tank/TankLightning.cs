using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankLightning : MonoBehaviour
{
   public ParticleSystem m_LightningGun;


   public void Fire(string fireButton)
   {
      if (Input.GetButtonDown(fireButton))
      {
         m_LightningGun.Play();
      }
      else if (Input.GetButtonUp(fireButton))
      {
         m_LightningGun.Stop();
      }
   }

   public void StopFiring()
   {
      m_LightningGun.Stop();
   }
}

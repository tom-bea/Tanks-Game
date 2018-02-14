using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIceGun : MonoBehaviour
{
   public ParticleSystem m_IceGun;


   public void Fire(string fireButton)
   {
      if (Input.GetButtonDown(fireButton))
      {
         m_IceGun.Play();
      }
      else if (Input.GetButtonUp(fireButton))
      {
         m_IceGun.Stop();
      }
   }

   public void StopFiring()
   {
      m_IceGun.Stop();
   }
}

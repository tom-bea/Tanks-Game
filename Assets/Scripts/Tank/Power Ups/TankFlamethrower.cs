using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFlamethrower : MonoBehaviour
{
   public ParticleSystem m_Flamethrower;
   public AudioClip m_FireClip;


   public void Fire(string fireButton, AudioSource source)
   {
      if (Input.GetButtonDown(fireButton))
      {
         m_Flamethrower.Play();
         source.clip = m_FireClip;
         source.loop = true;
         source.Play();
      }
      else if (Input.GetButtonUp(fireButton))
      {
         m_Flamethrower.Stop();
         source.Stop();
         source.loop = false;
      }
   }

   public void StopFiring(AudioSource source)
   {
      m_Flamethrower.Stop();
      source.Stop();
      source.loop = false;
   }
}

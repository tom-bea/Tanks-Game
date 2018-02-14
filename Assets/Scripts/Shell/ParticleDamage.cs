using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
   [SerializeField]
   private float m_DamagePerParticle = 1;

   private void OnParticleCollision(GameObject other)
   {
      TankHealth tank = other.GetComponent<TankHealth>();
      if (tank != null)
      {
         tank.TakeDamage(m_DamagePerParticle);
      }
   }
}

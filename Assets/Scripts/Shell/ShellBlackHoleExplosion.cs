using System.Collections;
using UnityEngine;

public class ShellBlackHoleExplosion : MonoBehaviour
{
   public LayerMask m_TankMask;
   public ParticleSystem m_ExplosionParticles;
   public float m_MaxDamage = 100f;
   public float m_ExplosionForce = -1000f;
   public float m_MaxLifeTime = 20f;
   public float m_ExplosionRadius = 10f;


   private void Start()
   {
      Destroy(gameObject, m_MaxLifeTime);
   }


   private void OnTriggerEnter(Collider other)
   {
      // Find all the tanks in an area around the shell and damage them.
      Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

      for (int i = 0; i < colliders.Length; i++)
      {
         Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

         if (!targetRigidbody)
            continue;

         StartCoroutine(Pull(targetRigidbody));

         TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

         if (!targetHealth)
            continue;

         StartCoroutine(DealDamage(targetHealth));
      }

      m_ExplosionParticles.transform.parent = null;

      m_ExplosionParticles.Play();

      Destroy(m_ExplosionParticles.gameObject, 20);
      Destroy(gameObject.GetComponent<MeshFilter>());
   }


   private float CalculateDamage(Vector3 targetPosition)
   {
      // Calculate the amount of damage a target should take based on it's position.
      Vector3 explosionToTarget = targetPosition - transform.position;

      float explosionDistance = explosionToTarget.magnitude; // pop pop

      float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

      float damage = relativeDistance * m_MaxDamage;

      damage = Mathf.Max(0f, damage);

      return damage;
   }

   private IEnumerator Pull(Rigidbody tank)
   {
      Vector3 dir = transform.position - tank.position;
      tank.AddForce(dir * m_ExplosionForce * m_ExplosionForce, ForceMode.Impulse);
      yield return null;
   }

   private IEnumerator DealDamage(TankHealth tank)
   {
      yield return new WaitForSeconds(7);

      tank.TakeDamage(100);

      Destroy(gameObject);
   }
}
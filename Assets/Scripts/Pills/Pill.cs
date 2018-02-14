using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Pill : MonoBehaviour
{
   [SerializeField]
   private float m_RotationSpeed = 3f;
   
   private Rigidbody m_Rb;
   
   protected void Awake()
   {
      m_Rb = GetComponent<Rigidbody>();
   }
   
   private void FixedUpdate()
   {
      if (!GameManager.GameStoppedPillActive)
         m_Rb.MoveRotation(m_Rb.rotation * Quaternion.Euler(0f, m_RotationSpeed, 0f));
   }

   private void OnEnable()
   {
      GameManager.EndRoundEvent += KillSelf;
   }

   private void OnDisable()
   {
      GameManager.EndRoundEvent -= KillSelf;
   }

   private void OnCollisionStay(Collision collision)
   {
      Destroy(gameObject);
   }

   private void KillSelf()
   {
      Destroy(gameObject);
   }
}

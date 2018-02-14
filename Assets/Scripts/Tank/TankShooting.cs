using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
   public int m_PlayerNumber = 1;
   public Rigidbody m_Shell;
   public Transform m_FireTransform;
   public Slider m_AimSlider;
   public AudioSource m_ShootingAudio;
   public AudioClip m_ChargingClip;
   public AudioClip m_FireClip;
   public float m_MinLaunchForce = 15f;
   public float m_MaxLaunchForce = 30f;
   public float m_MaxChargeTime = 0.75f;


   private string m_FireButton;
   private float m_CurrentLaunchForce;
   private float m_ChargeSpeed;
   private bool m_Fired;




   [Space(20)]

   public Rigidbody m_BlackHoleShell;

   [HideInInspector] public float m_PowerUpLength;
   [HideInInspector] public PowerType m_PowerUpType;
   
   private float m_TimeLeftOnPowerUp;
   private bool m_PowerUpActive;


   private bool m_CanShoot;
   public bool CanShoot
   {
      get { return m_CanShoot; }
      set { m_CanShoot = value; }
   }


   private void OnEnable()
   {
      m_CurrentLaunchForce = m_MinLaunchForce;
      m_AimSlider.value = m_MinLaunchForce;

      m_TimeLeftOnPowerUp = 0;
      m_PowerUpActive = false;

      m_CanShoot = true;
   }


   private void Start()
   {
      m_FireButton = "Fire" + m_PlayerNumber;

      m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
   }


   private void Update()
   {
      if (m_TimeLeftOnPowerUp > 0)
         m_TimeLeftOnPowerUp -= Time.deltaTime;
      else
      {
         m_PowerUpActive = false;
         m_TimeLeftOnPowerUp = 0;
         DeActivatePowerUp();
      }

      if (m_CanShoot && !PauseMenu.GameIsPaused)
      {
         // Track the current state of the fire button and make decisions based on the current launch force.
         m_AimSlider.value = m_MinLaunchForce;


         if (!m_PowerUpActive)
         {
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
               // at max charge, not fired
               m_CurrentLaunchForce = m_MaxLaunchForce;
               Fire(m_Shell);
            }
            else if (Input.GetButtonDown(m_FireButton))
            {
               // have we pressed fire for the first time??
               m_Fired = false;
               m_CurrentLaunchForce = m_MinLaunchForce;

               m_ShootingAudio.clip = m_ChargingClip;
               m_ShootingAudio.Play();
            }
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
               // holding the fire button, not yet fired
               m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

               m_AimSlider.value = m_CurrentLaunchForce;
            }
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
               // we release the button, having not fired yet
               Fire(m_Shell);
            }
         }
         else     // Using power up------------
         {
            if (m_CurrentLaunchForce > m_MinLaunchForce && m_PowerUpType != PowerType.Black_Hole)
            {
               Fire(m_Shell);
            }
            UsePowerUp();
         }
      }
   }


   private void Fire(Rigidbody shell)
   {
      // Instantiate and launch the shell.
      m_Fired = true;

      Rigidbody shellInstance = Instantiate(shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

      shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

      m_ShootingAudio.clip = m_FireClip;
      m_ShootingAudio.Play();

      m_CurrentLaunchForce = m_MinLaunchForce;
   }


   public void ActivatePowerUp()
   {
      m_PowerUpActive = true;
      m_TimeLeftOnPowerUp = m_PowerUpLength;
   }

   private void DeActivatePowerUp()
   {
      switch (m_PowerUpType)
      {
         case PowerType.Fire:
            GetComponent<TankFlamethrower>().StopFiring(m_ShootingAudio);
            break;
         case PowerType.Black_Hole:
            break;
         case PowerType.Ice:
            GetComponent<TankIceGun>().StopFiring();
            break;
         case PowerType.Lightning:
            GetComponent<TankLightning>().StopFiring();
            break;
      }
   }

   private void UsePowerUp()
   {
      switch (m_PowerUpType)
      {
         case PowerType.Fire:
            GetComponent<TankFlamethrower>().Fire(m_FireButton, m_ShootingAudio);
            break;
         case PowerType.Black_Hole:
            // Track the current state of the fire button and make decisions based on the current launch force.
            m_AimSlider.value = m_MinLaunchForce;

            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
               // at max charge, not fired
               m_CurrentLaunchForce = m_MaxLaunchForce;
               Fire(m_BlackHoleShell);
            }
            else if (Input.GetButtonDown(m_FireButton))
            {
               // have we pressed fire for the first time??
               m_Fired = false;
               m_CurrentLaunchForce = m_MinLaunchForce;

               m_ShootingAudio.clip = m_ChargingClip;
               m_ShootingAudio.Play();
            }
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
               // holding the fire button, not yet fired
               m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

               m_AimSlider.value = m_CurrentLaunchForce;
            }
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
               // we release the button, having not fired yet
               Fire(m_BlackHoleShell);
            }
            break;
         case PowerType.Ice:
            GetComponent<TankIceGun>().Fire(m_FireButton);
            break;
         case PowerType.Lightning:
            GetComponent<TankLightning>().Fire(m_FireButton);
            break;
      }
   }
}

public enum PowerType
{
   Fire,
   Black_Hole,
   Ice,
   Lightning
}
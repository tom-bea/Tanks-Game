using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillSpawner : MonoBehaviour
{
   [SerializeField]
   private PillStats[] m_Pills;

   [SerializeField]
   [Tooltip("The y-component is ignored.")]
   private Vector3 m_SpawnArea = Vector3.zero;


   private bool m_IsOn = false;

   private void Awake()
   {
      m_IsOn = false;
   }

   private void Start()
   {
      for (int i = 0; i < m_Pills.Length; i++)
         m_Pills[i].m_TimeToSpawn = Random.Range(m_Pills[i].m_MinSpawnTime, m_Pills[i].m_MaxSpawnTime);
   }

   private void Update()
   {
      if (m_IsOn)
      {
         for (int i = 0; i < m_Pills.Length; i++)
         {
            m_Pills[i].m_TimeToSpawn -= Time.deltaTime;

            if (m_Pills[i].m_TimeToSpawn <= 0)
            {
               Vector3 pos = new Vector3(
                  Random.Range(-m_SpawnArea.x / 2, m_SpawnArea.x / 2),
                  1.3f,
                  Random.Range(-m_SpawnArea.z / 2, m_SpawnArea.z / 2));

               Instantiate(m_Pills[i].m_Pill, pos, Quaternion.identity);

               m_Pills[i].m_TimeToSpawn = Random.Range(m_Pills[i].m_MinSpawnTime, m_Pills[i].m_MaxSpawnTime);
            }
         }
      }
   }

   private void OnEnable()
   {
      GameManager.StartRoundEvent += TurnSpawnerOn;
      GameManager.EndRoundEvent += TurnSpawnerOff;
   }

   private void OnDisable()
   {

      GameManager.StartRoundEvent -= TurnSpawnerOn;
      GameManager.EndRoundEvent -= TurnSpawnerOff;
   }


   private void TurnSpawnerOn()
   {
      m_IsOn = true;
   }

   private void TurnSpawnerOff()
   {
      m_IsOn = false;
   }


   [System.Serializable]
   private struct PillStats
   {
      public Pill m_Pill;

      [Tooltip("The minimum amount of time to spawn the next pill.")]
      public float m_MinSpawnTime;

      [Tooltip("The maximum amount of time to spawn the next pill.")]
      public float m_MaxSpawnTime;

      [HideInInspector]
      // The time left until the next pill should be spawned
      public float m_TimeToSpawn;

      public PillStats(Pill pill, float minSpawnTime, float maxSpawnTime)
      {
         m_Pill = pill;
         m_MinSpawnTime = minSpawnTime;
         m_MaxSpawnTime = maxSpawnTime;
         m_TimeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
      }
   }
}

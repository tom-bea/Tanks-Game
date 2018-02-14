using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TankChoiceMenu : MonoBehaviour
{
   public GameObject m_AllTanks;

   public static bool m_Picking;

   private static int m_NumberPicked;
   private int m_NumberPlayers;

   private static int[] m_PickedOptions;

   private void Awake()
   {
      m_NumberPicked = 0;
      m_NumberPlayers = 2;
      m_PickedOptions = new int[4];
      for (int i = 0; i < 4; i++)
         m_PickedOptions[i] = -1;
   }


   public void OnEnable()
   {
      UpdateText[] meshes = FindObjectsOfType<UpdateText>();
      foreach (UpdateText mesh in meshes)
         mesh.ResetSelf();
      Reset();
   }

   public static int CurrentPlayer
   {
      get { return m_NumberPicked; }
   }


   public void StartGame()
   {
      if (m_NumberPicked == m_NumberPlayers)
      {
         FindObjectOfType<DataKeeper>().SetTanks(m_PickedOptions);
         FindObjectOfType<LevelLoader>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
         gameObject.SetActive(false);
         m_AllTanks.SetActive(false);
      }
   }
   
   public void Option1()
   {
      if (m_NumberPicked < m_NumberPlayers && m_PickedOptions[0] == -1)
      {
         m_PickedOptions[0] = m_NumberPicked + 1;
         m_NumberPicked++;
         m_Picking = true;
      }
   }

   public void Option2()
   {
      if (m_NumberPicked < m_NumberPlayers && m_PickedOptions[1] == -1)
      {
         m_PickedOptions[1] = m_NumberPicked + 1;
         m_NumberPicked++;
         m_Picking = true;
      }
   }

   public void Option3()
   {
      if (m_NumberPicked < m_NumberPlayers && m_PickedOptions[2] == -1)
      {
         m_PickedOptions[2] = m_NumberPicked + 1;
         m_NumberPicked++;
         m_Picking = true;
      }
   }

   public void Option4()
   {
      if (m_NumberPicked < m_NumberPlayers && m_PickedOptions[3] == -1)
      {
         m_PickedOptions[3] = m_NumberPicked + 1;
         m_NumberPicked++;
         m_Picking = true;
      }
   }


   public static void Reset()
   {
      m_NumberPicked = 0;
      for (int i = 0; i < 4; i++)
         m_PickedOptions[i] = -1;
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataKeeper : MonoBehaviour
{
   private static DataKeeper m_Real = null;

   public int[] m_ChosenTanks;

   private void Start()
   {
      if (m_Real == null)
         m_Real = this;
      else if (m_Real != this)
         Destroy(gameObject);

      DontDestroyOnLoad(gameObject);
   }

   public void SetTanks(int[] pickedTanks)
   {
      m_ChosenTanks = pickedTanks;
   }
}

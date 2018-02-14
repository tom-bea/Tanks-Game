using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
   public void UpdateSelf()
   {
      if (TankChoiceMenu.m_Picking)
      {
         TankChoiceMenu.m_Picking = false;
         GetComponent<TextMeshProUGUI>().text = "PLAYER " + TankChoiceMenu.CurrentPlayer;
      }
   }

   public void ResetSelf()
   {
      GetComponent<TextMeshProUGUI>().text = "";
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankColorer : MonoBehaviour
{
   [SerializeField]
   private Color m_TankColor;

   private void Start()
   {
      MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

      for (int i = 0; i < renderers.Length; i++)
      {
         renderers[i].material.color = m_TankColor;
      }
   }
}

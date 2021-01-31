using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpawnGizmos : MonoBehaviour
{
   
    [SerializeField] private Color m_colorGizmos;

    private void OnDrawGizmosSelected() {
//        Color.magenta : Mantra // Color.green : Soul
//        Debug.Log("OnDrawGizmosSelected");
//        foreach (Transform child in transform.GetComponentsInChildren<Transform>()) 
//        {
//            if (child.GetComponentsInChildren<Transform>().Length == 1) {
//                Debug.Log("OnDrawGizmos");
//                Gizmos.color = m_colorGizmos;
//                Gizmos.DrawSphere(child.position, 0.15f);
//            }
//        }
    }

    private void OnDrawGizmos() 
    {
//        Color.magenta : Mantra // Color.green : Soul
        Debug.Log("OnDrawGizmos");
        foreach (Transform child in transform.GetComponentsInChildren<Transform>()) 
        {
            if (child.GetComponentsInChildren<Transform>().Length == 1) {
                Debug.Log("OnDrawGizmos");
                Gizmos.color = m_colorGizmos;
                Gizmos.DrawSphere(child.position, 0.15f);
            }
        }
    }
}

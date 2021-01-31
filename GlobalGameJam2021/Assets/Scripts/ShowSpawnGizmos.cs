using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpawnGizmos : MonoBehaviour {

    private List<Vector3> spawnPos = null;
   
    [SerializeField] private ColorGizmos m_colorGizmos;

    enum ColorGizmos {
        Mantra,
        Soul
    }

    private List<Vector3> AllSpawnPoint(String tag) 
    {
        List<Vector3> list = new List<Vector3>();
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag(tag)) {
            list.Add(fooObj.transform.position);
        }
        return list;
    }

    private void OnDrawGizmos() {
        if (m_colorGizmos == ColorGizmos.Mantra) {
            spawnPos = AllSpawnPoint("SpawnMantra");
        }
        else {
            spawnPos = AllSpawnPoint("SpawnSoul");
        }
        if (spawnPos != null) {
            foreach (Vector3 point in spawnPos) {
                if (m_colorGizmos == ColorGizmos.Mantra) {
                    Gizmos.color = Color.magenta;
                }
                else {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawSphere(point, 0.15f);
            }
        }
    }
}

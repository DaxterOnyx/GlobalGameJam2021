using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    
    [Header("Game Settings")]
    [SerializeField] private float m_initTimer;

    [Header("Game Component")]
    [SerializeField] private GameObject m_mantra;
    [SerializeField] private GameObject m_chest;
    private GameObject m_player;
    private GameObject m_place;
    private PlayerControl playerScript;
    
    private List<Vector3> soulSpawnPos = null;
    private List<Vector3> soulSpawnPosUsed = new List<Vector3>();
    
    private List<Vector3> mantraSpawnPos = null;
    private Vector3 currentSoulSpawnPos;
    private Vector3 currentMantraSpawnPos;

    private int mantraCounter = 0;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
        }

        m_place = GameObject.FindGameObjectsWithTag("Place")[0];
        m_player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerScript = m_player.GetComponent<PlayerControl>();
        
        soulSpawnPos = AllSpawnPoint("SpawnSoul");
        mantraSpawnPos = AllSpawnPoint("SpawnMantra");
        currentSoulSpawnPos = soulSpawnPos[0];
        currentMantraSpawnPos = mantraSpawnPos[0];
    }
    
    private void Start()
    {
        StartGame();
    }
    
    private void Update() 
    {
        
    }

    public void FoundMantra(GameObject mantra) {
        Destroy(mantra);
        playerScript.IntoSoul();
        
        //spawnSoul
        Vector3 bufferPos = m_player.transform.position;
        while (soulSpawnPosUsed.Contains(currentSoulSpawnPos)) {
            currentSoulSpawnPos = newRandomPos(soulSpawnPos, currentSoulSpawnPos);
        }
        soulSpawnPosUsed.Add(currentSoulSpawnPos);
        m_player.transform.position = currentSoulSpawnPos;

        StartCoroutine("SpawningChest", bufferPos);
    }

    private IEnumerator SpawningChest(Vector3 bufferPos) 
    {
        playerScript.FreezeMoving = true;
        yield return new WaitForSeconds(0.5f);
        Instantiate(m_chest,  bufferPos, Quaternion.identity);
        playerScript.FreezeMoving = false;
        SoulManager.Instance.WakeUpSouls();
    }

    public void FoundChest(GameObject chest) {
        mantraCounter++;
        Debug.Log("mantraCounter : " + mantraCounter);
        Destroy(chest);
        playerScript.IntoChest();
        InitNewRound();
        SoulManager.Instance.SleepSouls();
    }
    
    void InitNewRound() {
        //spawn mantra
        currentMantraSpawnPos = newRandomPos(mantraSpawnPos, currentMantraSpawnPos);
        Instantiate(m_mantra,  currentMantraSpawnPos, Quaternion.identity);
    }

    private void StartGame() {
        Debug.Log("StartGame");
        InitNewRound();
    }

    public void EndGame()
    {
        Debug.Log("EndGame");
    }

    private Vector3 newRandomPos(List<Vector3> listPos, Vector3 oldPos) {
        Vector3 toReturn = oldPos;
        while (toReturn == oldPos) {
            toReturn = listPos[Random.Range(0, soulSpawnPos.Count)];
        }
        return toReturn;
    }

    private List<Vector3> AllSpawnPoint(String tag) 
    {
        List<Vector3> list = new List<Vector3>();
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag(tag)) {
            list.Add(fooObj.transform.position);
        }
        return list;
    }
    
    public static GameManager Instance { get { return m_instance; } }
    public GameObject Player { get { return m_place; } }

    private void OnDrawGizmos() {
        mantraSpawnPos = AllSpawnPoint("SpawnSoul");
        if (mantraSpawnPos != null) {
            foreach (Vector3 point in mantraSpawnPos) {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(point, 0.15f);
            }
        }
        soulSpawnPos = AllSpawnPoint("SpawnMantra");
        if (soulSpawnPos != null) {
            foreach (Vector3 point in soulSpawnPos) {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(point, 0.15f);
            }
        }
    }
}

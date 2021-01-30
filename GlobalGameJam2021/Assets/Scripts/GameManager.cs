using System;
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
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_place;
    [SerializeField] private GameObject m_mantra;
    private PlayerControl playerScript;
    
    private List<Vector3> playerSpawnPos = null;
    private List<Vector3> mantraSpawnPos = null;
    private Vector3 currentPlayerSpawnPos;
    private Vector3 currentMantraSpawnPos;

    private int mantraCounter = 0;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }

        playerScript = m_place.GetComponent<PlayerControl>();
        playerSpawnPos = AllSpawnPoint("PlayerSpawn");
        mantraSpawnPos = AllSpawnPoint("MantraSpawn");
        currentPlayerSpawnPos = playerSpawnPos[0];
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
        mantraCounter++;
        InitNewRound();
    }

    public void InitNewRound() 
    {
        //spawnPlayer
        currentPlayerSpawnPos = newRandomPos(playerSpawnPos, currentPlayerSpawnPos);
        m_player.transform.position = currentPlayerSpawnPos;
        
        //spawn mantra
        currentMantraSpawnPos = newRandomPos(mantraSpawnPos, currentMantraSpawnPos);
        Instantiate(m_mantra,  currentMantraSpawnPos, Quaternion.identity);
    }
    
    private void StartGame() 
    {
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
            toReturn = listPos[Random.Range(0, playerSpawnPos.Count)];
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
        playerSpawnPos = AllSpawnPoint("SpawnMantra");
        if (playerSpawnPos != null) {
            foreach (Vector3 point in playerSpawnPos) {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(point, 0.15f);
            }
        }
    }
}

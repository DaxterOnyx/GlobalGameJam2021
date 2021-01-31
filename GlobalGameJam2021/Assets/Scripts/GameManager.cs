using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    
    [Header("Game Settings")]
    [SerializeField] private int m_initTimerToFoundChest = 120;
    [SerializeField] private float m_initNbRoundToWin = 5;
    private bool secondChanceUsed = false;

    [Header("Game Component")]
    [SerializeField] private GameObject m_mantra;
    [SerializeField] private GameObject m_chest;
    [SerializeField] private GameObject m_player;
    private PlayerControl playerScript;
    
    private List<Vector3> soulSpawnPos = null;
    private List<Vector3> soulSpawnPosUsed = new List<Vector3>();
    
    private List<Vector3> mantraSpawnPos = null;
    private Vector3 currentSoulSpawnPos;
    private Vector3 currentMantraSpawnPos;

    private int mantraCounter = 0;

    private Coroutine timer;
    
    void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
        }

        playerScript = m_player.GetComponent<PlayerControl>();
        
        soulSpawnPos = AllSpawnPoint("SpawnSoul");
        currentSoulSpawnPos = soulSpawnPos[0];
        mantraSpawnPos = AllSpawnPoint("SpawnMantra");
        currentMantraSpawnPos = mantraSpawnPos[0];
        
        if (m_initNbRoundToWin > mantraSpawnPos.Count) {
            m_initNbRoundToWin = mantraSpawnPos.Count;
        }
    }
    
    void Start()
    {
        Debug.Log("StartGame");
        InitNewRound();
    }
    
    void Update() 
    {
        if (mantraCounter >= m_initNbRoundToWin) {
            EndGameWin();
        }
    }
    
    
    public void InitNewRound() {
        //spawn mantra
        currentMantraSpawnPos = newRandomPos(mantraSpawnPos, currentMantraSpawnPos);
        Instantiate(m_mantra,  currentMantraSpawnPos, Quaternion.identity);
    }

    public void EndGameLose()
    {
        Debug.Log("EndGame Lose");
    }
    
    public void EndGameWin()
    {
        Debug.Log("EndGame Win");
    }
    
    public void FoundMantra(GameObject mantra) 
    {
        Destroy(mantra);
        
        // select new spawn
        Vector3 bufferPos = m_player.transform.position;
        while (soulSpawnPosUsed.Contains(currentSoulSpawnPos)) {
            currentSoulSpawnPos = newRandomPos(soulSpawnPos, currentSoulSpawnPos);
        }

        soulSpawnPosUsed.Add(currentSoulSpawnPos);
        
        // spawn chest
        var chest = Instantiate(m_chest,  bufferPos, Quaternion.identity);
        var chestControl = chest.GetComponent<ChestControl>();

        chestControl.OnSoulAnimEnded.AddListener(() =>
        {
            m_player.transform.position = currentSoulSpawnPos;
            SoulManager.Instance.WakeUpSouls();
            timer = StartCoroutine(CountdownTimer());
        });
        
        // make animations
        chestControl.ToSoul(m_player);

    }
    
    public void FoundChest(GameObject chest) 
    {
        mantraCounter++;
        Debug.Log("mantraCounter : " + mantraCounter);
        
        var chestControl = chest.GetComponent<ChestControl>();
        
        chestControl.OnOniAnimEnded.AddListener(() =>
        {
            InitNewRound();
            SoulManager.Instance.SleepSouls();
            StopCoroutine(timer);
            secondChanceUsed = true;
        });
        
        chestControl.ToOni(m_player);
    }

    IEnumerator CountdownTimer() {
        int counter = m_initTimerToFoundChest;
        UITimer.Instance.ShowTime();
        
        while (counter > 0 && !secondChanceUsed) 
        {
            Debug.Log("Timer : " + counter);
            UITimer.Instance.SetTime((int) Mathf.Floor(counter / 60f), counter%60);
            yield return new WaitForSeconds(1);
            counter--;
        }
        
        if (secondChanceUsed) secondChanceUsed = false;
        else EndGameLose();
        
        UITimer.Instance.HideTime();
    }

    private Vector3 newRandomPos(List<Vector3> listPos, Vector3 oldPos) {
        Vector3 toReturn = oldPos;
        while (toReturn == oldPos) {
            toReturn = listPos[Random.Range(0, listPos.Count)];
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

}

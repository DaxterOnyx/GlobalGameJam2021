using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    
    [Header("Game Settings")]
    [SerializeField] private int m_initTimerToFoundChest = 120;
    [SerializeField] private float m_initNbRoundToWin = 5;

    [Header("Game Component")]
    [SerializeField] private GameObject m_mantra;
    [SerializeField] private GameObject m_chest;
    [SerializeField] private GameObject m_player;
    
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
        SoundManager.Instance.PlayMusic(SoundManager.Instance.maigoni);
    }
    
    void Update() 
    {
        if (mantraCounter >= m_initNbRoundToWin) {
            EndGameWin();
        }
    }
    
    public void InitNewRound() {
        // spawn mantra
        currentMantraSpawnPos = newRandomPos(mantraSpawnPos, currentMantraSpawnPos);
        var mantra = Instantiate(m_mantra,  currentMantraSpawnPos, Quaternion.identity);

        // set objective
        m_player.GetComponent<PlayerObjective>().Objective = mantra.transform;
    }

    public void EndGameLose()
    {
        Debug.Log("EndGame Lose");
        SceneManager.LoadScene(0);
    }
    
    public void EndGameWin()
    {
        Debug.Log("EndGame Win");
        SceneManager.LoadScene(0);
    }
    
    public void FoundMantra(GameObject mantra) 
    {
        Destroy(mantra);
        
        SoundManager.Instance.PlayEffect(SoundManager.Instance.trigger2);
        
        // select new spawn
        Vector3 bufferPos = m_player.transform.position;
        while (soulSpawnPosUsed.Contains(currentSoulSpawnPos)) {
            currentSoulSpawnPos = newRandomPos(soulSpawnPos, currentSoulSpawnPos);
        }

        soulSpawnPosUsed.Add(currentSoulSpawnPos);
        
        // spawn chest
        var chest = Instantiate(m_chest,  bufferPos, Quaternion.identity);
        var chestControl = chest.GetComponent<ChestControl>();

        // set objective
        m_player.GetComponent<PlayerObjective>().Objective = chest.transform;
        
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
            UIGame.Instance.HideTime();
        });
        
        chestControl.ToOni(m_player);
    }

    IEnumerator CountdownTimer() 
    {
        int counter = m_initTimerToFoundChest;
        UIGame.Instance.ShowTime();
        
        while (counter > 0)
        {
            UIGame.Instance.SetTime(counter / 60, counter%60);
            yield return new WaitForSeconds(1);
            counter--;
        }
        
        EndGameLose();
        UIGame.Instance.HideTime();
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

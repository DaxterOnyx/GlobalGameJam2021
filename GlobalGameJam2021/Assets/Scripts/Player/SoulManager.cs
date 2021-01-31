using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Player;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    private static SoulManager instance;

    public static SoulManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoulManager>();
            if (instance == null)
                Debug.LogError("Do not have SoulManager in scene");
            return instance;
        }
    }

    [SerializeField] private PlayerRecord Player;
    [SerializeField] private GameObject SoulPrefab;

    private int nbSoul = 0;
    private List<Soul> souls = new List<Soul>();
    private List<PlayerTrace> traces = new List<PlayerTrace>();

    private void Start()
    {
        if (Player == null) Debug.LogError("Player in SoulManager is not referenced");
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            WakeUpSouls();

        if (Input.GetKeyDown(KeyCode.Backspace))
            SleepSouls();
#endif
    }

    /// <summary>
    /// Wake up all souls, if it is the first time wake up no soul
    /// Start record of player movement
    /// </summary>
    /// <returns>nb of souls</returns>
    public int WakeUpSouls()
    {
        //Create trace for new player movement
        var playerTrace = new PlayerTrace();
        playerTrace.Index = nbSoul;
        traces.Add(playerTrace);
        Player.StartRecord(playerTrace);

        //Waek up each soul with her trace
        for (var index = 0; index < nbSoul; index++)
        {
            var soul = souls[index];
            var trace = traces[index];

            soul.StartMove(trace);
        }

        return nbSoul;
    }

    /// <summary>
    /// Sleep all the souls, and stop record fo player movement
    /// </summary>
    /// <returns>nb of souls</returns>
    public int SleepSouls()
    {
        //stop record of movement of player
        Player.StopRecord();

        //Create a new soul for this new record
        var playerCopy = Instantiate(SoulPrefab).GetComponent<Soul>();
        souls.Add(playerCopy);
        playerCopy.gameObject.SetActive(false);

        //Sleep souls
        for (var index = 0; index < nbSoul; index++)
        {
            var soul = souls[index];

            soul.StopMove();
        }

        UIGame.Instance.SetSoulCounter(++nbSoul);
        //count the new soul
        return nbSoul;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HealthNamespace;

public class GameHandler : MonoBehaviour
{
    public GameHandler Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI timerUI;
    private float elapsedTime = 0f; //The time game running in second 
    private bool isGameRunning;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than 1 game handler!!!");
        }

        Instance = this;
    }
    private void Start()
    {
        Debug.Log("Game start!!!");
        isGameRunning = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;

            //split time to the hh:mm:ss format
            float minute = Mathf.FloorToInt(elapsedTime / 60);
            float second = Mathf.FloorToInt(elapsedTime % 60);

            timerUI.text = string.Format("{0:00}:{1:00}", minute, second);
        }
    }

    public float GetElapsedTime() => elapsedTime;
    
}

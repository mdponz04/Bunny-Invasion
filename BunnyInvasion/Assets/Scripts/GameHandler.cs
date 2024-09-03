using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerUI;
    private float elapsedTime = 0f; //The time game running in second 
    private bool isGameRunning;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game start!!!");
        isGameRunning = true;
    }

    // Update is called once per frame
    void Update()
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
}

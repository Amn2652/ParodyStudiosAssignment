using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverMessage;
    public TMP_Text cubesCounts;

    [Header("Game Settings")]
    public float gameDuration = 120f;
    public Transform player;
    public LayerMask groundLayer;

    private float remainingTime;
    private bool isGameOver = false;
    private float fallTime = 0f;
    private int totalCubes = 5;
    private int collectedCubes = 0;

    void Start()
    {
        cubesCounts.text = "Cubes: " + Mathf.FloorToInt(collectedCubes).ToString() + "/5";
        remainingTime = gameDuration;
        gameOverPanel.SetActive(false);
        StartCoroutine(GameTimer());
    }

    void Update()
    {
        if (isGameOver) return;

        CheckPlayerFall();
        CheckCubeCollection();
    }

    IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
            yield return null;
        }
        EndGame("Time's Up!");
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = minutes + ":" + seconds.ToString("00");
    }

    void CheckPlayerFall()
    {
        if (!Physics.Raycast(player.position, Vector3.down, 1f, groundLayer))
        {
            fallTime += Time.deltaTime;
            if (fallTime >= 5f)
            {
                EndGame("You fell off!");
            }
        }
        else
        {
            fallTime = 0f; 
        }
    }

    void CheckCubeCollection()
    {
        if (collectedCubes >= totalCubes)
        {
            EndGame("You collected all cubes!");
        }
    }

    public void CollectCube()
    {
        collectedCubes++;
        cubesCounts.text = "Cubes: " + Mathf.FloorToInt(collectedCubes).ToString() + "/5";
    }

    void EndGame(string message)
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverMessage.text = message;
        StopAllCoroutines();
    }
}

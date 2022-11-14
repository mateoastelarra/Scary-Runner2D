using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Configuration;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float initialScrollSpeed;

    public int score;
    private float timer;
    private float meters;
    private float scrollSpeed;
    public string input;
    

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }      
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateSpeed();
    }

    public void ShowGameOverScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void RestartScene()
    {
        //SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    private void UpdateScore()
    {
        timer += Time.deltaTime;
        meters += scrollSpeed * Time.deltaTime;
        score = (int)(meters);
        scoreText.text = string.Format("{0:00000}", score);
    }

    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }

    public void ReadScreenInput(string s)
    {
        input = s;
        Debug.Log(input);
    }

    private void UpdateSpeed()
    {
        float speedDivider = 7f;
        scrollSpeed = initialScrollSpeed + timer / speedDivider;
    }
}

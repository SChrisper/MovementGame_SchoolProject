using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TextMeshProUGUI timeCounter;
    private TimeSpan timePlaying;
    private bool timerGoing;
    private bool hasStarted;

    private float elapsedTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (!hasStarted &&
            (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)))
        {
            BeginTimer();
            hasStarted = true;
        }
        SaveTime();

        if (timerGoing)
        {
            Debug.Log("Elapsed Time: " + elapsedTime);
        }
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }

public void SaveTime()
{
    if (!timerGoing)
    {
        Debug.LogError("Cannot save time, timer is not running!");
        return;
    }

    string formattedTime = timePlaying.ToString("mm':'ss'.'ff");
    string numericTime = formattedTime.Replace(":", "").Replace(".", "");

    Debug.Log("Saving Time: " + numericTime);

    PlayerPrefs.SetString("FinalTime", numericTime);

    PlayerPrefs.Save();

    string savedTime = PlayerPrefs.GetString("FinalTime");
    Debug.Log("Saved Time: " + savedTime);

}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EndTimer();
        Debug.Log("Scene Loaded. Timer stopped.");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
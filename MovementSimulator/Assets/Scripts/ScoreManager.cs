
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Reference")]
    private Timer ti;

    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEngine.Events.UnityEvent<string, int> submitScoreEvent;

    private void Start()
    {
        ti = Timer.instance;


        string savedTime = PlayerPrefs.GetString("FinalTime", "000000");


        Debug.Log("Retrieved Saved Time: " + savedTime);


        inputScore.text = savedTime;
    }

    public void SubmitScore()
    {

        int score = 0;


        string time = inputScore.text;

        if (time.Length == 6)
        {
            int minutes = int.Parse(time.Substring(0, 2));
            int seconds = int.Parse(time.Substring(2, 2));
            int milliseconds = int.Parse(time.Substring(4, 2));

            score = minutes * 60 + seconds;
        }

        submitScoreEvent.Invoke(inputName.text, score);
    }
}
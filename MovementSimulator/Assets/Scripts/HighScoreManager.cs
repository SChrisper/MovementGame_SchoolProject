using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private Timer timer; // Reference to Timer instance
    private string connectionString;

    public Transform leaderboardContent; // Parent Transform for leaderboard rows
    public TMPro.TextMeshProUGUI[] playerNameFields; // Array of player name fields assigned in the inspector
    public TMPro.TextMeshProUGUI[] playerScoreFields; // Array of player score fields assigned in the inspector

    public TMP_InputField playerNameInputField; // InputField for entering the player's name
    private int currentScore; // Dynamically assigned later in Start()

    void Start()
    {
        connectionString = "URI=file:" + Application.persistentDataPath + "/HighScore.db";
        CreateHighScoreTable(); // Ensure database table exists

        // Set up Timer reference and initialize currentScore
        if (Timer.instance != null)
        {
            timer = Timer.instance;
            currentScore = int.Parse(timer.GetFormattedTime().Replace(":", "").Replace(".", ""));
        }
        else
        {
            Debug.LogError("Timer instance is missing. Ensure Timer script is in a persistent state.");
        }

        DisplayHighScores();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Database path: " + connectionString);
    }

    // Ensure the database table is created
    void CreateHighScoreTable()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS HighScore (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL UNIQUE,
                        Score INTEGER NOT NULL
                    );";
                command.ExecuteNonQuery();
            }
        }
    }

    // Submit a player's name and score
    public void SubmitScore()
    {
        string playerName = playerNameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty. Please enter a valid name.");
            return;
        }

        if (currentScore == 0)
        {
            Debug.LogWarning("Score is 0. Score will not be submitted.");
            return; // Do not submit a score of 0
        }

        UpdateOrInsertScore(playerName, currentScore);
    }

    // Always overwrite the score for the name (even if it's lower or higher)
    void UpdateOrInsertScore(string playerName, int score)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var commandUpsert = connection.CreateCommand())
            {
                // Insert or replace the name & score directly
                commandUpsert.CommandText = "REPLACE INTO HighScore (Name, Score) VALUES (@Name, @Score);";
                commandUpsert.Parameters.Add(new SqliteParameter("@Name", playerName));
                commandUpsert.Parameters.Add(new SqliteParameter("@Score", score));
                commandUpsert.ExecuteNonQuery();

                Debug.Log($"Score set for {playerName} with score {score}");
            }
        }

        DisplayHighScores();
    }

    // Fetch and display the top 10 high scores in ascending order (lowest score = #1) and exclude scores = 0
    void DisplayHighScores()
    {
        List<(string, int)> highScores = FetchHighScores();
        int maxRows = Mathf.Min(playerNameFields.Length, highScores.Count);

        // Populate the leaderboard arrays with fetched database values
        for (int i = 0; i < maxRows; i++)
        {
            playerNameFields[i].text = highScores[i].Item1;
            playerScoreFields[i].text = highScores[i].Item2.ToString();
        }

        // Clear any remaining unused rows if the database has less than 10 scores
        for (int i = maxRows; i < playerNameFields.Length; i++)
        {
            playerNameFields[i].text = "";
            playerScoreFields[i].text = "";
        }
    }

    // Fetch top 10 high scores from database excluding scores = 0
    List<(string, int)> FetchHighScores()
    {
        List<(string, int)> highScores = new List<(string, int)>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // Fetch only scores greater than 0 and sort from lowest to highest
                command.CommandText = "SELECT Name, Score FROM HighScore WHERE Score > 0 ORDER BY Score ASC LIMIT 10;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highScores.Add((reader["Name"].ToString(), int.Parse(reader["Score"].ToString())));
                    }
                }
            }
        }

        return highScores;
    }
}

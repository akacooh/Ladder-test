using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTable
{
    public int score { get; private set; }

    private List<ScoreRecord> scoreTable;
    private string currentPlayerName;
    private string savePath = Application.persistentDataPath + "/scores.txt";

    public ScoreTable() {
        score = 0;
        currentPlayerName = string.Empty;
        LoadScore();
    }

    private void LoadScore() {
        scoreTable = new List<ScoreRecord>();
        if (File.Exists(savePath)) {
            string[] jsons = File.ReadAllLines(savePath);
            foreach (var json in jsons) {
                ScoreRecord record = JsonUtility.FromJson<ScoreRecord>(json);
                scoreTable.Add(record);
            }
        } else {
            for (int i = 0; i < 3; i++) {
                scoreTable.Add(new ScoreRecord("", 0));
            }
        }
        scoreTable.Sort((a, b) => b.score.CompareTo(a.score));
    }

    private void SaveScore() {
        string[] jsons = new string[scoreTable.Count];
        for (int i = 0; i < scoreTable.Count; i++) {
            jsons[i] = JsonUtility.ToJson(scoreTable[i]);
        }
        File.WriteAllLines(savePath, jsons);
    }

    public void ScoreUpdate(int value) {
        score += value;
        int index = scoreTable.FindIndex(x => x.name == currentPlayerName);
        if (index != -1) {
            scoreTable[index].score = score;
            scoreTable.Sort((a, b) => b.score.CompareTo(a.score));
        } else {
            scoreTable.Add(new ScoreRecord(currentPlayerName, score));
            scoreTable.Sort((a, b) => b.score.CompareTo(a.score));
            scoreTable.RemoveAt(scoreTable.Count - 1);
        }
        SaveScore();
    }

    public (string name , int score) GetScoreAtIndex(int index) {
        if (index > scoreTable.Count - 1) {
            return ("", -1);
        }
        string name = scoreTable[index].name;
        int score = scoreTable[index].score;
        return (name, score);
    }

    public int GetRowsCount() {
        return scoreTable.Count;
    }

    public void SetNewPlayer(string name) {
        currentPlayerName = name;
    }

    [Serializable]
    private class ScoreRecord {
        public string name;
        public int score;

        public ScoreRecord(string name, int score) {
            this.name = name;
            this.score = score;
        }
    }
}

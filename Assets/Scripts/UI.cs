using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputField playerNameInput;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject background;
    [SerializeField] private GameStates game;
    [SerializeField] private Text scoreText;
    [SerializeField] private List<GameObject> topScoreCells;

    private ScoreTable scoreTable;

    void Start()
    {
        scoreTable = new ScoreTable();
        scoreTable.SetNewPlayer(playerNameInput.text);
        UpdateScoreUI();
        SetupEvents();
    }

    private void SetupEvents() {
        player.PlayerJumpedForward += ScoreUpdate;
        player.PlayerDied += OnPlayerDeath;

        playButton.onClick.AddListener(PlayPressed);
        pauseButton.onClick.AddListener(PausePressed);
        playerNameInput.onEndEdit.AddListener(ChangePlayerName);
    }

    private void UpdateScoreUI() {
        int max = Mathf.Min(scoreTable.GetRowsCount(), topScoreCells.Count);
        for (int i = 0; i < max; i++) {
            (string name, int score) = scoreTable.GetScoreAtIndex(i);
            topScoreCells[i].transform.GetChild(1).GetComponent<Text>().text = name;
            topScoreCells[i].transform.GetChild(2).GetComponent<Text>().text = score.ToString();
        }

    }

    private void OnDestroy() {
        if (player != null) {
            player.PlayerJumpedForward -= ScoreUpdate;
            player.PlayerDied -= OnPlayerDeath;
        }
    }
    private void ScoreUpdate() {
        scoreTable.ScoreUpdate(1);
        scoreText.text = scoreTable.score.ToString();
    }

    public void PausePressed() {
        UpdateScoreUI();
        game.ChangeState(GameState.Pause);
        ToogleInPlayUI(false);
    }

    public void PlayPressed() {
        game.ChangeState(GameState.Play);
        ToogleInPlayUI(true);
    }

    private void ChangePlayerName(string name) {
        scoreTable.SetNewPlayer(name);
    }
    private void OnPlayerDeath() {
        UpdateScoreUI();
        ToogleInPlayUI(false);
        game.ChangeState(GameState.GameOver);
    }
    private void ToogleInPlayUI(bool active) {
        pauseButton.gameObject.SetActive(active);
        scoreText.gameObject.SetActive(active);
        background.SetActive(!active);
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            PausePressed();
        }
    }

}

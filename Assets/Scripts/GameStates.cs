using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    public GameState currentState { get; private set; }

    void Start()
    {
        currentState = GameState.Start;
        Time.timeScale = 0;
    }

    public void ChangeState(GameState newState) {
        if (newState == GameState.Play) {
            if (currentState == GameState.Pause || currentState == GameState.Start) {
                Play();
            }
        }
        if (newState == GameState.Pause) {
            Pause();
        }
        if (newState == GameState.GameOver) {
            GameOver();
        }
    }
    private void Play() {
        currentState = GameState.Play;
        Time.timeScale = 1;
    }

    private void Pause() {
        currentState = GameState.Pause;
        Time.timeScale = 0;
    }

    private void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
public enum GameState {
    Start,
    Play,
    Pause,
    GameOver
}
using System;
using UnityEngine;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.GamePaused:
                HandleGamePause();
                break;
            case GameState.GamePlay:
                HandleGamePlay();
                break;
            case GameState.Lose:
                HandleLosing();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting()
    {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        // Ex: ChangeState(GameState.GamePaused);
    }

    private void HandleGamePause()
    {

    }

    private void HandleGamePlay()
    {

    }

    private void HandleLosing()
    {

    }
    
}

/// <summary>
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState {
    Starting = 0,
    GamePaused = 1,
    GamePlay = 2,
    Lose = 3,
}
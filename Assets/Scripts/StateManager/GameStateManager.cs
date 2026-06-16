using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    [SerializeField] GameState currentState = GameState.NotActive;
    GameState previousState = GameState.Failed;

    public event Action<bool> CanPlayerMove;
    public event Action<bool> canEnemyMove;

    [SerializeField] GameObject playerObject;
    [SerializeField] List<GameObject> enemyObjects = new();
    [SerializeField] Camera cam;


    bool playerCanMove = false;
    bool enemiesCanMove = false;

    public enum GameState
    {
        NotActive,
        Playing,
        Cutscene,
        Completed,
        Failed
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        CheckChangeInGameState(currentState);
    }

    private void Update()
    {
        CheckChangeInGameState(currentState);
    }
    // Update is called once per frame


    void CheckChangeInGameState(GameState newState)
    {
        if (previousState != newState)
        {
            previousState = currentState;
            currentState = newState;
            EnterNewState(newState);
            CanPlayerMove?.Invoke(playerCanMove);
            canEnemyMove?.Invoke(enemiesCanMove);
        }
    }

    void SetEntitiesMovementTo(bool status)
    {
        playerCanMove = status;
        enemiesCanMove = status;
    }

    IEnumerator CutSceneDelayBeforeMovement(float amountOfTime)
    {
        SetEntitiesMovementTo(false);
        yield return new WaitForSeconds(amountOfTime);
        SetEntitiesMovementTo(true);
        CheckChangeInGameState(GameState.Playing);
    }

    void StartCutScene(float time)
    {
        StartCoroutine(CutSceneDelayBeforeMovement(time));
    }

    public GameObject TellPlayerObject() => playerObject;

    void EnterNewState(GameState state)
    {
        switch (state)
        {
            case GameState.NotActive:
                SetEntitiesMovementTo(false);
                break;
            case GameState.Playing:
                SetEntitiesMovementTo(true);
                cam.GetComponent<CameraInterface>().FollowPlayer(playerObject);
                break;
            case GameState.Cutscene:
                StartCutScene(3);
                break;
            case GameState.Completed:
                break;
            case GameState.Failed:
                break;
        }
    }
}

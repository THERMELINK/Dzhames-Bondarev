using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    //singleton
    public static GameStateManager instance;
    [SerializeField] GameState currentState = GameState.NotActive;
    GameState previousState = GameState.Failed;

    public event Action<bool> CanPlayerMove;
    public event Action<bool> canEnemyMove;

    [SerializeField] GameObject playerObject;
    [SerializeField] List<GameObject> enemyObjects = new();
    [SerializeField] Camera cam;

    [SerializeField] GameObject winScreenUI;
    [SerializeField] GameObject loseScreenUI;

    bool playerCanMove = false;
    bool enemiesCanMove = false;


    //the possible states that the game can be in
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
        //creates a singleton from the state manager so every script can see easily what state it should be in
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


    /// <summary>
    /// this method sets all the movement bools from entities to a value
    /// </summary>
    void SetEntitiesMovementTo(bool status)
    {
        playerCanMove = status;
        enemiesCanMove = status;
    }

    /// <summary>
    /// creates a small delay where the player can not move, after delay sets all entities movement to active
    /// </summary>
    IEnumerator CutSceneDelayBeforeMovement(float amountOfTime)
    {
        SetEntitiesMovementTo(false);
        yield return new WaitForSeconds(amountOfTime);
        SetEntitiesMovementTo(true);
        CheckChangeInGameState(GameState.Playing);
    }

    /// <summary>
    /// this method runs when a cutscene is activated, this method sets the delay until movement 
    /// </summary>
    void StartCutScene(float time)
    {
        StartCoroutine(CutSceneDelayBeforeMovement(time));
    }

    void ShowUIElement(GameObject UIelement)
    {
        UIelement.SetActive(true);
    }

    public void FailPlayer()
    {
        EnterNewState(GameState.Failed);
    }

    public void WinPlayer()
    {
        EnterNewState(GameState.Completed);
    }

    /// <summary>
    /// this method gives the player object to scripts that need it (for example a newly instantiated enemy who is clueless)
    /// </summary>
    public GameObject TellPlayerObject() => playerObject;

    /// <summary>
    /// this method manages a new state and changes the behavior that the entities can do
    /// </summary>
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
                SetEntitiesMovementTo(false);
                ShowUIElement(winScreenUI);
                break;
            case GameState.Failed:
                SetEntitiesMovementTo(false);
                ShowUIElement (loseScreenUI);
                break;
        }
    }
}

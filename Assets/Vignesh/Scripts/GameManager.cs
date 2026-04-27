using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    //reference for the pickup target
    public Transform target;
    //reference for the player
    public Transform player;
    //reference for the enemy
    public Transform enemy;
    //reference for the enemy ai audio
    public GameObject enemyAIAudio;

    //Reference to the scripts for controlling the enemy movement and timer
    [Header("Scripts")]
    public EnemyMovement enemyMovement;
    public UIGameTimer uiGameTimer;

    //Spawn points for the pickup and enemy
    [Header("Spawn Points")]
    public Transform[] targetSpawnPos;
    public Transform[] enemySpawnPos;

    //used in ensuring minimum distance from the player
    [Header("Min Distance")]
    public float minTargetDistance = 30f;
    public float minEnemyDistance = 20f; 

    //Buttons used to pause and restart the game
    [Header("End UI")]
    public GameObject pauseButton;
    public GameObject returnToStartMenuButton;

    [Header("UI Sound")]
    // Has the UI sound effect
    public AudioSource uiClick;
    //Delay to ensure that the ui sound is played
    public float menuDelay = 0.2f;

    //for if the game is paused
    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        //Game starts at a regular speed
        Time.timeScale = 1f;
        //Return button is not shown when game begins
        if(returnToStartMenuButton != null){
            returnToStartMenuButton.SetActive(false);
        }
    }

    //Used when starting new round
    public void RoundStart(){
        //Game starts at a regular speed
        Time.timeScale = 1f;
        //Pause is set to its initial state of not paused
        isPaused = false;

        //random spawn for pickup and enemy
        RandomizeEnemyAndTarget();

        //resets enemy speed
        if(enemyMovement != null){
            enemyMovement.ResetSpeed();
        }
        
        //resets timer and starts it again
        if(uiGameTimer != null){
            uiGameTimer.ResetTimer();
            uiGameTimer.StartTimer();
        }

        //Return button is not shown during game
        if(returnToStartMenuButton != null){
            returnToStartMenuButton.SetActive(false);
        }

        // shows the pause button during game
        if(pauseButton != null){
            pauseButton.SetActive(true);
        }

    }

    //Used to randomize spawn for pickup and enemy
    void RandomizeEnemyAndTarget(){
        //Makes sure that player, target, and enemy are available
        if(target == null || player == null || enemy == null){
            return;
        }

        //used for getting a pickup spawn point that is a far enough
        // distance from the player
        Transform targetSpawn = GetRandomSpawn(targetSpawnPos, player.position, minTargetDistance);

        //Goes to the new spawn point
        if(targetSpawn != null){
            target.position = targetSpawn.position;
        }

        //used for getting an enemy spawn point that is a far enough
        // distance from the player
        Transform enemySpawn = GetRandomSpawn(enemySpawnPos, player.position, minEnemyDistance);

        //Goes to the new spawn point
        if(enemySpawn != null){
            MoveEnemy(enemySpawn.position);
        }
    }

    Transform GetRandomSpawn(Transform[] spawn, Vector3 avoidPos, float minDist){
        //returns if the spawn points are not available
        if(spawn == null || spawn.Length == 0){
            return null;
        }

        //used for trying to get spawn points far away from player
        for(int i = 0; i < 52; i++){
            //gets a random spawn point
            Transform potentialSpawn = spawn[Random.Range(0, spawn.Length)];

            //distance from player and spawn point
            float dist = Vector3.Distance(potentialSpawn.position, avoidPos);

            //Returns a valid spawn point if the distance is correct
            if(dist >= minDist){
                return potentialSpawn;
            }
        }

        //Returns a random spawn point if it didnt work
        return spawn[Random.Range(0, spawn.Length)];
    }

    //used to move the enemy ai
    void MoveEnemy(Vector3 enemyPos){
        if(enemy == null){
            return;
        }

        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        // If the NavMeshAgent is active, use warp
        if(agent != null && agent.enabled && enemy.gameObject.activeInHierarchy){
            agent.Warp(enemyPos);
        }
        //will be used to directly move enemy to spawn position
        else{
            enemy.position = enemyPos;
        }
    }

    //changes from pausing to resuming game
    public void ChangePauseStatus(){
        if(isPaused){
            ResumeGame();
        }
        else{
            PauseGame();
        }
    }

    //used to pause the game
    public void PauseGame(){
        isPaused = true;
        Time.timeScale = 0f;
    }

    //used to resume the game
    public void ResumeGame(){
        isPaused = false;
        Time.timeScale = 1f;
    }

    //used to help end the game round
    public void RoundEnd(){
        Time.timeScale = 1f;
        //stops the timer
        if(uiGameTimer != null){
            uiGameTimer.StopTimer();
        }

        //stops the enemy ai audio
        if(enemyAIAudio != null){
            enemyAIAudio.SetActive(false);
        }

        //used to hide the pause button
        if(pauseButton != null){
            pauseButton.SetActive(false);
        }

        //displays the return to menu button
        if(returnToStartMenuButton != null){
            returnToStartMenuButton.SetActive(true);
        }
    }

    //used to return to the start menu
    public void ReturnToStartMenu(){
        // ensures the UI sound click is played
        StartCoroutine(ReturnToStartMenuWithWait());
    }

    //briefly will wait and then load the start scene
    IEnumerator ReturnToStartMenuWithWait(){
        Time.timeScale = 1f;

        //plays the UI sound effect
        if(uiClick != null){
            uiClick.Play();
        }

        //used to wait
        //helps ensure the UI sound effect is played and not skipped
        // when loading the scene
        yield return new WaitForSecondsRealtime(menuDelay);

        //Used to load the start scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

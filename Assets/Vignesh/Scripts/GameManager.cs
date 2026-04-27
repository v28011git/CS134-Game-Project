using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    public Transform target;
    public Transform player;
    public Transform enemy;
    public GameObject enemyAIAudio;

    [Header("Scripts")]
    public EnemyMovement enemyMovement;
    public UIGameTimer uiGameTimer;

    [Header("Spawn Points")]
    public Transform[] targetSpawnPos;
    public Transform[] enemySpawnPos;

    [Header("Min Distance")]
    public float minTargetDistance = 30f;
    public float minEnemyDistance = 20f; 

    [Header("End UI")]
    public GameObject pauseButton;
    public GameObject returnToStartMenuButton;

    [Header("UI Sound")]
    public AudioSource uiClick;
    public float menuDelay = 0.2f;

    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        if(returnToStartMenuButton != null){
            returnToStartMenuButton.SetActive(false);
        }
    }

    public void RoundStart(){
        Time.timeScale = 1f;
        isPaused = false;

        RandomizeEnemyAndTarget();

        if(enemyMovement != null){
            enemyMovement.ResetSpeed();
        }
        
        if(uiGameTimer != null){
            uiGameTimer.ResetTimer();
            uiGameTimer.StartTimer();
        }

        if(returnToStartMenuButton != null){
            returnToStartMenuButton.SetActive(false);
        }

        if(pauseButton != null){
            pauseButton.SetActive(true);
        }

    }

    void RandomizeEnemyAndTarget(){
        if(target == null || player == null || enemy == null){
            return;
        }

        Transform targetSpawn = GetRandomSpawn(targetSpawnPos, player.position, minTargetDistance);

        if(targetSpawn != null){
            target.position = targetSpawn.position;
        }

        Transform enemySpawn = GetRandomSpawn(enemySpawnPos, player.position, minEnemyDistance);

        if(enemySpawn != null){
            MoveEnemy(enemySpawn.position);
        }
    }

    Transform GetRandomSpawn(Transform[] spawn, Vector3 avoidPos, float minDist){
        if(spawn == null || spawn.Length == 0){
            return null;
        }

        for(int i = 0; i < 52; i++){
            Transform potentialSpawn = spawn[Random.Range(0, spawn.Length)];

            float dist = Vector3.Distance(potentialSpawn.position, avoidPos);

            if(dist >= minDist){
                return potentialSpawn;
            }
        }
        return spawn[Random.Range(0, spawn.Length)];
    }

    void MoveEnemy(Vector3 enemyPos){
        if(enemy == null){
            return;
        }

        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        if(agent != null && agent.enabled && enemy.gameObject.activeInHierarchy){
            agent.Warp(enemyPos);
        }
        else{
            enemy.position = enemyPos;
        }
    }

    public void ChangePauseStatus(){
        if(isPaused){
            ResumeGame();
        }
        else{
            PauseGame();
        }
    }

    public void PauseGame(){
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void RoundEnd(){
        Time.timeScale = 1f;
        if(uiGameTimer != null){
            uiGameTimer.StopTimer();
        }

        if(enemyAIAudio != null){
            enemyAIAudio.SetActive(false);
        }

        if(pauseButton != null){
            pauseButton.SetActive(false);
        }

        if(returnToStartMenuButton != null){
            returnToStartMenuButton.SetActive(true);
        }
    }

    public void ReturnToStartMenu(){
        // ensures the UI sound click is played
        StartCoroutine(ReturnToStartMenuWithWait());
    }

    IEnumerator ReturnToStartMenuWithWait(){
        Time.timeScale = 1f;

        if(uiClick != null){
            uiClick.Play();
        }

        yield return new WaitForSecondsRealtime(menuDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

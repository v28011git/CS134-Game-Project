using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

   [Header("AI Speed")]
   public float initialSpeed = 3f;
   public float maxSpeed = 8f;
   public float timeUntilMaxSpeed = 180f;

   //used in time for the game round
   private float timerRound;

   //current speed of the enemy AI
   private float currentSpeed;

 // Reference to the player's transform.
 public Transform player;

 // Reference to the NavMeshAgent component for pathfinding.
 private NavMeshAgent navMeshAgent;

 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the NavMeshAgent component attached to this object.
        navMeshAgent = GetComponent<NavMeshAgent>();

        ResetSpeed();
    }

 // Update is called once per frame.
 void Update()
    {
      if(player == null || navMeshAgent == null){
         return;
      }

      // used to track how long the game round has been running
      timerRound += Time.deltaTime;

      //gives progress to reaching the max speed (0 to 1)
      float progressSpeed = Mathf.Clamp01(timerRound / timeUntilMaxSpeed);

      //used to slowly increase the speed
      currentSpeed = Mathf.Lerp(initialSpeed, maxSpeed, progressSpeed);

      navMeshAgent.speed = currentSpeed;

      // Set the enemy's destination to the player's current position.
      navMeshAgent.SetDestination(player.position);
        
    }

    public void ResetSpeed(){
      timerRound = 0f;
      currentSpeed = initialSpeed;
      if(navMeshAgent == null){
         navMeshAgent = GetComponent<NavMeshAgent>();
      }
      if(navMeshAgent != null){
         navMeshAgent.speed = currentSpeed;
         navMeshAgent.ResetPath();
      }
    }
}
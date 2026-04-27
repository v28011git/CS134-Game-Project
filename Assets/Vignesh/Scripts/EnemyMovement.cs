using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

   [Header("AI Speed")]
   //starting speed for enemy
   public float initialSpeed = 3f;
   //max speed for enemy
   public float maxSpeed = 8f;
   //time it takes for enemy ai to reach max speed
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

      // sets the enemy speed to its initial speed
      ResetSpeed();
   }

   // Update is called once per frame.
   void Update()
   {
      //makes sure that navMeshAgent and player are available
      if(player == null || navMeshAgent == null){
         return;
      }

      // used to track how long the game round has been running
      timerRound += Time.deltaTime;

      //gives progress to reaching the max speed (0 to 1)
      float progressSpeed = Mathf.Clamp01(timerRound / timeUntilMaxSpeed);

      //used to slowly increase the speed
      currentSpeed = Mathf.Lerp(initialSpeed, maxSpeed, progressSpeed);

      //gives the new current speed to the navMeshAgent
      navMeshAgent.speed = currentSpeed;

      // Set the enemy's destination to the player's current position.
      navMeshAgent.SetDestination(player.position);
        
   }

   //Used to reset the path and enemy speed
   public void ResetSpeed(){
      //Resets the game round timer
      timerRound = 0f;

      //changes the current speed back to the initial speed
      currentSpeed = initialSpeed;
      //makes sure that the navMeshAgent is assigned
      if(navMeshAgent == null){
         navMeshAgent = GetComponent<NavMeshAgent>();
      }
      //checks if the navMeshAgent is available
      // If so, it resets the path and sets it to initial speed
      if(navMeshAgent != null){
         navMeshAgent.speed = currentSpeed;
         navMeshAgent.ResetPath();
      }
   }
}
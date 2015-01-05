//@script RequireComponent(AudioSource)
using UnityEngine;
using System;
using System.Linq;
using System.Collections;

/* Here is a copy and an update from  http://unity3d.com/learn/tutorials/projects/stealth/enemy-ai
 Functions as the "Brain" of the Robot, TODO use StateMachine to update the states and select the behaviour*/
public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 4f;                          // The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 6f;                           // The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
	public float patrolWaitTime = 2f;                       // The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
	public GameObject[] patrols;							// An gameobject array to hold all patrol points


	//todo patrol tag and foreach over all waypoints to set them

	//copyed from Robot script
	public bool isActive = false; 							//is the robot activ?
	public bool enemyInSight = false;						//is the enemy in sight?
	public static bool initIsActiv = true; 				// what is the start value of the robot isActive-State
	public static float normalPowerUse = -1.0f;				// battery use under normal conditions
	public float powerChange;								// active powerChange, can change
	public float batteryLevel = 0f;							//batteryLevel - changes with every update
	public float initBatteryLevel = 90f; 			//Starting value for BatteryLevel
	public float maxBatteryLevel = 100f; 			//Maximum possible BatteryLevel 
	public float criticalBatteryLevel = 30f;			//Level of Battery to change state to recharge
	public float maxRechargingSpeed = 10f;			//Maximum recharging speed - to limit the recharge Duration per Robot
	public Vector3 myLastPosition;                   		// an 3d position of the robot, last known position, so he can return to it.
	public Vector3 myActualPosition;                     	// an 3d position of the robot, actual position

	public GameObject[] gos;								//tagged objects 
	public GameObject closest;								//closest object found
	public string state;									//state of robot
	//public string startingState = "Patrolling";				//starting state, todo, save as object or something other than string
	public string lastState;								//last state, save for later use


	//var stateChangeSound : AudioClip;


	//private string sound_criticalBattary = "3beep_2";			//transformname of the element holding the alarm Music
	public AudioSource robot_voice;

	public EnemySight enemySight;                          // Reference to the EnemySight script.
	public NavMeshAgent nav;                               // Reference to the nav mesh agent.
	//private Transform player;                               // Reference to the player's transform.
	//private PlayerHealth playerHealth;                      // Reference to the PlayerHealth script.
	public LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
	public float chaseTimer;                               // A timer for the chaseWaitTime.
	public float patrolTimer;                              // A timer for the patrolWaitTime.
	public int wayPointIndex;                              // A counter for the way point array.
	public float minPlayerDetectionRange = 2f;				// minimum range the player
	public Skynet skynet;          		               // Skynet object, needed for robot control

	public NavMesh mesh;									//nav mesh needed to calculate robot-Paths
	public Transform nextPowersupply;						//store the transform location of the nearest power supply
	private NavMeshPath pathToNextPowersupply;				//navMeshPath to NextPowersupply


	//public int numSelectors = 5;
	//public GameObject[] selectorArr;
	//public GameObject selector; //selected in the editor
	public GameObject[] waypoints;

	//HFSM implementation
	HFSM stateMachine;

	void Awake ()
	{

		// Setting up the references.
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		powerChange = normalPowerUse; //set normal power consumption

		//Creates a variable to check the objects position.
		//player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		//playerHealth = player.GetComponent<PlayerHealth>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
		changeState("Patrolling");
		//state = "Patrolling";//Starting state
		skynet = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<Skynet>();

		//Debug.Log (this.name);
		//set the patrol waypoints, to the right robot
		if (this.name == "Robot") {
			//patrol of robot 1
				patrols = GameObject.FindGameObjectsWithTag ("Patrol_1").OrderBy (go => go.name).ToArray ();
				for (int i = 0; i < patrols.Length; i++) {
					//Debug.Log("Waypoint Number "+i+" is named "+waypoints[i].transform.position.x);
					patrolWayPoints [i] = patrols [i].transform;
				}
		} else {
			//standard patrol (Robot_2)
			patrols = GameObject.FindGameObjectsWithTag ("Patrol_2").OrderBy (go => go.name).ToArray ();
			for (int i = 0; i < patrols.Length; i++) {
				//Debug.Log("Waypoint Number "+i+" is named "+waypoints[i].transform.position.x);
				patrolWayPoints [i] = patrols [i].transform;
			}

		}

	}

	//intial Robot 
	void Start(){
		batteryLevel = initBatteryLevel; //starting value for battery
		//only robot is active at the start of the level, robot_2 not
		//if (this.name == "Robot") {
		isActive = initIsActiv; //starting value for isActive
		//		} else {
		//	isActive = isActive;	
		//}
		//set starting State
		Debug.Log ("************************* HFSM New **************************");
		Debug.Log (this.name);
		this.stateMachine = new HFSM (this.name);

		//waypoint_1.position = new Vector3(-15.67651f,0.5f,0f);
		//patrolWayPoints[0]. = waypoint_1;
		//patrolWayPoints[1] = waypoint_1;
		///patrolWayPoints[2] = waypoint_1;
		//patrolWayPoints[3] = waypoint_1;
		//patrolWayPoints[4] = waypoint_1;
		//Debug.Log (waypoint_1.position.ToString());
		//patrolWayPoints[] = waypoint_1;
		/*
		selectorArr = new GameObject[numSelectors];
		for (int i = 0; i < numSelectors; i++)
		{
			GameObject go = Instantiate(selector, new Vector3((float)i, 1, 0), Quaternion.identity) as GameObject;
			go.transform.localScale = Vector3.one;
			selectorArr[i] = go;
		}
		Debug.Log (waypoint_1);
		*/
	}

	
	//normal
	void Update ()
	{
		//is robot active?
		if (isRobotActive()) {
			//Debug.Log ("is active");
		



			addBatteryLevel(); //pro Update use/give some energy
			//switch to Recharging, if BatteryLevel is lower than critical state or the state is still in the recharching
			if(state == "Recharging"){//criticalBatteryLevel > batteryLevel || 
				//Debug.Log ("Battery critical! Robot needs to Recharge!");
				Recharging();
			}else{ // Enougth battery,... normal operations


				// If the player is in sight and is alive...
				//if(enemySight.playerInSight && playerHealth.health > 0f)
					// ... shoot.
					//Shooting();
				
				// If the player has been sighted and isn't dead...
				//else if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f)
				if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition){
				// ... chase.

					Chasing();
			
				// Otherwise...
				}else{
					//Debug.Log ("call patrolling");
				// ... patrol the waypoints

					Patrolling();
				}
			}

		} else {
			//Debug.Log ("is inactive");
			this.stateMachine.setState (this.stateMachine.IdleState);
			//do nothing 
		}
	}
	
	//not yet implemented, could be used if the robot can shoot the player
	public void Shooting ()
	{
		// Stop the enemy where it is.
		nav.Stop();
	}

	//XXXXXXXXXXXX OLD functions but working START XXXXXXXXXXXXX

	
	public void Chasing ()
	{
		this.stateMachine.setState (this.stateMachine.ChasingState);
		/*
		changeState("Chasing"); //set the state

		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
		//Debug.Log (sightingDeltaPos);
		//Debug.Log (sightingDeltaPos.sqrMagnitude);
		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > minPlayerDetectionRange)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
			nav.destination = enemySight.personalLastSighting;
		
		// Set the appropriate speed for the NavMeshAgent.
		nav.speed = chaseSpeed;
		
		// If near the last personal sighting...
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			chaseTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			chaseTimer = 0f;
			*/
	}

	
	public void Patrolling()
	{
		this.stateMachine.setState (this.stateMachine.PatrolState);

		/*
		//this.state = "Patrolling"; //set the state
		changeState("Patrolling"); //set the state
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		if(nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;
				
				// Reset the timer.
				patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;
		//Debug.Log ("set new waypoint position patrolling");
		// Set the destination to the patrolWayPoint.
//Debug.Log ("wayPointIndex");
//Debug.Log (wayPointIndex);
		nav.destination = patrolWayPoints[wayPointIndex].position;
		*/
	}

	//the robot needs to find and go to the nearest PowerSupplay to recharge his battery level
	public void Recharging()
	{
		this.stateMachine.setState (this.stateMachine.RechargingState);
		/*
		//save actual state and position, only do it on first entering 
		if (this.state != "Recharging") {
			changeState("Recharging");
			//lastState = this.state;
			myActualPosition = transform.position;
			myLastPosition = myActualPosition;
		}

		//set new target location
		nav.destination = FindClosestTagObject(Tags.powerSupply).transform.position;


		//recharging automatically if near a powerstation, ...


		//..change to last state when you are at maxBattaryLevel
		if(maxBatteryLevel <= batteryLevel){
			//back to last operation
			changeState(lastState); //set the state
			//this.state = ; //set the last state
			nav.destination = myLastPosition;//return to the last location
		}
		*/
	}


//XXXXXXXXXXXX OLD functions but working ENDE XXXXXXXXXXXXX

	//Drain or restore power to batteryLevel
	public void addBatteryLevel(){
		//currentBatteryLife -= batteryLifeLostPerSecond * Time.deltaTime; // Reduces the battery correctly over time
		this.batteryLevel = this.batteryLevel + this.powerChange * (float)Time.deltaTime; //Drain or restore power
		//cap the batteryLavel at the maximum and 0
		if(this.batteryLevel < 0){
			this.batteryLevel = 0;
		}else if(this.batteryLevel > maxBatteryLevel){
			this.batteryLevel = maxBatteryLevel;
		}
	}
	
	//Find closest Object with a given Tag name
	public GameObject FindClosestTagObject(string objectName){
		gos = GameObject.FindGameObjectsWithTag(objectName);

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	//if trigger is activated, use it to
	// - to recharge the robot
	void OnTriggerStay (Collider other)
	{
		//if it connects with powerSupply...
		if(other.gameObject.tag == Tags.powerSupply) 
		{
			powerChange = maxRechargingSpeed; //set to recharching speed
		}else{
			powerChange = normalPowerUse; //otherwise set to normal use speed
		}
	}

	//used to general actions between ALL statechanges, like playing a sounnd
	public void changeState(string toThisState ){
		//string logtext = "from State" + this.state + " to state " + toThisState; //used to debug things
		//Debug.Log(logtext);
		if (this.state != toThisState) {
			lastState = this.state;
			this.state = toThisState; //set the state
			audio.Play ();//play the robot beep
		}
	}


	//toggles the robot on and off
	public void toggleActive(){
		if(this.isActive){
			this.isActive = false;
		}else{
			this.isActive = true;
		}
	}

	//return the robot state
	public bool isRobotActive(){
		//Debug.Log ("check robot state");
		//only if skynet is activ and robot is activ, robot is working
		if (skynet.getRobotState() && this.isActive) {
			//Debug.Log ("run Nav Mesh Agent");
			nav.Resume();//navMesh Agent resums the original speed
			return true;
		} else {
			//Debug.Log ("stop Nav Mesh Agent");
			nav.speed = 0;//navMesh Agent Stops his action.
			nav.Stop();
			return false;


		}

	}


}
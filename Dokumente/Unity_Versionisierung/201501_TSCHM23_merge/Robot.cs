using UnityEngine;
using System.Collections;

//Veraltet! Verwende EnemyAI
//Veraltet! Verwende EnemyAI
//Veraltet! Verwende EnemyAI
//Veraltet! Verwende EnemyAI//Veraltet! Verwende EnemyAI
//Veraltet! Verwende EnemyAI
//Veraltet! Verwende EnemyAI











public class Robot : MonoBehaviour {
	//public string name; //geerbt

	public bool isActive = false; //is the robot activ?
	public bool enemyInSight = false;//is the enemy in sight?
	public static bool initIsActiv = false; // what is the start value of the robot isActive-State
	public static float powerChange = -0.8f;// during this "state", add or subtract batteryLevel
	public float batteryLevel = 0f;//batteryLevel - changes with every update
	public static float initBatteryLevel = 50f; //Starting value for BatteryLevel
	public static float maxBatteryLevel = 100f; //Maximum possible BatteryLevel 
	public static float criticalBatteryLevel = 30f;//Level of Battery to change state to recharge
	public float maxRechargingSpeed = 10f;//Maximum recharging speed - to limit the recharge Duration per Robot

	//Die Roboterwerte initialisieren
	void Start(){
		this.batteryLevel = initBatteryLevel; //starting value for battery
		this.isActive = initIsActiv; //starting value for isActive
	}

	void Update(){
		//addBatteryLevel (); //pro Update einmal die "Energie" der aktion verbrauchen.
		//if(this.batteryLevel < criticalBatteryLevel){
		//	recharching();
		//}
	}

	//prüft, ob ein Spieler sich in sichtweite befindet, könnte man evtl. vererben
	void checkSight(){

	}

	//prüft, welche Zustände der Roboter hat und was als nächstes gemacht werden muss -> states
	void checkRoboterSystems(){
		
	}

	//schaltet den Roboter ein oder aus
	void toggleActive(){
		if(isActive){
			this.isActive = false;
		}else{
			this.isActive = true;
		}
	}

	public bool getActiveState(){
		return this.isActive;

	}

	void addBatteryLevel(){
		//currentBatteryLife -= batteryLifeLostPerSecond * Time.deltaTime; // Reduces the battery correctly over time
		this.batteryLevel = this.batteryLevel + powerChange * (float)Time.deltaTime; //Drain or restore power
	}



}

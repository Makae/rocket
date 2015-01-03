using UnityEngine;
using System.Collections;

public class Environment {

	private bool powerState;
	private bool powerless;
	private bool isEmpty;

	public Environment() {
		this.powerState = false;
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//
		
	}

	public bool onMessage(Telegram tg){
		return true;
	}

	public bool getPowerState (){
		return powerState;
	}
	public void setPowerState (bool onPower){
		this.powerState = onPower;
	}



}

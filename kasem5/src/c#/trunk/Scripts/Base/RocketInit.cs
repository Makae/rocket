using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketInit : MonoBehaviour, IMessageReceiver {
	public string EntKey {
		get { 
			return "init_script"; 
		} 
	}
	void Awake() {
		EntityManager.RegisterEntity(this.EntKey, this);
	}
	
	public void OnMessage(Telegram telegram) {

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if(Input.GetKeyUp(KeyCode.F)) {
			Debug.LogWarning(((Inventory) EntityManager.GetEntity("inventory")).GetCurrentItem());
		}			
	}
}

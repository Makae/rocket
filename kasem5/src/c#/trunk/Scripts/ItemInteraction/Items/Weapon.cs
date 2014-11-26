using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : Item {

	public int Damage = 1;

	override public void OnMessage(Telegram t) {
		Debug.Log ("Got Message");
		Debug.Log (t.Message);
	}
	
	new public void Awake() {
		base.Awake();
	}

}
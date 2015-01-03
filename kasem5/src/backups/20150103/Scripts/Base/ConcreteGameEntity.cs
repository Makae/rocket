using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class ConcreteGameEntity : AbstractGameEntity {

	override public string EntKey {
		get { 
			return "mein_scheich"; 
		} 
	}

	override public List<string> Groups {
		get { 
			return new List<string>(new string[2]{"light", "power"});
		}
	}

	override public void OnMessage(Telegram t) {
		Debug.Log ("Got Message");
		Debug.Log (t.Message);
	}

	new public void Awake() {
		base.Awake();
	}
}
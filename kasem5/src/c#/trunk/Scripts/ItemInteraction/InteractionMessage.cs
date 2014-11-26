using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionMessage : MonoBehaviour, IMessageReceiver {
	public string[][] Label;
	private static int Counter = 0;
	private string EntKey;

	void Start() {
		this.EntKey = "im_" + InteractionMessage.Counter++;
		EntityManager.RegisterEntity(this.EntKey, this);
	}

	void Update() {

	}
	
	public void OnMessage(Telegram t) {
      Debug.Log(t);
	}	

}
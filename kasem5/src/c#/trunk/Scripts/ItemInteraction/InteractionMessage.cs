using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionMessage : MonoBehaviour, IMessageReceiver {
	public string[][] Label;
	private static int Counter = 0;
	private int Idx;
	private string Identifier = "im";
	public string EntKey {
		get { 
			return this.Identifier + "_" + this.Idx; 
		} 
	}

	void Start() {
		this.Idx = InteractionMessage.Counter++;
		EntityManager.RegisterEntity(this.EntKey, this);
	}

	void Update() {

	}
	
	public void OnMessage(Telegram t) {
      Debug.Log(t);
	}	

}
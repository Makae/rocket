using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionMessage : MonoBehaviour, IMessageReceiver {
	// Standard message if no itemKey matches
	public string StdLabel;
	// The length of ItemKeys and Labels has to be the same, they correspond with each other
	// The strings are predefinied in order to provide input-fields in the Unity-Inspector
	// "none" values are then ignored
	public string[] ItemKeys = new string[]{"none", "none", "none", "none", "none", "none"};
	public string[] Labels = new string[]{"none", "none", "none", "none", "none", "none"};
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
		TextDisplayer.setMessage(this.StdLabel, 2, 10.0f);
	}	

}
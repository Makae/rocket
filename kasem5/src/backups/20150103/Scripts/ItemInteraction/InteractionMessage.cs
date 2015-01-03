using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionMessage : MonoBehaviour, IMessageReceiver {
	// Standard message if no itemKey matches
	public string StdLabel;
	// The length of ItemKeys and Labels has to be the same, they correspond with each other
	// The strings are predefinied in order to provide input-fields in the Unity-Inspector
	// "none" values are then ignored
	public string[] ItemKeys = new string[]{"none", "none", "none", "none", "none", "none", "none", "none", "none", "none"};
	public string[] Labels = new string[]{"none", "none", "none", "none", "none", "none", "none", "none", "none", "none"};
	private string NoneLabel = "none";
	private static int Counter = 0;
	private int Idx;
	private string Identifier = "im";
	private bool active = true;

	public string EntKey {
		get {
			return this.Identifier + "_" + this.Idx;
		}
	}

	void Start() {
		this.Idx = InteractionMessage.Counter++;
		EntityManager.RegisterEntity(this.EntKey, this);
		EntityManager.AddToGroup("interaction", this);
	}

	void Update() {

	}

	public void OnMessage(Telegram t) {
		Debug.Log (this.EntKey + "_" + t.Message);
		if(t.Message == "interrupt") {
			this.active = false;
		} else if(t.Message == "proceed") {
			this.active = true;
		}

		if(t.From == "player") {
			if(!this.active)
				return;

			if(!t.Args.ContainsKey("item"))
				return;

			Item item = (Item) t.Args["item"];


			if(item == null)
				return;

			string label = this.StdLabel;
			// Get the corresponding key to the item
			for(int i = 0; i<this.ItemKeys.Length; i++) {
				if(this.ItemKeys[i] == item.EntKey) {
					label = this.Labels[i];
					if(label == this.NoneLabel) {
						label = this.StdLabel;
					}
					break;
				}
			}
			TextDisplayer.setMessage(label, 2, 3.0f);
		}
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : AbstractGameEntity {
	
	override public string EntKey {
		get { 
			return this.Identifier + "_" + this.Idx; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[2]{"items", Item.GroupPrefix + this.Identifier});
		}
	}

	public string ItemLabel = "";
	public string Identifier = "";
	public Texture ItemTexture;

	protected int Idx = 0;
	protected static string GroupPrefix = "items_";
	
	private static int Counter = 0;

	override public void OnMessage(Telegram t) {
		Debug.Log ("Got Message");
		Debug.Log (t.Message);
	}
	
	new public void Awake() {
		this.Idx = Item.Counter++;
		this.AddProximityMessage();
		base.Awake();
	}

	private void AddProximityMessage() {
		this.gameObject.AddComponent("ProximityMessage");
		ProximityMessage pm = (ProximityMessage) this.gameObject.GetComponent ("ProximityMessage");
		pm.label = this.ItemLabel;
	}
	
	public string GetLabel() {
		return this.ItemLabel;
	}

	public void DrawTexture(int left, int top) {
		GUI.DrawTexture(new Rect(left, top, this.ItemTexture.height, this.ItemTexture.width), this.ItemTexture, ScaleMode.ScaleToFit, true);
    }


}
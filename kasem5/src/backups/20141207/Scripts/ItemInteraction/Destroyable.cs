using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroyable : AbstractGameEntity, IDestroyable {
	public int Health = 100;
	
	override public string EntKey {
		get { 
			return "crate"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"destroyable"});
		}
	}
	
	override public void OnMessage(Telegram t) {
		if(t.Message != "hurt")
			return;
		
		this.Health = this.Health - (int) t.Args["amount"];
		TextDisplayer.setMessage("Duration: " + this.Health, 2, 2.0f);
		if(this.Health <=0)
			this.transform.localPosition = new Vector3(0, 9999, 0);
		
	}
	
	new public void Awake() {
		base.Awake();
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketInit : MonoBehaviour, IMessageReceiver {
	private string EntKey = "init_script";
	void Awake() {
		EntityManager.RegisterEntity(this.EntKey, this);
	}
	
	public void OnMessage(Telegram telegram) {

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			/* Tests MessageDispatcher */ 
			
			//MessageDispatcher md = MessageDispatcher.Instance();
			/*md.Dispatch(0, this.EntKey, "group:light", "Mei Meschagee");
			md.Dispatch(1, this.EntKey, "group:light", "Mei delöied Meschagee");
			*/

			/* Tests Item and Inventory */
			Inventory inv = (Inventory) EntityManager.GetEntity("inventory");
			Item item = (Item) EntityManager.GetEntity("potion");
			inv.AddItem(item);
			inv.AddItem((Item) EntityManager.GetEntity("crowbar"));
			inv.AddItem((Item) EntityManager.GetEntity("fuse"));
		}

		if(Input.GetKeyUp(KeyCode.F)) {
			Debug.LogWarning(((Inventory) EntityManager.GetEntity("inventory")).GetCurrentItem());
		}			
	}
}

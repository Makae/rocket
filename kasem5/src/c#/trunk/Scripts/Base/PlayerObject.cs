using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
public class PlayerObject : AbstractGameEntity {

	override public string EntKey {
		get { 
			return "player"; 
		} 
	}

	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"player"});
		}
	}

	public Inventory Inventory;
	public int DefaultDamage = 10;
	private Transform Camera;
	private bool initialized = false;

	private void Hit(IDestroyable comp) {
		float delay = 0;
		string from = this.EntKey;
		string to = ((AbstractGameEntity) comp).EntKey;
		string message = "hurt";
		Hashtable args = new Hashtable();
		args["amount"] = this.CurrentDamage();
		
		MessageDispatcher md = MessageDispatcher.Instance();
		md.Dispatch(delay, from, to, message, args);
	}
	
	private void Interact(IMessageReceiver comp) {
		float delay = 0;
		string from = this.EntKey;
		string to = ((IMessageReceiver) comp).EntKey;
		string message = "interact";

		Hashtable args = new Hashtable();
		args["item"] = this.Inventory.GetCurrentItem();
		
		MessageDispatcher.Instance().Dispatch(delay, from, to, message, args);
	}

	public void Interact(GameObject obj) {
		if((obj.GetComponent("Item") as Item) != null)
			this.PickupItem(obj);
		
		Component[] comps = obj.GetComponents<Component>();
		foreach(Component comp in comps) {
			if(PlayerObject.Implements(comp, typeof(IDestroyable))) {
				this.Hit((IDestroyable) comp);
			} else if(PlayerObject.Implements (comp, typeof(IMessageReceiver))) {
				this.Interact ((IMessageReceiver) comp);
			}
		}
	}
	
	private int CurrentDamage() {
		Item item = this.Inventory.GetCurrentItem ();
		
		if(item == null || !PlayerObject.Implements(item, typeof(Weapon)))
			return this.DefaultDamage;
		return (item as Weapon).Damage;
	}

	public void PickupItem(GameObject obj) {
		if((obj.GetComponent("Item") as Item) == null) {
			Debug.LogError("Bad Interaction with object as Item");
			return;
		}
		
		this.AddToInventory((Item) obj.GetComponent("Item"));
		obj.transform.localPosition = new Vector3(0, 9999, 0);
	}

	private void AddToInventory(Item item) {
		this.Inventory.AddItem(item);
		// Remove Item from scene, but it has to stay as instance to be rendered in the inventory
		
	}
	
	// Checks if an object implements a specific interface
	public static bool Implements(object obj, Type t) {
		if(t.IsAssignableFrom(obj.GetType()))
			return true;
		return false;
	}

	public static bool Implements(GameObject obj, Type t) {
		Component[] comps = obj.GetComponents<Component>();
		foreach(Component comp in comps) {
			if(PlayerObject.Implements(comp, t))
				return true;
		}
		return false;
	}
	

	public bool LookingAt(GameObject obj) {
		return this.LookingAt(obj, 2.5f);
	}

	public bool LookingAt(GameObject obj, float maxReach) {
		Vector3 fwd = this.Camera.TransformDirection(Vector3.forward);
		RaycastHit hit = new RaycastHit();
		
		if(Physics.Raycast(this.Camera.position, fwd, out hit, maxReach)) {
			GameObject hit_obj = hit.transform.gameObject;
			return obj == hit_obj;
		}
		return false;
	}

	override public void OnMessage(Telegram t) {
		Debug.Log ("Got Message");
		Debug.Log (t.Message);
	}

	new public void Awake() {
		this.Camera = this.transform.Find("Main Camera");
		base.Awake();
	}

	public void Update() {
		if(!this.initialized) {
			return;
			this.Inventory = (Inventory) this.GetComponent ("Inventory");
			GameObject hands = GameObject.FindGameObjectWithTag("Hands");
			this.PickupItem(hands);
			this.initialized = true;
		}
	}
}
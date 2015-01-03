using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/**
 * The PlayerObject is responsible for the interaction of the player with the various
 * GameObjects inside the scene. It decides which data is sended to an  Interacable object, when
 * an interaction happens
 */
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
	public int DefaultDamage = 0;
    public float minimumLookingDistance = 3f;
	private Transform Camera;
	private bool initialized = false;
	
	public void Start() {
		this.Inventory = ((Inventory) this.GetComponent("Inventory"));
	}

	/**
	 * Is used, when the user interacts with a destroyable object (Crates / Wooden doors)
	 * The Damage of the Inventory-Object he is currently holding is added to the base-damage
     * a Telegram is then sent to the destructable object with the total attack damage
	 */
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
	
	/**
	 * This is used for a normal interaction (buttons / switches and so on)
	 */
	private void Interact(IMessageReceiver comp) {
		float delay = 0;
		string from = this.EntKey;
		string to = ((IMessageReceiver) comp).EntKey;
		string message = "interact";

		Hashtable args = new Hashtable();
		args["item"] = this.Inventory.GetCurrentItem();
		
		MessageDispatcher.Instance().Dispatch(delay, from, to, message, args);
	}

	/**
	 * Interacts witch each GameEntity assigned to a game object and executes the
     * Corresponding Interaction (Hit / Interact / PickupItem)
	 */
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
	
	/**
	 * Calculates the current damage the player is dealing witch the weapon in his hand.
	 */
	private int CurrentDamage() {
		Item item = this.Inventory.GetCurrentItem ();
		
		if(item == null || !PlayerObject.Implements(item, typeof(Weapon)))
			return this.DefaultDamage;
		return (item as Weapon).Damage;
	}

	/**
	 * Adds an Item to the inventory and relocates the gameobject out of the way
	 */
	public void PickupItem(GameObject obj) {
		if((obj.GetComponent("Item") as Item) == null) {
			Debug.LogError("Bad Interaction with object as Item");
			return;
		}
		
		this.AddToInventory((Item) obj.GetComponent("Item"));
		// Remove Item from scene, but it has to stay as instance to be rendered in the inventory
		obj.transform.localPosition = new Vector3(0, 9999, 0);
	}

	private void AddToInventory(Item item) {
		this.Inventory.AddItem(item);
	}
	
	/** 
	 * Checks if an object implements a specific interface 
	 */
	public static bool Implements(object obj, Type t) {
		if(t.IsAssignableFrom(obj.GetType()))
			return true;
		return false;
	}

	/** 
	 * Checks if a Component of a GameObject implements a specific interface 
	 */
	public static bool Implements(GameObject obj, Type t) {
		Component[] comps = obj.GetComponents<Component>();
		foreach(Component comp in comps) {
			if(PlayerObject.Implements(comp, t))
				return true;
		}
		return false;
	}
	
	/**
	 * Sugar for non provided distance of the LookingAt mehtod
	 */
	public bool LookingAt(GameObject obj) {
		return this.LookingAt(obj, this.minimumLookingDistance);
	}

	/**
	 * Determines based on a raycast if a player is looking at an specific GameObject
	 *
	 * @param GameObject obj the relevant GameObject
	 * @param float maxReach the maximum distance of the Raycast
	 */
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
		
	}

	new public void Awake() {
		this.Camera = this.transform.Find("Main Camera");
		base.Awake();
	}

}
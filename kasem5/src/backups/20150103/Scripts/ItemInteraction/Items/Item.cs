using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Item : AbstractGameEntity {

	override public string EntKey {
		get {
			return this.Identifier;
		}
	}

	override public List<string> Groups {
		get {
			return new List<string>(new string[1]{"items"});
		}
	}

	/* GUI properties */
	public string ItemLabel = "";
	public string Identifier = "";
	public Texture ItemTexture;

	/* Entity properties */
	protected int Idx = 0;
	protected bool Stackable = false;
	protected static string GroupPrefix = "items_";
	private static int Counter = 0;

	/* Scaling animation properties */
	private PlayerObject player = null;
	private Vector3 scale;
	private Vector3 scaleTo;
	private float animTime = 0.1f;
	private float timePassed;
	private bool prevLooking = false;

	override public void OnMessage(Telegram t) {
	}
	
	/**
	 * Overrides the parent Awake method and sets the Index
     */
	new public void Awake() {
		this.timePassed = this.animTime;
		this.Idx = Item.Counter++;
		this.AddProximityMessage();
		this.scale = this.gameObject.transform.localScale;
		this.scaleTo = this.scale * 0.2f;
		base.Awake();
	}

	/**
     * Provides the scaling animation for when the player is looking at the item
     */
	public void Update() {
		if(this.player == null)
			this.player = (PlayerObject) EntityManager.GetEntity("player");
		if(this.player == null)
			return;

		Vector3 startScale;
		Vector3 endScale;
		if(this.player.LookingAt(this.gameObject)) {
			startScale = this.scale;
			endScale = this.scale + this.scaleTo;

			if(!prevLooking) {
				this.timePassed = 0;
			}

			this.prevLooking = true;
		} else {
			startScale = this.scale + this.scaleTo;
			endScale = this.scale;

			if(prevLooking) {
				this.timePassed = 0;
			}

			this.prevLooking = false;
		}

		this.timePassed += Time.deltaTime;
		if(this.timePassed > this.animTime)
			return;

		float partial =  this.timePassed / this.animTime;
		this.gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, partial);
	}

	/**
	 * Adds a ProximityMessage with the Label as text to the item.
	 */
	private void AddProximityMessage() {
		this.gameObject.AddComponent("ProximityMessage");
		ProximityMessage pm = (ProximityMessage) this.gameObject.GetComponent ("ProximityMessage");
		pm.label = this.ItemLabel;
	}

	public string GetLabel() {
		return this.ItemLabel;
	}

	/**
	 * Draw the icon of the Item at the provided coordinates.
     * Currently used by the inventory
	 */
	public void DrawTexture(int left, int top) {
		GUI.DrawTexture(new Rect(left, top, this.ItemTexture.height, this.ItemTexture.width), this.ItemTexture, ScaleMode.ScaleToFit, true);
	}


}
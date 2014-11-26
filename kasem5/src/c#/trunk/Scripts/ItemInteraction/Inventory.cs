// C#
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	/* Styles */
	private int Padding = 10;
	private int ImageSize = 32;
	private int Margin = 30;
	private int LineHeight = 40;
	private int InvWidth = 100;
	private GUIStyle StyleLabelActive = null;
	private GUIStyle StyleLabelInActive = null;

	/* Item Handling in List*/
	private List<Item> Items = new List<Item>();
	private int CurrentItem = 0;
	private float NextScroll = 0;
	private float ScrollTimer = 0.05f;
	

	public void AddItem(Item item) {
		this.Items.Add (item);
	}

	public Item GetCurrentItem() {
		if(this.Items.Count == 0) {
			return null;
		}
		return this.Items[this.CurrentItem];
	}

	public void Awake() {
		this.StyleLabelActive = new GUIStyle { normal = new GUIStyleState { textColor = new Color(150, 150, 150)} };
		this.StyleLabelInActive = new GUIStyle { normal = new GUIStyleState { textColor = new Color(255, 0, 0)} };
		EntityManager.RegisterEntity("inventory", this);
	}

	public void Update() {
		//Push Wheel forward
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			this.JumpSelect(false);
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			this.JumpSelect(true);
		}

	}

	private void JumpSelect(bool forward) {
		if(this.NextScroll < Time.time) {
			if(this.Items.Count == 0)
				return;
			this.CurrentItem += (forward ? 1 : -1);
			if(this.CurrentItem < 0)
				this.CurrentItem = this.Items.Count - 1;
			if(this.CurrentItem >= this.Items.Count)
				this.CurrentItem = 0;
			this.NextScroll = Time.time + this.ScrollTimer;
		}
	}

	public void OnGUI () {
		int InvHeight = Screen.height;
		
		GUI.BeginGroup (new Rect (Screen.width - this.InvWidth, Screen.height - InvHeight, this.InvWidth, InvHeight));
		
		GUI.Box (new Rect (0, 0, this.InvWidth, InvHeight), "Inventory");
		this.renderItems ();
		

		GUI.EndGroup ();
	}
	
	private void renderItems() {
		int label_width = this.InvWidth - 2 * Padding;
		int label_height = this.LineHeight;
		int left = this.Padding + this.ImageSize;
		for(int i = 0; i < this.Items.Count; i++) {
			Item item = this.Items[i];
			int top = this.Margin + label_height * i;

			item.DrawTexture(left - this.ImageSize - 5, top - 8);
			if(i == this.CurrentItem) {
				GUI.Label (new Rect(left,  top, label_width, this.LineHeight), item.GetLabel(), this.StyleLabelInActive);
			} else {
				GUI.Label (new Rect(left,  top, label_width, this.LineHeight), item.GetLabel(), this.StyleLabelActive);
			}
			
		}
	}
}
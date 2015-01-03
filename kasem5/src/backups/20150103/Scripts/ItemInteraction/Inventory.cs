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
	private int InvWidth = 125;
	private GUIStyle StyleLabelActive = null;
	private GUIStyle StyleLabelInActive = null;
	
	/* Item Handling in List*/
	private List<List<object>> Entries = new List<List<object>>();
	private int CurrentEntry = 0;
	private float NextScroll = 0;
	private float ScrollTimer = 0.05f;
	
	
	public void AddItem(Item item) {
		if(PlayerObject.Implements(item, typeof(Stackable)))
			this.AddStackableItem((Stackable) item, ((Stackable) item).Amount);
		else
			this.Entries.Add(new List<object>{item, 1, 1});
		
	}
	
	public void AddStackableItem(Stackable item, int amount) {
		for(var i = 0; i < this.Entries.Count; i++) {
			List<object> entry = this.Entries[i];
			Stackable entry_item = (Stackable) entry[0];
			if(entry_item.StackGroup == item.StackGroup) {
				this.Entries[i][1] = (int) this.Entries[i][1] + 1;
				this.Entries[i][2] = (int) this.Entries[i][2] + amount;
				return;
			}
		}

		this.Entries.Add (new List<object>{item, 1, amount});
	}

	public void RemoveItem(Stackable item, int amount) {

		for(var i = 0; i < this.Entries.Count; i++) {
			List<object> entry = this.Entries[i];
			Stackable entry_item = (Stackable) entry[0];
			if(entry_item.StackGroup != item.StackGroup)
				continue;

			if((int) entry[1] - amount < 0)
				this.Entries.Remove(entry);
			else
				this.Entries[i][2] = (int) this.Entries[i][2] - amount;
			break;
		}
		this.CheckSelectItem();

	}

	public void RemoveItem(Item item) {
		if(PlayerObject.Implements (item, typeof(Stackable))) {
			this.RemoveItem((Stackable) item, 1);
			return;
		}

		for(var i = 0; i < this.Entries.Count; i++) {
			List<object> entry = this.Entries[i];
			Item entry_item = (Item) entry[0];

			if(entry_item != item)
				continue;

			this.Entries.Remove(entry);
			break;
		}
		this.CheckSelectItem();
	}
	
	public Item GetCurrentItem() {
		if(this.Entries.Count == 0) {
			return null;
		}
		return (Item) this.Entries[this.CurrentEntry][0];
	}
	
	/**
	 * Returns the Entries which are assigned to the Inventory
	 * @return List<object> item_list has two entries, first is the item object, second the number of items
	 */
	public List<List<object>> GetItems() {
		return this.Entries;
	}

	public void Awake() {
		this.StyleLabelActive = new GUIStyle { normal = new GUIStyleState { textColor = new Color(150, 150, 150)} };
		this.StyleLabelInActive = new GUIStyle { normal = new GUIStyleState { textColor = new Color(255, 0, 0)} };
		EntityManager.RegisterEntity("inventory", this);
	}
	
	public void Update() {
		//Push Wheel forward
		if(Input.GetAxis("Mouse ScrollWheel") > 0) {
			this.JumpSelect(false);
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			this.JumpSelect(true);
		}
		
	}
	
	/**
	 * Selects the next entry inside the inventory based on the direction
	 * "Debouncing" implemented
	 */
	private void JumpSelect(bool forward) {
		if(this.NextScroll < Time.time) {
			if(this.Entries.Count == 0)
				return;
			this.CurrentEntry += (forward ? 1 : -1);
			this.CheckSelectItem();
			this.NextScroll = Time.time + this.ScrollTimer;
		}
	}
	
	/**
	 *	Selects a new item if the current index is invalid
	 */
	private void CheckSelectItem() {
		if(this.CurrentEntry < 0)
			this.CurrentEntry = this.Entries.Count - 1;
		if(this.CurrentEntry >= this.Entries.Count)
			this.CurrentEntry = 0;
	}
	
	public void OnGUI () {
		int InvHeight = Screen.height;
		
		GUI.BeginGroup (new Rect (Screen.width - this.InvWidth, Screen.height - InvHeight, this.InvWidth, InvHeight));
		
		GUI.Box (new Rect (0, 0, this.InvWidth, InvHeight), "Inventory");
		this.renderEntries ();
		
		
		GUI.EndGroup ();
	}
	
	/**
     * Renders the Inventory on the side of the screen, sets different color for the current Item
	 */
	private void renderEntries() {
		int label_width = this.InvWidth - 2 * Padding;
		int label_height = this.LineHeight;
		int left = this.Padding + this.ImageSize;
		for(int i = 0; i < this.Entries.Count; i++) {
			List<object> entry = this.Entries[i];
			Item item = (Item) entry[0];
			int num = (int) entry[1];
			int amount = (int) entry[2];
			int top = this.Margin + label_height * i;
			string label = item.GetLabel();
			if(amount > 1)
				label = label + " (" + amount + ")";
			item.DrawTexture(left - this.ImageSize - 5, top - 8);
			if(i == this.CurrentEntry) {
				GUI.Label (new Rect(left,  top, label_width, this.LineHeight), label, this.StyleLabelInActive);
			} else {
				GUI.Label (new Rect(left,  top, label_width, this.LineHeight), label, this.StyleLabelActive);
			}
			
		}
	}
}
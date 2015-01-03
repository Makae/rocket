using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseCursor : MonoBehaviour, IMessageReceiver {
	public Texture2D yourCursor;  // Your cursor texture
	public int cursorSizeX = 32;  // Your cursor size x
	public int cursorSizeY = 32;  // Your cursor size y
	public int cursorOffsetX = 0;
	public int cursorOffsetY = 0;
	public bool enabled = false;

	public string EntKey {
		get {
			return "mouse_cursor";
		}
	}
	
	public void Start() {
	    Screen.showCursor = false;
        EntityManager.RegisterEntity(this.EntKey, this);
	}
	
	public void Update() {
	}
	
	//Show as gui component, show mouse curser
	public void OnGUI() {
		if(!this.enabled)
			return;
		GUI.DrawTexture (new Rect(Event.current.mousePosition.x - this.cursorOffsetX, Event.current.mousePosition.y - cursorOffsetY, this.cursorSizeX, this.cursorSizeY), this.yourCursor);
		
	}

	public void OnMessage(Telegram t) {
		Debug.Log ("t.message");
		if(t.Message == "disable") {
			this.enabled = false;
		} else if(t.Message == "enable") {
			this.enabled = true;
		}
	}
}
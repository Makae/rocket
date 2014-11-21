using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crosshair : MonoBehaviour {
	public string ItemLabel = "";
	public string Identifier = "";
	public Texture ItemTexture;
	public int Width = 25;
	public int Height = 25;	

	void Awake() {
	}

	public void OnGUI () {
		GUI.DrawTexture(new Rect((Screen.width - this.Height) / 2, (Screen.height - this.Width) / 2, this.Width, this.Height), this.ItemTexture, ScaleMode.ScaleToFit, true);
	}

	public string GetLabel() {
		return this.ItemLabel;
	}



}
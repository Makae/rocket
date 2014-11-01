using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	public int SpriteX = 0;
	public int SpriteY = 0;
	public string ItemLabel = "";
	public string Identifier = "";
	public Texture ItemSpriteTexture;

	public int TileWidth;
	public int TileHeight;
	private float TileWidthPart;
	private float TileHeightPart;
	private float TilePosXPart;
	private float TilePosYPart;
	
	private bool Drawn = false;

	void Awake() {
		this.TileWidthPart = this.TileWidth / (float) this.ItemSpriteTexture.width;
		
		this.TileHeightPart = this.TileHeight / (float) this.ItemSpriteTexture.height;
		this.TilePosXPart = this.TileWidthPart * (float) SpriteX;
		this.TilePosYPart = 1.0f - this.TileHeightPart * (float) SpriteY;

		EntityManager.RegisterEntity(this.Identifier, this);
		EntityManager.AddToGroup("items", this);
	}

	public string GetLabel() {
		return this.ItemLabel;
	}

	public void DrawTexture(int left, int top) {
//		if (!this.Drawn) {
//			Debug.Log ("left: " + left);
//			Debug.Log ("top: " + top);	
//			Debug.Log ("Tilewidht: " + this.TileWidth);
//			Debug.Log ("Tileheight: " + this.TileHeight);
//			Debug.Log ("tileposxpart: " + this.TilePosXPart);
//			Debug.Log ("tileposypart: " + this.TilePosYPart);
//			Debug.Log ("tilewidthpart: " + this.TileWidthPart);
//			Debug.Log ("tileheightpart: " + this.TileHeightPart);
//			Debug.Log ("itemsprite texture: " + this.ItemSpriteTexture);
//			this.Drawn = true;
//		}
		GUI.DrawTextureWithTexCoords(
			new Rect(left, top - (this.TileHeight / 2) + 10, this.TileWidth, this.TileHeight),
			this.ItemSpriteTexture,
			new Rect(this.TilePosXPart, (float) 1 - this.TilePosYPart, this.TileWidthPart, this.TileHeightPart));
    }


}
using UnityEngine;
using System.Collections;

/**
 * The Fusebox is a Level-GameObject which is used for providing the power to the level
 */
public class Fusebox : MonoBehaviour, IMessageReceiver {
	public string EntKey {
		get { 
			return "fuse_box"; 
		} 
	}

	private GameObject fuse;

	// Use this for initialization
	void Start () {
		this.fuse = GameObject.FindGameObjectWithTag("Fuse_Inserted");
		MeshRenderer m = this.fuse.GetComponent<MeshRenderer>();
		m.enabled = false;
		EntityManager.RegisterEntity(this.EntKey, this);
		EntityManager.AddToGroup("power", this);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnMessage(Telegram t) {
		Debug.Log(t.Message);
		if(t.From == "player") {

			if(!t.Args.ContainsKey("item"))
				return;

			Item item = (Item) t.Args["item"];


			if(item == null)
				return;
		
			if(item.EntKey != "fuse")
				return;

			MeshRenderer m = this.fuse.GetComponent<MeshRenderer>();
			m.enabled = true;

			PlayerObject player = (PlayerObject) EntityManager.GetEntity ("player");
			player.Inventory.RemoveItem(item);

			MessageDispatcher.Instance().Dispatch(0, this.EntKey, "group:power", "fuse_inserted");
		}
	}

}


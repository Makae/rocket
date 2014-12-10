using UnityEngine;
using System.Collections;

public class barrierScript : MonoBehaviour, IMessageReceiver {
	public string EntKey {
		get { 
			return "barrier_script"; 
		} 
	}

	public GameObject explosion;
	public float radius = 5.0f;
	public float power = 10.0f;
	public float explosion_time = 1.0f;
	private bool exploding = false;
	private float init_time;
	private GameObject bomb;
	private GameObject explosion_clone;
	private bool bombPlanted = false;
	private string code = "";	
	private GUIStyle TextboxStyle;

	
	void Start () {
		this.bomb = GameObject.FindGameObjectWithTag("Bomb_Pallet");
		MeshRenderer m = this.bomb.GetComponent<MeshRenderer>();
		m.enabled = false;
		EntityManager.RegisterEntity(this.EntKey, this);
	
		this.TextboxStyle = new GUIStyle { normal = new GUIStyleState { textColor = new Color(150, 150, 150)} };
		this.TextboxStyle.fontSize = 30;
		
		//this.TextboxStyle.lineHeight = 50;

	}

	void Update () {
		
	}

	/* Argument is percent from 0.0 to 1.0) */
	private float Parabel(float x) {
		//Debug.Log("Calculate Parabel");
		//Debug.Log(" Param:" + x);
		float result = -1 * Mathf.Pow((2 * x - 1), 2) + 1;
		Debug.Log(" Result:" + result);
		return result;
	}

	public void ApplyForceBeta() {
		Collider[] colliders = Physics.OverlapSphere (this.transform.position, radius);
		foreach(Collider hit in colliders) {
			if(hit && hit.rigidbody) {
				Vector3 force_direction = hit.transform.position - this.transform.position;
				force_direction.y = 0;
				hit.rigidbody.AddForce(force_direction * this.power, ForceMode.Impulse);
			}
		}
	}

	public void ApplyForce() {
		float passed_time = Time.time - this.init_time;
		float progress = 0.0f;
		// Debug.Log("Passed Time: " + passed_time);
		if(passed_time > 0.0f)
			progress = passed_time / this.explosion_time;
		
		float coefficient = this.Parabel(Mathf.Min(1, progress));
		// Debug.Log("Explosion Time:" + this.explosion_time);
		// Debug.Log("Progress Time:" + progress);
		// Debug.Log("Coefficient Time:" + coefficient);
		// Applies an explosion force to all nearby rigidbodies
		Collider[] colliders = Physics.OverlapSphere (this.transform.position, radius);
		Debug.Log(colliders.Length);
		foreach(Collider hit in colliders) {
			if(hit && hit.rigidbody) {
				Vector3 force_direction = hit.transform.position - this.transform.position;
				hit.rigidbody.AddForce(force_direction * this.power * coefficient);
			}
		}
		if(passed_time > this.explosion_time)
			this.FinishExplode();
	}

	public void Explode() {
		this.exploding = true;
		this.init_time = Time.time;
	}

	public void FinishExplode() {
		this.exploding = false;
	}

	public void OnGUI () {
		return;
		int padding = 300;
		int gui_width = Screen.width - padding;
		int gui_height = Screen.height - padding;
		
		GUI.BeginGroup (new Rect (padding/2, padding/2, gui_width, gui_height));
		
		GUI.Box (new Rect (0, 0, gui_width, gui_height), "ENTER BOMB CODE");
		
		this.code = GUI.TextField ( new Rect ((gui_width-200)/2, 20, 200, 30), this.code, 4, this.TextboxStyle);
		
		GUI.EndGroup ();
	}
	

	public void OnMessage(Telegram t) {
		Debug.Log (t.Message);
		if(t.From == "player") {

			if(!t.Args.ContainsKey("item"))
				return;

			Item item = (Item) t.Args["item"];


			if(item == null)
				return;
		
			if(item.EntKey != "bomb")
				return;

			MeshRenderer m = this.bomb.GetComponent<MeshRenderer>();
			m.enabled = true;
			MessageDispatcher.Instance ().Dispatch(5.0f, "barrier_script", "barrier_script", "booom");

			PlayerObject player = (PlayerObject) EntityManager.GetEntity ("player");
			player.Inventory.RemoveItem(item);

		} else if(t.Message == "booom") {
			MeshRenderer m = this.bomb.GetComponent<MeshRenderer>();
			m.enabled = false;
			this.explosion_clone = (GameObject) Instantiate(this.explosion, transform.position, transform.rotation);
			Destroy(gameObject);
			MessageDispatcher.Instance ().Dispatch(5.0f, "barrier_script", "barrier_script", "stop_booom");

			this.ApplyForceBeta();

		} else if(t.Message == "stop_booom") {
			Destroy(this.explosion_clone);
		}
	}

}


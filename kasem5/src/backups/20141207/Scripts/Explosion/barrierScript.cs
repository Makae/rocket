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
	
	private float init_time;
	private GameObject bomb;
	private GameObject explosion_clone;
	private bool gui_visible = false;
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

	public void ApplyForceDirect() {
		Collider[] colliders = Physics.OverlapSphere (this.transform.position, radius);
		foreach(Collider hit in colliders) {
			if(hit && hit.rigidbody) {
				Vector3 force_direction = hit.transform.position - this.transform.position;
				force_direction.y = 0;
				hit.rigidbody.AddForce(force_direction * this.power, ForceMode.Impulse);
			}
		}
	}

//
//	public void ApplyForce() {
//		float passed_time = Time.time - this.init_time;
//		float progress = 0.0f;
//		// Debug.Log("Passed Time: " + passed_time);
//		if(passed_time > 0.0f)
//			progress = passed_time / this.explosion_time;
//		
//		float coefficient = this.Parabel(Mathf.Min(1, progress));
//		// Debug.Log("Explosion Time:" + this.explosion_time);
//		// Debug.Log("Progress Time:" + progress);
//		// Debug.Log("Coefficient Time:" + coefficient);
//		// Applies an explosion force to all nearby rigidbodies
//		Collider[] colliders = Physics.OverlapSphere (this.transform.position, radius);
//		Debug.Log(colliders.Length);
//		foreach(Collider hit in colliders) {
//			if(hit && hit.rigidbody) {
//				Vector3 force_direction = hit.transform.position - this.transform.position;
//				hit.rigidbody.AddForce(force_direction * this.power * coefficient);
//			}
//		}
//		if(passed_time > this.explosion_time)
//			this.FinishExplode();
//	}

	public void toggleGUI() {
		this.code = "";
		MouseLook lookX = (MouseLook) GameObject.Find("Main Camera").GetComponent("MouseLook");
		MouseLook lookY = (MouseLook) GameObject.Find("FirstPersonController").GetComponent("MouseLook");
		CharacterMotor movement = (CharacterMotor) GameObject.Find("FirstPersonController").GetComponent("CharacterMotor");
		if(this.gui_visible) {
			lookX.enabled = true;
			lookY.enabled = true;
			movement.canControl = true;
			this.gui_visible = false;
			MessageDispatcher.Instance ().Dispatch(0f, "barrier_script", "group:interaction", "proceed");
		} else {
			lookX.enabled = false;
			lookY.enabled = false;
			movement.canControl = false;
			this.gui_visible = true;
			MessageDispatcher.Instance ().Dispatch(0f, "barrier_script", "group:interaction", "interrupt");
		}
	}

	public void setCode(int code) {
		if(code != this.currentCode())
			return;
		
		MeshRenderer m = this.bomb.GetComponent<MeshRenderer>();
		m.enabled = true;
		MessageDispatcher.Instance ().Dispatch(5.0f, "barrier_script", "barrier_script", "booom");
		
		PlayerObject player = (PlayerObject) EntityManager.GetEntity ("player");
		player.Inventory.RemoveItem((Item) EntityManager.GetEntity("bomb"));
		
		this.toggleGUI();
	}

	public int currentCode() {
		return 1234;
	}

	public void OnGUI () {
		if(!this.gui_visible)
			return;

		int padding = 300;
		int gui_width = Screen.width - padding;
		int gui_height = Screen.height - padding;
		
		GUI.BeginGroup (new Rect (padding/2, padding/2, gui_width, gui_height));
		
		GUILayout.BeginArea (new Rect (gui_width-53, 3, 50, 50));
		if(GUILayout.Button ("Close")) {
			Debug.Log ("CLOSE");
			this.toggleGUI();
		}
		GUILayout.EndArea ();
		GUI.Box (new Rect (0, 0, gui_width, gui_height), "ENTER BOMB CODE");
		GUI.Label (new Rect(40, 30, 40, 20), "Code: " , this.TextboxStyle);
		GUI.Box (new Rect (50 + (gui_width - 200)/2, 30, 120, 30), "");
		GUI.Box (new Rect (50 + (gui_width - 200)/2, 30, 120, 30), "");
		this.code = GUI.TextField (new Rect (50 + (gui_width - 200)/2, 30, 120, 30), this.code, 4, this.TextboxStyle);
		
		GUILayout.BeginArea (new Rect (260, 34, 53, 30));
		if(GUILayout.Button ("SET")) {
			this.setCode(int.Parse (this.code));
		}
		GUILayout.EndArea ();

		GUI.EndGroup ();
	}
	

	public void OnMessage(Telegram t) {
		if(t.From == "player") {

			if(!t.Args.ContainsKey("item"))
				return;

			Item item = (Item) t.Args["item"];


			if(item == null)
				return;
		
			if(item.EntKey != "bomb")
				return;
			
			this.toggleGUI ();

		} else if(t.Message == "booom") {
			MeshRenderer m = this.bomb.GetComponent<MeshRenderer>();
			m.enabled = false;
			this.explosion_clone = (GameObject) Instantiate(this.explosion, transform.position, transform.rotation);
			Destroy(gameObject);
			MessageDispatcher.Instance ().Dispatch(5.0f, "barrier_script", "barrier_script", "stop_booom");

			this.ApplyForceDirect();

		} else if(t.Message == "stop_booom") {
			Destroy(this.explosion_clone);
		}
	}

}


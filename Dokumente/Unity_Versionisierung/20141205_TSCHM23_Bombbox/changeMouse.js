var yourCursor : Texture2D;  // Your cursor texture
var cursorSizeX : int = 32;  // Your cursor size x
var cursorSizeY : int = 32;  // Your cursor size y
function Start()
{
    Screen.showCursor = false;
}

function Update(){
	if(Application.loadedLevelName == "level1"){
//Debug.Log(Application.loadedLevelName);
//Debug.Log("Curser hidden");
		//Screen.showCursor = false;
		fireScript = GetComponent("changeMouse");
		fireScript.enabled = false;
	}else{
//Debug.Log(Application.loadedLevelName);
//Debug.Log("Curser shown");
		//Screen.showCursor = true;
		fireScript = GetComponent("changeMouse");
		fireScript.enabled = true;
	}
}

//Show as gui component, show mouse curser
function OnGUI()
{
	//show curser only in menus
	//if(Application.LoadedLevelName != "level1"){
	//if(Application.loadedLevelName != "level1"){
		GUI.DrawTexture (Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, cursorSizeX, cursorSizeY), yourCursor);
	//}
}
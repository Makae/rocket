var isQuit=false;
var isStart=false;
var isOptions=false;
var isCredits=false;
var isLoadGame=false;
var isBackToMain=false;

//Menu Color
function OnMouseEnter(){
	renderer.material.color=Color.red;
}

//menu color back to normal
function OnMouseExit(){
	//change text color
	renderer.material.color=Color.yellow;
}

//Action on mouseclick
function OnMouseUp(){
	//is this quit
	if(isQuit==true) {
		//quit the game
		Application.Quit();
	}
	if(isStart==true){
		//load level
		Application.LoadLevel("level1");
	}
	if(isCredits==true){
		//open credits
		Application.LoadLevel("Menu_Credits");
	}
	if(isOptions==true){
		//open options
		Application.LoadLevel("Menu_Options");
	}
	if(isLoadGame==true){
		//open_load_game
		Application.LoadLevel("Menu_Load");
	}
	if(isBackToMain==true){
		//open_load_game
		Application.LoadLevel("Menu_Main");
	}
	
}
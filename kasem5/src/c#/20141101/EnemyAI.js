#pragma strict
public var state : String = 'pat';
private var old_state : String = null;
private var states : String[] = ['pat', 'inv', 'chase'];


function Awake () {

}

function Update() {
  /*
  if(state == 'pat')
    Patroulling(old_state == state);
  else if(state == 'inv')
    Investigating(old_state == state);
  else if(state == 'chasing')
    Chasing(old_state == state);
  old_state = state;*/
}
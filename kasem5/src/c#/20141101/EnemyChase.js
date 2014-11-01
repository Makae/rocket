#pragma strict

public var nav : NavMeshAgent;
public var waypoints : Transform[];
public var waypoint_idx : int = 0;
public var behaviour : String = 'random';

private var threshold : float = 0.5f;
private var enemyAI : EnemyAI;
// private var playerSighting : PlayerSighting;

function Awake() {
  nav = GetComponent(NavMeshAgent);
  enemyAI = GetComponent(EnemyAI);
  // playerSighting = GetComponent(PlayerSighting);
}

function Update() {
  if(!enemyAI.state == 'chase')
    return;

  // if(nav.destination != playerSighting.lastSighting)
  // 	nav.destination = playerSighting.lastSighting;

  // if(nav.distance >= threshold)
  //   Chase();
}

function Chase() {

  return;
}
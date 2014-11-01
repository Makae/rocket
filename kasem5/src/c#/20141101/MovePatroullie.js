#pragma strict

public var nav : NavMeshAgent;
public var waypoints : Transform[];
public var waypoint_idx : int = 0;
public var behaviour : String = 'random';

private var threshold : float = 0.5f;
private var enemyAI : EnemyAI;

function Awake() {
  nav = GetComponent(NavMeshAgent);
  enemyAI = GetComponent(EnemyAI);
}

function Update() {
  if(!enemyAI.state == 'pat')
    return;

  if(nav.remainingDistance <= threshold)
    TargetNextWaypoint();

}

function TargetNextWaypoint() {
  if(behaviour == 'random')
    waypoint_idx = Random.Range(0, waypoints.Length-1);

  waypoint_idx++;
  if(waypoint_idx >= waypoints.Length)
    waypoint_idx = 0;

  nav.destination = waypoints[waypoint_idx].position;
  return;
}
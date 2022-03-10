using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    //Player ist die einzige Referenz auﬂerhalb vom Enemy
    public Transform Player;
    //Alle andere Referenzen sind auf dem Enemy
    public float distanceToAttackRange;
    public float distanceToAttackMelee;
    public float distanceAlerted;

    public GameObject RelaxedSymbol;
    public GameObject AlertedSymbol;
    public GameObject LongRangeAttackSymbol;
    public GameObject MeleeAttackSymbol;

    public Transform bulletSpawnPos;

    [HideInInspector]
    public ObjectPooler objectPooler;

    [HideInInspector]
    public NavMeshAgent enemy;
    [HideInInspector]
    public float distance;
    
    EnemyBaseState currentState;
    public EnemyRelaxedState relaxedState = new EnemyRelaxedState();
    public EnemyAlertedState alertedState = new EnemyAlertedState();
    public EnemyLongRangeState longRangeState = new EnemyLongRangeState();
    public EnemyMeleeState meleeState = new EnemyMeleeState();

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        
        currentState = relaxedState;

        currentState.EnterState(this);
    }

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        distance = Vector3.Distance(enemy.transform.position, Player.transform.position);
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}

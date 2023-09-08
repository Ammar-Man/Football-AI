using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : StateMachineBehaviour
{
     bool  FootballWith, FootballBool;
    PCN_Move PCNPlayer, BluePlayer;
    NavMeshAgent Agent;
     GameObject Waypoint;
    Animator fsm;
    FootBall_On_Off Football;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       
        Waypoint = GameObject.Find("LeftAttack");
        fsm = animator.GetComponent<Animator>();
        Agent = animator.GetComponent<NavMeshAgent>();
       Agent.SetDestination(Waypoint.transform.position);
        //  FootballWith = fsm.GetBool("FootballWith");
        // Player = animator.gameObject.GetComponent<PCN_Move>();

        // FootballBool = Football.Getbool();
        // Player.


        PCNPlayer = GameObject.Find("PCNPlayer").GetComponent<PCN_Move>();
        BluePlayer = GameObject.Find("BluePlayer").GetComponent<PCN_Move>();
        Football = GameObject.Find("Football").GetComponent<FootBall_On_Off>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("FootballWith" + FootballWith);

        WhoPlayer(PCNPlayer);
        WhoPlayer(BluePlayer);

    }

   private void WhoPlayer(PCN_Move name)
    {
        FootballWith = name.FootballWith;

        if (name.FootballPlayerDis(5, name.gameObject))
        {
            name.SetSearchMode();
        }

        // If Football distans(2,5) < (5) true
        //   && (Player.FootballPCFDis(5))
        if (name.AgentToAttack)
        {
            if (FootballWith || (!name.FootballPlayerDis(5, name.gameObject)))
            {
                Debug.Log("ShootBall");
                Football.ShootBall(name);

            }
        }
    }

    
}


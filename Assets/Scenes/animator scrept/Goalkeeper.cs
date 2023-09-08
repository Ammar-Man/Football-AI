using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : StateMachineBehaviour
{
    PCN_Move  BlueKeeper, PCNKeeper;
    bool FootballWith;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        BlueKeeper = GameObject.Find("BlueKeeper").GetComponent<PCN_Move>();
        PCNKeeper = GameObject.Find("PCNKeeper").GetComponent<PCN_Move>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WhoKeeper(BlueKeeper);
        WhoKeeper(PCNKeeper);
    }

    private void WhoKeeper(PCN_Move name)
    {
        FootballWith = name.FootballWith;
       // Debug.Log(name+ "FootballWith" +  FootballWith);
        if (!name.FootballPlayerDis(35, name.gameObject) )
        {
            name.SetTargetBall();
            //Debug.Log(name + "FootballWith 111 " );
        }

        if (!FootballWith)
        {
            name.FootballFalse();
        }

        if (name.HomeGoalKeeperDis(name.gameObject))
        {
            name.SetNextWaypoint(name.HomeGoal);
            // Debug.Log(name+ "FootballWith 333 " + name.HomeGoal);
        }


    }
}

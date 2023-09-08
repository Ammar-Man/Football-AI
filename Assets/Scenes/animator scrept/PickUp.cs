using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class PickUp : StateMachineBehaviour
{
    PCN_Move PCNPlayer, BluePlayer, BlueKeeper, PCNKeeper;
    bool FootballWith, SeeFootball,FootballBool;
    FootBall_On_Off Football;
    bool one = true;
    bool two = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        PCNPlayer = GameObject.Find("PCNPlayer").GetComponent<PCN_Move>();
        BluePlayer = GameObject.Find("BluePlayer").GetComponent<PCN_Move>();

        BlueKeeper = GameObject.Find("BlueKeeper").GetComponent<PCN_Move>();
        PCNKeeper = GameObject.Find("PCNKeeper").GetComponent<PCN_Move>();
        //   Player = animator.gameObject.GetComponent<PCN_Move>();
        SeeFootball = animator.GetBool("SeeFootball");
        one = true;
        two = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        BlueKeeper.SetGoalkeeperTrue();
        PCNKeeper.SetGoalkeeperTrue();

        WhoPlayer(PCNPlayer);
        WhoPlayer(BluePlayer);

    }

    private void WhoPlayer(PCN_Move name)
    {
        FootballWith = name.FootballWith;
        

        if (!FootballWith)
        {
            Debug.Log("PickUp 1" + name.name);
            name.SetSearchMode();
            return;
           
        }


        if ( !name.AgentToAttack )
        {
           
            Debug.Log("PickUp 2 " + name.name);
            name.FootballSeeFalse();
            name.SetNextWaypoint(name.EnemyGoal);
            one = false;
            return;
        }


        if (!name.FootballPlayerDis(5, name.gameObject) )
        {
            
           name.SettAttackTrue();
            Debug.Log("PickUp 3" + name.name);
        
        }

    }

}



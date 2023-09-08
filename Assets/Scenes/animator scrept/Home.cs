using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Home : StateMachineBehaviour
{
    PCN_Move PCNPlayer, BluePlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PCNPlayer = GameObject.Find("PCNPlayer").GetComponent<PCN_Move>();
        BluePlayer = GameObject.Find("BluePlayer").GetComponent<PCN_Move>();
        // Player = animator.gameObject.GetComponent<PCN_Move>();
    }
    // if (distancePCN_Fo > nu)
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!BluePlayer.FootballWith) { WhoPlayer(BluePlayer); }
        if (!PCNPlayer.FootballWith) { WhoPlayer(PCNPlayer); }

        if (!PCNPlayer.FootballWith && !BluePlayer.FootballWith)
        {
            WhoPlayer(PCNPlayer);
            WhoPlayer(BluePlayer);
        }

    }

    private void WhoPlayer(PCN_Move name)
    {
        if (!name.FootballPlayerDis(35, name.gameObject))
        {

          //  name.SetSearchMode();
            name.SetHomeModeFalse();
        }


        if (name.HomeGoalPlayerDis())
        {
            name.SetNextWaypoint(name.Defence);

        }

    }


}

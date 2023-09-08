using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class SerachState : StateMachineBehaviour
{

    PCN_Move PCNPlayer, BlueKeeper, PCNKeeper, BluePlayer, Player;
    //   PCN_Move Player, BlueKeeper, PCNKeeper, PCNPlayer, BluePlayer;

    bool SeeFootball, FootballWith;
    FootBall_On_Off Football;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // samma som start metod
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         // Player = animator.GetComponent<PCN_Move>();
       
        BlueKeeper = GameObject.Find("BlueKeeper").GetComponent<PCN_Move>();
        PCNKeeper = GameObject.Find("PCNKeeper").GetComponent<PCN_Move>();

        PCNPlayer = GameObject.Find("PCNPlayer").GetComponent<PCN_Move>();
        BluePlayer = GameObject.Find("BluePlayer").GetComponent<PCN_Move>();

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //update state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        // animator.SetBool("Goalkeeper", true);
        // Player.SetGoalkeeperTrue();


       // BlueKeeper.SetSearchMode();
       // PCNKeeper.SetSearchMode();

        BlueKeeper.SetGoalkeeperTrue();
       PCNKeeper.SetGoalkeeperTrue();

       // WhoPlayer(PCNPlayer);
        if (!BluePlayer.FootballWith) { WhoPlayer(BluePlayer); }
        if (!PCNPlayer.FootballWith) { WhoPlayer(PCNPlayer); }

        if (!PCNPlayer.FootballWith && !BluePlayer.FootballWith) {
            WhoPlayer(PCNPlayer);
            WhoPlayer(BluePlayer);
        }

    }
    private void WhoPlayer(PCN_Move name)
    {

        
        FootballWith = name.FootballWith;
     //   Debug.Log(name+ "FootballWith" + FootballWith);
        if (FootballWith)
        {
            //  Debug.Log("FootballSeeFalse");
            name.FootballSeeFalse();
        };


        // If Football distans > (20) true
        if (!name.FootballPlayerDis(35 , name.gameObject))
        {
            name.SetTargetBall();
             // Debug.Log("catch the ball");
        }


        // DISTANSK > 20 = TRUE
        if (name.FootballPlayerDis(35,  name.gameObject))
        {
            //   Debug.Log("SetHomeMode");
            name.SetHomeMode();
        }

    }
}



/*
  SerachState har ett fucktion att finna vart ballen finns 
  och gi spelaren postionen till ballen sådant att kan gå finna den sen stopper den
   och positions funcktionen finns i spelarens pcn move script.
*/
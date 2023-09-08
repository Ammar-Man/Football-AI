using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PCN_Move : MonoBehaviour
{
    public NavMeshAgent Agent;
    public GameObject HomeGoal, EnemyGoal, Defence;
        //Blue_Goalkeeper, PCN_Goalkeeper;

    
    FootBall_On_Off Football;
    public Animator fsm;
    RaycastHit hit;
    public bool SeeFootball, FootballWith, AgentToAttack;
    int VisionDistance = 10, VisionAngle = 70;
    
    public float distanceToGoal, distanceToHome, distanceToFoot;
    Vector3 initialPos;
   

    void Start()
    {

        initialPos = transform.position;
        fsm = GetComponent<Animator>();

        Football = GameObject.Find("Football").GetComponent<FootBall_On_Off>();
       Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

            FootballWith = fsm.GetBool("FootballWith");
            SeeFootball = fsm.GetBool("SeeFootball");
        // den måste vara här annars blir de false

        //   if (!FootballWith) { seeFootball = false; }

        if (Football.GoalResult >= 1)
            {
                Football.FootballSpon();
                Goal();
                UnityEngine.Debug.Log("GoalResult 1");
            }

            //  distans < 18
            if (!GoalPlayerDis())
            {
                AgentToAttack = false;
            }

          
          //  distanceToGoal = Vector3.Distance(EnemyGoal.transform.position, Agent.transform.position);
          //  distanceToHome = Vector3.Distance(HomeGoal.transform.position, Agent.transform.position);
          /*
            Debug.DrawRay(transform.position, (Football.transform.position - transform.position), Color.red);


           
            if (Vector3.Distance(transform.position, Football.transform.position) <=
                VisionDistance && Vector3.Angle(transform.forward, (Football.transform.position - transform.position)) < VisionAngle)
            {
                if (Physics.Raycast(transform.position, Football.transform.position - transform.position, out hit))
                {

                    if (hit.collider.gameObject == EnemyGoal)
                    {
                      //  AgentToAttack = true;
                       // Debug.Log("true");
                    }

                }
            }
           */

        
    }

 

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == EnemyGoal)
            {
            AgentToAttack = true;
        }
        }

        private void OnCollisionEnter(Collision other)
        {
        if (other.collider.gameObject == EnemyGoal)
        {
            AgentToAttack = true;
            // Debug.Log("true");
        }
    }

        public void SetNextWaypoint(GameObject target = null)
        {
            Agent.SetDestination(target.transform.position);
        }

        public void SetTargetBall()
        {
            Agent.SetDestination(Football.transform.position);
        }

        public void SetHomeMode()
        {
            fsm.SetBool("Home", true);
        }
         public void SetHomeModeFalse()
    {
        fsm.SetBool("Home", false);
    }
         public void SetSearchMode()
        {
            fsm.SetBool("SeeFootball", true);
        }

        public void FootballFalse()
        { 
                fsm.SetBool("FootballWith", false);
        }

        public void FootballSeeFalse()
        {
            fsm.SetBool("SeeFootball", false);
        }

        public void FootballTrue()
        {
            fsm.SetBool("FootballWith", true);
        }

        public void SetGoalkeeperTrue()
        {
        fsm.SetBool("Goalkeeper", true);
        }
         public bool FootballPlayerDis(int nu, GameObject name)
        {

            distanceToFoot = Vector3.Distance(Football.transform.position, name.transform.position);

        if (distanceToFoot > nu)
            {
              //  Debug.Log(distanceToFoot);
                return true;
            }
            else return false;
        }

        public bool GoalPlayerDis()
        {
        //  distans 10 < 18

        distanceToGoal = Vector3.Distance(EnemyGoal.transform.position, Agent.transform.position);
        if (distanceToGoal < 17)
            {
                //Debug.Log(distancePCN_Fo);
                return true;
            }
            else return false;
        }

    public bool HomeGoalPlayerDis()
    {
        //  distans < 18
        distanceToHome = Vector3.Distance(HomeGoal.transform.position, Agent.transform.position);
        if (distanceToHome > 18)
        {
            // Debug.Log(distancePCN_Fo - nu);
            return true;
        }
        else return false;
    }

    public bool HomeGoalKeeperDis(GameObject name)
    {
        distanceToHome = Vector3.Distance(HomeGoal.transform.position, name.transform.position);

        //  distans < 18
        if (distanceToHome > 25)
        {
            // Debug.Log(distancePCN_Fo - nu);
            return true;
        }
        else return false;
    }


    public void SettAttackTrue()
    {
        fsm.SetBool("Attack", true);
    }
    public void Goal()
    {
        Agent.transform.position = initialPos;
        fsm.SetBool("SeeFootball", false);
        fsm.SetBool("FootballWith", false);
        fsm.SetBool("Home", false);
        fsm.SetBool("Attack", false);
        Debug.Log("Goal by:" + Agent.gameObject + "Goal loction : " + EnemyGoal);


        // PCN Home goal Right
        //      enemy Goal Left 
    }
}




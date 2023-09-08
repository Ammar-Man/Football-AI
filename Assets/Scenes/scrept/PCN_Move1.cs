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

public class PCN_Move1 : MonoBehaviour
{
    public NavMeshAgent Agent;
    public GameObject HomeGoal, EnemyGoal, Goalkeeper;
        //Blue_Goalkeeper, PCN_Goalkeeper;

    public GameObject[] Waypoint;
    FootBall_On_Off Football;
    public Animator fsm;
    RaycastHit hit;
    public bool seeFootball, FootballWith, AgentToAttack;
    int VisionDistance = 10, VisionAngle = 70;
    
    public float distanceToGoal, distanceToHome, distanceToFoot;
    Vector3 initialPos;
    GameObject currentTarget, RightGoal;

    void Start()
    {

        initialPos = transform.position;
        fsm = GetComponent<Animator>();

        Football = GameObject.Find("Football").GetComponent<FootBall_On_Off>();
        RightGoal = GameObject.Find("RightAttack");
       Agent = GetComponent<NavMeshAgent>();
        Goalkeeper =  GameObject.FindWithTag("Goalkeeper");
        // Blue_Goalkeeper = GameObject.Find("Blue_Goalkeeper");
        // PCN_Goalkeeper = GameObject.Find("PCN_Goalkeeper");


    }

    // Update is called once per frame
    void Update()
    {
       




        {
            // den måste vara här annars blir de false
            FootballWith = fsm.GetBool("FootballWith");
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

            distanceToFoot = Vector3.Distance(Football.transform.position, Agent.transform.position);
            distanceToGoal = Vector3.Distance(EnemyGoal.transform.position, Agent.transform.position);
            distanceToHome = Vector3.Distance(HomeGoal.transform.position, Agent.transform.position);

            Debug.DrawRay(transform.position, (Football.transform.position - transform.position), Color.red);

            seeFootball = false;


           
            if (Vector3.Distance(transform.position, Football.transform.position) <=
                VisionDistance && Vector3.Angle(transform.forward, (Football.transform.position - transform.position)) < VisionAngle)
            {
                if (Physics.Raycast(transform.position, Football.transform.position - transform.position, out hit))
                {

                    if (hit.collider.gameObject == EnemyGoal)
                    {
                        AgentToAttack = true;
                       // Debug.Log("true");
                    }

                }
            }
           

        }
    }

 

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == EnemyGoal)
            {
            Debug.Log("true");
         //   AgentToAttack = true;
            //fsm.GetBehaviour<SerachState> ().OnTriggerEnter(other);
        }
        }

        private void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.tag == "Football")
            {
                /*
                     Football.PickUpFootball2(Agent);
                     // Distans > 3 = true
                     if (!FootballPlayerDis(3))
                     {
                         Debug.Log("distans mindre än 3");
                        FootballTrue();

                     }

                     */
                //  Football.PickUpFootball(other);
            }
        }

        public void SetNextWaypoint(GameObject target = null)
        {
            Agent.SetDestination(target.transform.position);
        }

        public void SetTargetBall()
        {
            //  if (!Football || currentTarget == Football) return;
            // currentTarget = Football.gameObject;
            Agent.SetDestination(Football.transform.position);
        }

        public void SetHomeMode()
        {
            fsm.SetBool("Home", true);
        }

        public void SetSearchMode()
        {
            fsm.SetBool("SeeFootball", true);
        }

        public void FootballFalse()
        { 
       // distans 6 > 5 = true
          //  if (!FootballPlayerDis(5))  return;

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
    // FootballDist > nu = true
    public bool FootballPlayerDis(int nu)
        {

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
        if (distanceToHome > 18)
        {
            // Debug.Log(distancePCN_Fo - nu);
            return true;
        }
        else return false;
    }

    public bool HomeGoalKeeperDis()
    {
        distanceToHome = Vector3.Distance(HomeGoal.transform.position, Goalkeeper.transform.position);

        //  distans < 18
        if (distanceToHome > 18)
        {
            // Debug.Log(distancePCN_Fo - nu);
            return true;
        }
        else return false;
    }

    public bool ScoorRightGool()
        {
            if (EnemyGoal == RightGoal) { return true; }

            else return false;
        }


    public void Goal()
    {
        Agent.transform.position = initialPos;
        fsm.SetBool("SeeFootball", false);
        fsm.SetBool("FootballWith", false);
        fsm.SetBool("Home", false);
        fsm.SetBool("Attack", false);
        Debug.Log("Goal by:" + Agent + "Goal loction : " + EnemyGoal);


        // PCN Home goal Right
        //      enemy Goal Left 
    }
}




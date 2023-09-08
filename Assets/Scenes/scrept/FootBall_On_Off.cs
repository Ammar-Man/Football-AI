using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class FootBall_On_Off : MonoBehaviour
{
   // Transform initialPos;
     Animator fsm;
    public bool isFootballOn = false;
    bool isShot = false;
    bool footballOff = false;
    Rigidbody rb, boss;
    Animator animator;
    PCN_Move PCNPlayer, Goalkeeper, BluePlayer, PCNGoalkeeper;
    //  PCN_Move player, Goalkeeper, BluePlayer, PCNGoalkeeper, PCNPlayer;

    GameObject Football;
    Vector3 initialPos;
    public bool On = false;
    public int GoalResult = 0;
   // Collision other; 
    // Start is called before the first frame update
    void Start()
    { 
        Football = GameObject.Find("Football");
        fsm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
      //  player = GameObject.FindWithTag("PlayerAI").GetComponent<PCN_Move>();
        Goalkeeper = GameObject.FindWithTag("Goalkeeper").GetComponent<PCN_Move>();
     //   PCNGoalkeeper = GameObject.FindWithTag("PCNGoalkeeper").GetComponent<PCN_Move>();
        BluePlayer = GameObject.Find("BluePlayer").GetComponent<PCN_Move>();
       PCNPlayer = GameObject.Find("PCNPlayer").GetComponent<PCN_Move>();


    }

    void Update()
    {
        GoalResult = 0;
        FootballNotWithPlayer(BluePlayer,PCNPlayer);
        FootballNotWithPlayer(PCNPlayer, BluePlayer);
    }
    private void FootballNotWithPlayer(PCN_Move name, PCN_Move other)
    {
        if (name.FootballPlayerDis(3, name.gameObject))
        {
            if (!other.FootballWith) { On = false; }

            name.FootballFalse();
            name.Agent.speed = 6.0f;
        }

    }

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name == "PCNPlayer")
        {

           // PickUpFootball(PCNPlayer);
            /*
            On = true;
            PCNPlayer.FootballSeeFalse();

            PCNPlayer.FootballTrue();
            */
            //  UnityEngine.Debug.Log("PCNPlayer with ball off seeball");
        }

        if (other.gameObject.name == "BluePlayer")
        {

           // PickUpFootball(BluePlayer);
            //  On = true;
            // BluePlayer.FootballSeeFalse();

            //BluePlayer.FootballTrue();

            //UnityEngine.Debug.Log("BluePlayer with ball off seeball");
        }

        // Football Collision with Goalkeeper
        if (other.gameObject.name == "BlueKeeper")
        {
           if (On) return;
           // PickUpFootball(BluePlayer);
            PickUpFootballFromKeeper(BluePlayer);
        }

        if (other.gameObject.name == "PCNKeeper")
        {
            if (On) return;
          //  PickUpFootball(PCNPlayer);
            PickUpFootballFromKeeper(PCNPlayer);
        }

        if (PCNPlayer.Agent.name == other.gameObject.name && (!PCNPlayer.FootballPlayerDis(5, PCNPlayer.gameObject)) && !isShot)
        {
            PickUpFootball(PCNPlayer);
        }

        if (BluePlayer.Agent.name == other.gameObject.name && (!BluePlayer.FootballPlayerDis(5, BluePlayer.gameObject)) && !isShot)
        {
            PickUpFootball(BluePlayer);
          
        }


        if (other.gameObject.tag == "Walls")
        {
            FootballSpon();
            PCNPlayer.Goal();
            BluePlayer.Goal();
            GoalResult = 1;
        }
    }

    public void PickUpFootball(PCN_Move name) {
        On = true;
  

       // test.SetSearchMode();
    
        name.Agent.speed = 9.0f;

        // when football is with player
        rb.isKinematic = true;
        transform.SetParent(name.gameObject.transform);
        transform.localPosition = new Vector3(0f, -0.5f, 1f);

        // StartCoroutine(shootCooldown());
        name.FootballSeeFalse();
        name.FootballTrue();
        UnityEngine.Debug.Log("PickUpFootball and name" + name.name);

        /*
        Football.transform.position = new Vector3(1, 3, -2);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        */
    }

    public void PickUpFootballFromKeeper(PCN_Move name)
    {
        On = true;
        name.Agent.speed = 9.0f;

        // when football is with player
        rb.isKinematic = false;
        // transform.SetParent(name.gameObject.transform);
        // transform.localPosition = new Vector3(0f, -0.5f, 1f);
        // Agent.SetDestination(Football.transform.position);
        //rb.AddForce(new Vector3(-20, 1, 0), ForceMode.Impulse);
       // rb.AddForce(name.transform.position, ForceMode.Impulse);

        Vector3 ball = Football.transform.position;
        Vector3 player = name.transform.position;
        Vector3 speed = ball - player;
        UnityEngine.Debug.Log("Ball = "+ ball + player + speed);
        rb.AddForce(-speed, ForceMode.Impulse);
        // rb.AddForce(new Vector3(-20, 1, 0), ForceMode.Impulse);
        // StartCoroutine(shootCooldown());
        // name.FootballSeeFalse();
        // name.FootballTrue();

    }


    public void TransformFooball (NavMeshAgent other)
    {
        rb.isKinematic = true;
        transform.SetParent(other.transform);
        transform.localPosition = new Vector3(0f, -0.5f, 1f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            FootballSpon();
            PCNPlayer.Goal();
            BluePlayer.Goal();
            GoalResult = 1;
        }
    }

    public void FootballSpon()
    {
        UnityEngine.Debug.Log("Goal");
       // rb.isKinematic = false;
        //float speed = 0;
        
        Football.transform.position = new Vector3(1, 3, -2);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    

    public void DropBall()
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        isFootballOn = false;
    }

    public void ShootBall(PCN_Move name)
    {
        UnityEngine.Debug.Log("ShootBall");
        if (!On) return;
        DropBall();
        StartCoroutine(shootCooldown());
        
        if (name.EnemyGoal.name == "RightAttack")
        {
            rb.AddForce(new Vector3(-10, 1, 0), ForceMode.Impulse);
        }

        if (name.EnemyGoal.name == "LeftAttack")
        {
            rb.AddForce(new Vector3(10, 1, 0), ForceMode.Impulse);
        }
 
    }

    IEnumerator shootCooldown()
    {
        isShot = true;
        yield return new WaitForSeconds(0.8f);
        isShot = false;
    }
}


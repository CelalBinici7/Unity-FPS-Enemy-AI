using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    //the time to wait for the search after losing the player
    private float losePlayerTimer;

    private float shootTimer;
    public GameObject bulletPrefab;
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (shootTimer > enemy.fireRate)
            {
                Shoot();
            }
            //move the enemy to a rondom position after a rondom time
            if (moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere*5));
                moveTimer = 0;
            }
        }
        else//lost sight player
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > Random.Range(3, 7))
            {
                //Change the search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

   public void Shoot()
    {
        //store reference to gun barrel
        Transform gunBarrel = enemy.gunBarrel;
        //instantiate a new  bullet 
        GameObject bullet = GameObject.Instantiate(bulletPrefab,gunBarrel.position,enemy.transform.rotation);
        //calculate the direction to the player
        Vector3 shootDirection =( enemy.Player.transform.position-gunBarrel.transform.position).normalized;
        //add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up)* shootDirection * 40f;
        Debug.Log("shoot");
        shootTimer = 0;
    }
}

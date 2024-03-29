﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;
    
    private float attackTime;

    public float attackSpeed;

    // Update is called once per frame
    void Update()
    {
        if(player != null){
            if(Vector2.Distance(transform.position, player.position) > stopDistance){
                Debug.Log(Vector2.Distance(transform.position, player.position));
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else {         
                if(Time.time >= attackTime){
                    attackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }

        }

        IEnumerator Attack() { 
                player.GetComponent<Player>().TakeDamage(damage);

                Vector2 originalPosition = transform.position;
                Vector2 targetPosition = player.position;

                float percent = 0f;
                while (percent <= 1){
                    percent += Time.deltaTime * attackSpeed;
                    float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
                    transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);
                    yield return null;

                }

            }


    }
}

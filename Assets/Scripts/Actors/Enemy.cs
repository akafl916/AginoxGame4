using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    private TextMeshPro healthText;

    public int health;
    public int damage;
    public float speed;
    public int mass;

    public float dx;
    public float dy;

    private float pH;
    private float pV;

    private double speedMag;
    private bool isAggrivated;
    private Animator animator;

    private Player player;
    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        this.gameObject.tag = "Enemy";
        healthText = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        updateText();
        this.gameObject.GetComponent<Rigidbody2D>().mass = mass;
        speed /= 5;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    private void setAnimationValues()
    {
        animator.SetFloat("Horizontal", dx/Math.Abs(dx));
        animator.SetFloat("Vertical", dy /Math.Abs(dy));
        animator.SetFloat("pHorizontal", pH/ Math.Abs(pH));
        animator.SetFloat("pVertical", pV/ Math.Abs(pV));
        if(isAggrivated)
        {
            animator.SetFloat("Speed", (float)speedMag);
        } else
        {
            animator.SetFloat("Speed", 0);
        }
        
        

    }
    private void die()
    {
        Destroy(this.gameObject);
    }

    private void updateText()
    {
        healthText.text = "HP: " + health + "\nHNR: " + damage;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
        updateText();
    }

    public void doDamage()
    {
        player.takeDamage(damage);
    }

    private void moveTowardPlayer()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = transform.position;

        dx = playerPos.x - enemyPos.x;
        dy = playerPos.y - enemyPos.y;

        if(dy <= 1)
        {
            dx *= 2;
            dy *= 2;
        }

        transform.position += new Vector3((dx) * speed * Time.deltaTime, (dy) * speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.takeDamage(damage);
        }
    }

    private void Update()
    {
        speedMag = Math.Sqrt(dx * dx + dy * dy);
        isAggrivated = Array.Exists(player.getEnemyRange(), x => x.Equals(this.gameObject.GetComponent<Collider2D>()));
        Debug.Log(speedMag);
    }

    private void FixedUpdate()
    {
        if (speedMag > 0.01)
        {
            pH = dx;
            pV = dy;
        }
        if(isAggrivated)
        {
            moveTowardPlayer();
        }
        setAnimationValues();
    }
}

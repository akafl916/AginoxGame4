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

    private bool isAggrivated;

    private Player player;
    private void Start()
    {
        this.gameObject.tag = "Enemy";
        healthText = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        updateText();
        this.gameObject.GetComponent<Rigidbody2D>().mass = mass;
        speed /= 5;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
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
        isAggrivated = Array.Exists(player.getEnemyRange(), x => x.Equals(this.gameObject.GetComponent<Collider2D>()));
    }

    private void FixedUpdate()
    {
        if(isAggrivated)
        {
            moveTowardPlayer();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int MAX_HEALTH;
    public int damage;
    public int projectileSpeed;
    public float projectileStart;

    private int health;
    public int easyRangeTime;
    public int hardRangeTime;

    private bool isAggrivated;

    private float timer = 0;
    private float easyRangeTimer = 0;
    private float hardRangeTimer = 0;

    private bool isHard;
    private int hardRotation = 17;

    private Animator animator;
    private GameObject projectilePrefab;
    private Player player;
    private GameObject BossBar;
    
    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        health = MAX_HEALTH;
        BossBar = GameObject.Find("LEVELUI").transform.GetChild(3).gameObject;
        projectilePrefab = this.gameObject.transform.GetChild(0).gameObject;
        this.gameObject.tag = "Enemy";
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    private void updateUI()
    {
        BossBar.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Health: " + health + "/" + MAX_HEALTH;
        BossBar.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Honor\n" + damage*100;
        BossBar.transform.GetChild(4).gameObject.GetComponent<Text>().text = name;
        BossBar.GetComponent<Slider>().value = (float) health / (float) MAX_HEALTH;
    }

    private void die()
    {
        BossBar.SetActive(false);
        Destroy(this.gameObject);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }

    public void doDamage()
    {
        player.takeDamage(damage);
    }

    private void attackPlayer()
    {
        if(easyRangeTimer >= easyRangeTime)
        {
            animator.SetTrigger("isAttacking");
            easyRangeAttack();
            easyRangeTimer = 0;
        }
        if (hardRangeTimer >= hardRangeTime)
        {
            if(!isHard)
            {
                animator.SetTrigger("isAttacking");
            }
            isHard = true;
        }
    }

    private void hardRangeAttack()
    {
        if ((int)timer % 3 == 0)
        {
            GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            go.transform.Rotate(new Vector3(0, 0, hardRotation += 17));
            go.transform.position += projectileStart*go.transform.up;
        }
        if(hardRangeTimer >= hardRangeTime*1.5)
        {
            isHard = false;
            hardRotation = 17;
            hardRangeTimer = 0;
            return;
        }
    }

    private void easyRangeAttack()
    {
        for(int i = 0; i < 360; i+=30)
        {
            GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            go.transform.Rotate(new Vector3(0, 0, i));
            go.transform.position += projectileStart * go.transform.up;
        }
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
        Debug.Log(health);
        updateUI();
        isAggrivated = Array.Exists(player.getBossRange(), x => x.Equals(this.gameObject.GetComponent<Collider2D>()));
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (isAggrivated)
        {
            BossBar.SetActive(true);
            easyRangeTimer += Time.deltaTime;
            hardRangeTimer += Time.deltaTime;
            attackPlayer();
        } else
        {
            BossBar.SetActive(false);
        }
        if (isHard) hardRangeAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Boss boss;

    private void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * Time.deltaTime * boss.projectileSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                Debug.Log(boss);
                player.takeDamage(boss.damage);
            }
            if (collision.gameObject.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
        }
        catch (System.Exception ex)
        {

        }
    }
}

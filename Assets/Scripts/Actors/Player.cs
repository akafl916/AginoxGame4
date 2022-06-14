using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int nextScene;

    private GameObject UI;
    private PlayerMovement movementScript;
    private Animator animator;

    public static int MAX_HEALTH = 100;
    public static int INITIAL_DAMAGE = 10;
    public static int INITIAL_ATTACK_RANGE = 1;

    private static int health = MAX_HEALTH;
    private static int damage = INITIAL_DAMAGE;
    private static int attackRange = INITIAL_ATTACK_RANGE;
    private static int runs = 0;
    private static List<Item> inventory;

    private bool isDead = false;

    private Collider2D[] overlapCircle;
    private Collider2D[] enemyRange;
    private Collider2D[] bossRange;
    private List<GameObject> enemiesInRange;
    private List<GameObject> npcsInRange;

    private void setAnimationValues()
    {
        animator.SetFloat("Horizontal", movementScript.horizontal);
        animator.SetFloat("Vertical", movementScript.vertical);
        animator.SetFloat("pHorizontal", movementScript.pHorizontal);
        animator.SetFloat("pVertical", movementScript.pVertical);
        animator.SetFloat("Speed", movementScript.speed);

    }

    private void debug()
    {
        damage = 1000;


        Debug.Log("health: " + health);
        Debug.Log("maxhealth: " + MAX_HEALTH);
        Debug.Log("damage: " + damage);
        Debug.Log("movementSpeed: " + movementScript.moveSpeed);

        string dbg = "";
        for(int i = 0; i < inventory.Count; i ++) {
            dbg += "item #" + i + " " + inventory[i] + "   ";
        }
        Debug.Log(dbg);

        string dbg1 = "";
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            dbg1 += "enemy #" + i + " " + enemiesInRange[i] + "   ";
        }
        Debug.Log(dbg1);

        string dbg2 = "";
        for (int i = 0; i < npcsInRange.Count; i++)
        {
            dbg2 += "npc #" + i + " " + npcsInRange[i] + "   ";
        }
        Debug.Log(dbg2);
    }

    private void Start()
    {
        createOverlapCircle();
        createEnemyRange();
        createBossRange();

        UI = GameObject.Find("LEVELUI");
        movementScript = this.gameObject.GetComponent<PlayerMovement>();
        animator = this.gameObject.GetComponent<Animator>();
        enemiesInRange = new List<GameObject>();
        npcsInRange = new List<GameObject>();
        if (runs++ < 1)
        {
            inventory = new List<Item>();
        }
    }

    private bool checkWin()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            GameObject winPanel = UI.transform.GetChild(4).gameObject;
            winPanel.SetActive(true);

            Color winPanelColor = winPanel.GetComponent<Image>().color;
            if (winPanelColor.a + Time.deltaTime * 3 < 1)
            {
                Debug.Log(winPanelColor);
                winPanel.GetComponent<Image>().color = new Color(0, 0, 0, winPanelColor.a + Time.deltaTime * 1.5f);
            } else
            {
                winPanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                Debug.Log("yoo");
                string scene = "";
                scene += (SceneManager.GetActiveScene().buildIndex + 1);
                scene += nextScene;
                Debug.Log(scene);
                new SceneButtonManager().toCutscene(scene);
            }
            return true;
        }
        return false;
    }

    private void die()
    {
        GameObject deathPanel = UI.transform.GetChild(8).gameObject;
        deathPanel.SetActive(true);

        Color deathPanelColor = deathPanel.GetComponent<Image>().color;
        if (deathPanelColor.a < 1)
        {
            deathPanel.GetComponent<Image>().color = new Color(0, 0, 0, deathPanelColor.a + Time.deltaTime*3);
        }
        health = MAX_HEALTH;
    }

    private void createOverlapCircle()
    {
        overlapCircle = Physics2D.OverlapCircleAll(transform.position, attackRange);
    }

    private void createEnemyRange()
    {
        enemyRange = Physics2D.OverlapCircleAll(transform.position, attackRange*4);
    }

    private void createBossRange()
    {
        bossRange = Physics2D.OverlapCircleAll(transform.position, attackRange * 10);
    }

    public Collider2D[] getEnemyRange()
    {
        return enemyRange;
    }

    public Collider2D[] getBossRange()
    {
        return bossRange;
    }

    private void updateInventory()
    {
        GameObject inv = UI.transform.GetChild(1).gameObject;
        for(int i = 2; i < 11; i++)
        {
            GameObject button = inv.transform.GetChild(i).gameObject; 
            if(inventory.Count > i-2)
            {
                if(inventory[i-2] == null)
                {
                    button.GetComponent<Image>().sprite = null;
                }
                button.GetComponent<Image>().sprite = inventory[i - 2].getSprite();
            }
        }
    }

    private void updateHUD()
    {
        GameObject HUD = UI.transform.GetChild(2).gameObject;

        HUD.GetComponent<Slider>().value = (float) health / (float) MAX_HEALTH;
        HUD.transform.GetChild(2).GetComponent<Text>().text = "Health: " + health + "/" + MAX_HEALTH;
        HUD.transform.GetChild(3).GetComponent<Text>().text = "HONOR:\n" + damage;

        HUD.transform.parent.transform.GetChild(5).GetComponent<Text>().text = "Enemies Left: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void useItem(int index)
    {
        if(index < inventory.Count)
        {
            Item item = inventory[index];
            item.effect();
            inventory.RemoveAt(index);
            UI.transform.GetChild(1).gameObject.transform.GetChild(index+2).gameObject.GetComponent<Image>().sprite = null;
        }
    }

    public void useItem(Item item)
    {
        item.effect();
        Destroy(item.gameObject);
    }

    private void addToInventory(Item item)
    {
        if(inventory.Count < 9)
        {
            inventory.Add(item);
        }
    }

    private void checkForItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        if (item != null)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (obj.GetComponent<Weapon>() == null)
                {
                    addToInventory(item);
                    Destroy(obj);
                }
                else
                {
                    useItem(item);
                }
            }
        }
    }

    private void checkForEnemy(GameObject obj)
    {
        Enemy enemy = obj.GetComponent<Enemy>();
        Boss boss = obj.GetComponent<Boss>();
        if (enemy != null)
        {
            if(Input.GetButtonDown("Attack"))
            {
                doDamage(enemy.gameObject);
            }
        }
        if(boss != null)
        {
            if (Input.GetButtonDown("Attack"))
            {
                doDamage(boss.gameObject);
            }
        }
    }

    //private void checkForNPC(GameObject obj)
    //{
    //    NPC npc = obj.GetComponent<NPC>();
    //    if (npc != null)
    //    {
    //        if (Input.GetButtonDown("Interact"))
    //        {
                
    //        }
    //    }
    //}

    public void increaseAttack(int dmg)
    {
        damage += dmg;
    }

    public void takeDamage(int damage)
    {
        if(GlobalVariables.DEBUG)
        {
            return;
        }
        if (health <= 0)
        {
            isDead = true;
        }
        if (health - damage <= MAX_HEALTH)
        {
            health -= damage;
        } else
        {
            health = MAX_HEALTH;
        }
    }

    public void doDamage(GameObject enemy)
    {
        if (enemy.GetComponent<Boss>() != null)
        {
            enemy.GetComponent<Boss>().takeDamage(damage);
        } else
        {
            enemy.GetComponent<Enemy>().takeDamage(damage);
        }
        animator.SetTrigger("isAttacking");
    }

    private void Update()
    {
        if(Input.GetButtonDown("Debug") || GlobalVariables.DEBUG)
        {
            debug();
        }

        if(isDead)
        {
            die();
            return;
        }
        if(checkWin())
        {
            return;
        }
      
        createOverlapCircle();
        createEnemyRange();
        createBossRange();

        foreach (Collider2D collider in overlapCircle)
        {
            GameObject obj = collider.gameObject;
            checkForItem(obj);
            checkForEnemy(obj);
            //checkForNPC(obj);
        }

        updateInventory();
        updateHUD();
        setAnimationValues();

    }
}
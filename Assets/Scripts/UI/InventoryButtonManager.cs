using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonManager : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void useItem(int index)
    {
        Debug.Log("buttonworking");
        player.useItem(index);
    }
}

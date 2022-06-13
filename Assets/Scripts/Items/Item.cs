using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Player player;
    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }
    
    public virtual void effect() {}
    public virtual Sprite getSprite() {
        return null;
    }

    void Update()
    {
        
    }
}
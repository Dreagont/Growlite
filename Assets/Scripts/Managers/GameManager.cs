using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public TileManager TileManager;
    void Start()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        ItemManager = GetComponent<ItemManager>();
        TileManager = GetComponent<TileManager>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

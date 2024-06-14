using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;

    public TileManager TileManager;

    public UIManager UIManager;

    public Player player;
    void Start()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        ItemManager = GetComponent<ItemManager>();
        TileManager = GetComponent<TileManager>();
        UIManager = GetComponent<UIManager>();
        DontDestroyOnLoad(gameObject);
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;

    public TileManager TileManager;

    public UIManager UIManager;

    public PlayerController player;

    public CurrencyManager currency;
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
        player = FindAnyObjectByType<PlayerController>();
        currency = GetComponent<CurrencyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

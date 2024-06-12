using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public CollectableItems[] CollectableItems;

    private Dictionary<CollectableType, CollectableItems> collectAbleDict= new Dictionary<CollectableType, CollectableItems>();

    private void Awake()
    {
        foreach (CollectableItems item in CollectableItems)
        {
            AddItem(item);
        }
    }
    private void AddItem(CollectableItems item)
    {
        if (!collectAbleDict.ContainsKey(item.type))
        {
            collectAbleDict.Add(item.type, item);
        }
    }

    public CollectableItems GetItemByType(CollectableType type)
    {
        if (collectAbleDict.ContainsKey(type)) {
            return collectAbleDict[type];
        }
        return null;
    }
}

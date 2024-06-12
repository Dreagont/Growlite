using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactAbleTile;

    [SerializeField] private Tile hiddenTile;

    [SerializeField] private Tile interactedTile;

    void Start()
    {
        foreach(var position in interactAbleTile.cellBounds.allPositionsWithin)
        {
            if (interactAbleTile.HasTile(position))
            {
                interactAbleTile.SetTile(position, hiddenTile);
            }
        }
    }

    void Update()
    {
        
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tileBase = interactAbleTile.GetTile(position);
        if (tileBase != null)
        {
            if (tileBase.name == "Interactable")
            {
                return true;
            }
        }
        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        Vector3Int newPos = new Vector3Int(position.x-1,position.y -1, position.z);
        interactAbleTile.SetTile(newPos, interactedTile);
    }
}

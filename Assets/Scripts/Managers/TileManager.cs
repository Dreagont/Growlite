using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactAbleTile;

    [SerializeField] private Tilemap HightLightTile;

    [SerializeField] private GameObject HightLightTileObject;

    [SerializeField] private GameObject interactAbleTileObject;

    [SerializeField] private Tile hiddenTile;

    [SerializeField] private Tile interactedTile;

    [SerializeField] private Tile highlightTile;

    private Vector3Int previousHighlightedCell;

    [SerializeField] private GameObject player;


    void Start()
    {
        HightLightTileObject.SetActive(true);
        interactAbleTileObject.SetActive(true);
        
        foreach(var position in interactAbleTile.cellBounds.allPositionsWithin)
        {
            if (interactAbleTile.HasTile(position))
            {
                interactAbleTile.SetTile(position, hiddenTile);
            }
        }

        foreach (var position in HightLightTile.cellBounds.allPositionsWithin)
        {
            if (HightLightTile.HasTile(position))
            {
                HightLightTile.SetTile(position, hiddenTile);
            }
        }

        previousHighlightedCell = new Vector3Int(-1, -1, -1);

    }

    void Update()
    {
        HighlightTileUnderMouse();
    }

    private void HighlightTileUnderMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = HightLightTile.WorldToCell(mouseWorldPos);

        cellPosition.z = 0;

        Vector3 playerPosition = new Vector3Int((int)player.transform.position.x, (int)player.transform.position.y, 0);

        float distance = Vector3.Distance(playerPosition, cellPosition);

        if (cellPosition != previousHighlightedCell && distance <= 5f)
        {
            HightLightTile.SetTile(previousHighlightedCell, null); 
            previousHighlightedCell = cellPosition;
            HightLightTile.SetTile(cellPosition, highlightTile); 
        }

        if (distance >5f)
        {
            HightLightTile.SetTile(previousHighlightedCell, hiddenTile);
        }
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
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = interactAbleTile.WorldToCell(mouseWorldPos);

        interactAbleTile.SetTile(cellPosition, interactedTile);
    }

    public string GetTileName(Vector3Int position)
    {
        if (interactAbleTile != null)
        {
            TileBase tile = interactAbleTile.GetTile(position);

            if (tile != null) { return tile.name; }
        }
        return "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject currentTile;
    public GameObject selectedTile;
    public GameObject defaultTile;
    public Sprite selectedTileSprite;
    public Image currentTileImage;
    public Vector2 selectedPosition;
    public List<PlacedTile> placedTiles;
    public MapGenerator mapGenerator;

    public Vector2 mousePosition;
    public Vector2 tilePosition;

    private void OnEnable()
    {
        TileButton.selectedTile += GetSelectedTile;
        TileButton.selectedSprite += GetSelectedSprite;
        ReturnPosition.selectedPosition += GetSelectedPosition;
        InputMonitor.StartTouch += GetMousePosition;
    }

    private void OnDisable()
    {
        TileButton.selectedTile += GetSelectedTile;
        TileButton.selectedSprite += GetSelectedSprite;
    }

    private void GetSelectedTile(GameObject tile)
    { 
        selectedTile = tile;
        currentTile = selectedTile;
    }

    private void GetSelectedSprite(Sprite sprite)
    {
        selectedTileSprite = sprite;
        currentTileImage.sprite = selectedTileSprite;
    }

    private void GetSelectedPosition(Vector2 pos)
    {
        selectedPosition = pos;
        PlaceTile();
    }

    private Color GetTileColor(GameObject tile)
    {
        var colorMap = mapGenerator.colorMapping;

        foreach (var entry in colorMap)
        {
            if (tile == entry.prefab)
            {
                return entry.color;
            }
        }

        return Color.red;

    }

    private void PlaceTile()
    {
        var tile = new PlacedTile();
        tile.position = selectedPosition;
        tile.tile = selectedTile;
        tile.color = GetTileColor(tile.tile);
        placedTiles.Add(tile);
        Instantiate(currentTile, new Vector3(selectedPosition.x, selectedPosition.y, transform.position.z), Quaternion.identity);
    }
    private void Start()
    {
        if (currentTile == null)
        {
            currentTile = selectedTile = defaultTile;
            currentTileImage.sprite = defaultTile.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void DeleteTile()
    {
        foreach (var tile in placedTiles)
        {
            if (selectedPosition == tile.position)
            {
                Debug.Log("DELETE");
            }
        }
    }

    private void GetMousePosition(Vector2 position, float time)
    {
        mousePosition = position;

        RaycastHit2D hit = Physics2D.BoxCast(position, new Vector2(0.01f, 0.01f), 0f, Vector2.zero);

        if (hit.collider.gameObject.layer == 7)
        {
            var tilePosition = hit.collider.gameObject.transform.position;

            if (selectedPosition.x == tilePosition.x && selectedPosition.y == tilePosition.y)
            {
                DeleteTile();
            }
        }
    }

}

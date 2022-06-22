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

    private void OnEnable()
    {
        TileButton.selectedTile += GetSelectedTile;
        TileButton.selectedSprite += GetSelectedSprite;
        ReturnPosition.selectedPosition += GetSelectedPosition;
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
}

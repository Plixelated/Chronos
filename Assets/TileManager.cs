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

    private void PlaceTile(Vector2 pos)
    {
        selectedPosition = pos;

        var tile = new PlacedTile();
        tile.position = pos;
        tile.prefab = selectedTile;
        tile.color = GetTileColor(tile.prefab);
        tile.tile = Instantiate(currentTile, new Vector3(selectedPosition.x, selectedPosition.y, transform.position.z), Quaternion.identity);
        placedTiles.Add(tile);
    }

    private void Start()
    {
        if (currentTile == null)
        {
            currentTile = selectedTile = defaultTile;
            currentTileImage.sprite = defaultTile.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void DeleteTile(Vector2 pos)
    {
        foreach (var tile in placedTiles)
        {
            if (pos == tile.position)
            {
                tile.tile.gameObject.SetActive(false);

                placedTiles.Remove(tile);
                break;
            }
        }
    }

    private void GetMousePosition(Vector2 position, float time)
    {
        mousePosition = position;

        RaycastHit2D hit = Physics2D.BoxCast(position, new Vector2(0.01f, 0.01f), 0f, Vector2.zero);

        if (hit.collider)
        {
            var selected = hit.collider.gameObject;
            Vector2 tilePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));

            if (selected.layer == 7)
            {
                DeleteTile(tilePosition);
            }
            if (selected.layer == 8)
            {

                PlaceTile(tilePosition);
            }
        }
    }

}

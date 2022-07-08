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

    public GameObject parent;

    public ParticleSystem placementParticle;
    public ParticleSystem destructionParticle;

    public Vector2 mousePosition;
    public Vector2 tilePosition;
    public Vector2Int lastPosition;
    public float swipeDelay;

    public bool canPlaceTile;



    private void OnEnable()
    {
        TileButton.selectedTile += GetSelectedTile;
        TileButton.selectedSprite += GetSelectedSprite;
        InputMonitor.EndTouch += ClearPosition;
        InputMonitor.swipingPosition += GetSwipePosition;
    }

    private void OnDisable()
    {
        TileButton.selectedTile -= GetSelectedTile;
        TileButton.selectedSprite -= GetSelectedSprite;
        InputMonitor.EndTouch -= ClearPosition;
        InputMonitor.swipingPosition -= GetSwipePosition;
    }

    private void Awake()
    {
        canPlaceTile = true;
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
        tile.tile = Instantiate(currentTile, new Vector3(selectedPosition.x, selectedPosition.y, transform.position.z), Quaternion.identity, parent.transform);
        placementParticle.transform.position = pos;
        placementParticle.Play();
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
                //tile.tile.gameObject.SetActive(false);
                destructionParticle.transform.position = pos;
                destructionParticle.Play();
                Destroy(tile.tile);
                placedTiles.Remove(tile);
                break;
            }
        }
    }

    private void GetSwipePosition(Vector2 position)
    {
        swipeToPlace(position);
    }

    private void ClearPosition(Vector2 position, float time)
    {
        lastPosition = new Vector2Int(-100,-100);
    }

    private void swipeToPlace(Vector2 position)
    {
        Vector2Int currentPosition = Vector2Int.RoundToInt(position);

        if(!currentPosition.Equals(lastPosition))
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(Mathf.Round(position.x), Mathf.Round(position.y)), new Vector2(0.01f, 0.01f), 0f, Vector2.zero);

            if (hit.collider)
            {
                var selected = hit.collider.gameObject;
                Vector2 tilePosition = new Vector2(Mathf.Round(position.x), Mathf.Round(position.y));

                if (canPlaceTile)
                {

                    if (selected.layer == 7 || selected.layer == 6)
                    {
                        if (selectedTile.tag != selected.tag)
                        {
                            DeleteTile(tilePosition);
                            PlaceTile(tilePosition);
                        }
                        else
                            DeleteTile(tilePosition);
                    }
                    if (selected.layer == 8)
                    {
                        PlaceTile(tilePosition);
                    }
                }
            }
            
            lastPosition = currentPosition;
        }
    }

}

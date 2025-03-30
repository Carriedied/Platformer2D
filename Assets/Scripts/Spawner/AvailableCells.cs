using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AvailableCells : MonoBehaviour
{
    [SerializeField] private Tilemap _groundTilemap;

    private List<Vector3> _availableCorrectCells;
    private int _coordinateZ = 0;
    private int _offsetY = 1;

    private void Awake()
    {
        _availableCorrectCells = new List<Vector3>();
        FindAvailableCorrectCells();
    }

    public int CountCorrectCells()
    {
        return _availableCorrectCells.Count;
    }

    public Vector3 GetCoordinateCorrectCell(int index)
    {
        return _availableCorrectCells[index];
    }

    public void FindAvailableCorrectCells()
    {
        BoundsInt bounds = _groundTilemap.cellBounds;
        Vector3Int position = new Vector3Int();
        Vector3Int upperPosition;
        Vector3 worldUpperPosition;

        for (int y = bounds.y; y < bounds.yMax; y++)
        {
            for (int x = bounds.x; x < bounds.xMax; x++)
            {
                position.x = x;
                position.y = y;
                position.z = _coordinateZ;

                if (_groundTilemap.GetTile(position) != null)
                {
                    upperPosition = position;
                    upperPosition.y += _offsetY;

                    if (_groundTilemap.GetTile(upperPosition) == null)
                    {
                        worldUpperPosition = _groundTilemap.GetCellCenterWorld(upperPosition);

                        _availableCorrectCells.Add(worldUpperPosition);
                    }
                }
            }
        }
    }

    public void RemoveCorrectCells(int index)
    {
        _availableCorrectCells.RemoveAt(index);
    }
}

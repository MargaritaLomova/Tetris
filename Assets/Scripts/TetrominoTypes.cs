using UnityEngine;
using UnityEngine.Tilemaps;

public enum TetrominoTypes
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}

[System.Serializable]
public struct TetrominoData
{
    public TetrominoTypes Type;
    public Tile Tile;
    public Vector2Int[] Cells { get; private set; }

    public void Init()
    {
        Cells = Data.Cells[Type];
    }
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostController : MonoBehaviour
{
    [SerializeField]
    private Tile tile;

    public Tilemap Tilemap { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }

    private BoardController board;
    private FigureController trackingFigure;

    private void Awake()
    {
        board = FindObjectOfType<BoardController>();
        trackingFigure = board.GetComponent<FigureController>();

        Tilemap = GetComponentInChildren<Tilemap>();
        Cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        foreach (var cell in Cells)
        {
            Vector3Int tilePosition = cell + Position;
            Tilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        for(int i = 0; i < Cells.Length; i++)
        {
            Cells[i] = trackingFigure.Cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int position = trackingFigure.Position;

        int currentRow = position.y;
        int bottom = -board.BoardSize.y / 2 - 1;

        board.Clear(trackingFigure);

        for(int row = currentRow; row >= bottom; row--)
        {
            position.y = row;

            if (board.IsValidPosition(trackingFigure, position))
            {
                Position = position;
            }
            else
            {
                break;
            }
        }

        board.Set(trackingFigure);
    }

    private void Set()
    {
        foreach (var cell in Cells)
        {
            Vector3Int tilePosition = cell + Position;
            Tilemap.SetTile(tilePosition, tile);
        }
    }
}
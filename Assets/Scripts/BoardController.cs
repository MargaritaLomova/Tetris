using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private Vector3Int spawnPosition;
    [SerializeField]
    private TetrominoData[] tetrominoes;

    public Vector2Int BoardSize { get; private set; }
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-BoardSize.x / 2, -BoardSize.y / 2);
            return new RectInt(position, BoardSize);
        }
    }

    private Tilemap tilemap;
    private FigureController activeFigure;
    private ScoreController scoreController;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activeFigure = GetComponent<FigureController>();
        scoreController = FindObjectOfType<ScoreController>();
        var boardSizeFromSpriteRenderer = GetComponent<SpriteRenderer>().size;
        BoardSize = new Vector2Int((int)boardSizeFromSpriteRenderer.x, (int)boardSizeFromSpriteRenderer.y);

        for (int i = 0; i < tetrominoes.Length; i++)
        {
            tetrominoes[i].Init();
        }
    }

    private void Start()
    {
        SpawnRandomFigure();
    }

    public void SpawnRandomFigure()
    {
        var randomTetrominoIndex = Random.Range(0, tetrominoes.Length);
        TetrominoData randomTetrominoData = tetrominoes[randomTetrominoIndex];

        activeFigure.Init(spawnPosition, randomTetrominoData);

        if(IsValidPosition(activeFigure, spawnPosition))
        {
            Set(activeFigure);
        }
        else
        {
            GameOver();
        }
    }

    public void Set(FigureController figure)
    {
        foreach (var cell in figure.Cells)
        {
            Vector3Int tilePosition = cell + figure.Position;
            tilemap.SetTile(tilePosition, figure.CurrentData.Tile);
        }
    }

    public void Clear(FigureController figure)
    {
        foreach (var cell in figure.Cells)
        {
            Vector3Int tilePosition = cell + figure.Position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public void ClearLines()
    {
        int row = Bounds.yMin;

        while (row < Bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }
            else
            {
                row++;
            }
        }
    }

    public bool IsValidPosition(FigureController figure, Vector3Int position)
    {
        RectInt bounds = Bounds;

        for (int i = 0; i < figure.Cells.Length; i++)
        {
            Vector3Int tilePosition = figure.Cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePosition) || tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    private void GameOver()
    {
        tilemap.ClearAllTiles();
        scoreController.ResetCurrentScore();
    }

    private void LineClear(int row)
    {
        for (int col = Bounds.xMin; col < Bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);

            scoreController.AddPoints(1);
        }

        while (row < Bounds.yMax)
        {
            for (int col = Bounds.xMin; col < Bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);
                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }

    private bool IsLineFull(int row)
    {
        for (int col = Bounds.xMin; col < Bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            if (!tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private Vector3Int spawnPosition;
    [SerializeField]
    private TetrominoData[] tetrominoes;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private Tilemap tilemap;
    private FigureController activeFigure;
    private Vector2Int boardSize;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activeFigure = GetComponent<FigureController>();
        var boardSizeFromSpriteRenderer = GetComponent<SpriteRenderer>().size;
        boardSize = new Vector2Int((int)boardSizeFromSpriteRenderer.x, (int)boardSizeFromSpriteRenderer.y);

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
        Set(activeFigure);
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
}
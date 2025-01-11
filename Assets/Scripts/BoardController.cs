using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private Vector3Int spawnPosition;
    [SerializeField]
    private TetrominoData[] tetrominoes;

    private Tilemap tilemap;
    private FigureController activeFigure;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activeFigure = GetComponent<FigureController>();

        for(int i = 0; i < tetrominoes.Length; i++)
        {
            tetrominoes[i].Init();
        }
    }

    private void Start()
    {
        SpawnRandomFigure();
    }

    private void SpawnRandomFigure()
    {
        var randomTetrominoIndex = Random.Range(0, tetrominoes.Length);
        TetrominoData randomTetrominoData = tetrominoes[randomTetrominoIndex];

        activeFigure.Init(spawnPosition, randomTetrominoData);
        Set(activeFigure);
    }

    private void Set(FigureController figure)
    {
        foreach(var cell in figure.Cells)
        {
            Vector3Int tilePosition = cell + figure.Position;
            tilemap.SetTile(tilePosition, figure.Data.Tile);
        }
    }
}
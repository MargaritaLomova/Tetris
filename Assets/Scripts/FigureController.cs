using UnityEngine;

public class FigureController : MonoBehaviour
{
    public Vector3Int Position { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public TetrominoData Data { get; private set; }

    private BoardController board;

    private void Awake()
    {
        board = FindObjectOfType<BoardController>();
    }

    public void Init(Vector3Int position, TetrominoData data)
    {
        Position = position;
        Data = data;

        if(Cells == null)
        {
            Cells = new Vector3Int[Data.Cells.Length];
        }
        for(int i = 0; i < Data.Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)Data.Cells[i];
        }
    }
}
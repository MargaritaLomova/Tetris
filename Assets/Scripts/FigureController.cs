using UnityEngine;

public class FigureController : MonoBehaviour
{
    public Vector3Int Position { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public TetrominoData CurrentData { get; private set; }

    private BoardController board;
    private float stepDelay = 1;
    private float lockDelay = 0.5f;
    private float stepTime;
    private float lockTime;
    private int rotationIndex;

    private void Awake()
    {
        board = FindObjectOfType<BoardController>();
    }

    private void Update()
    {
        board.Clear(this);

        lockTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if(Time.time >=stepTime)
        {
            Step();
        }

        board.Set(this);
    }

    public void Init(Vector3Int position, TetrominoData data)
    {
        Position = position;
        CurrentData = data;

        rotationIndex = 0;
        stepTime = Time.time + stepDelay;
        lockTime = 0f;

        if (Cells == null)
        {
            Cells = new Vector3Int[CurrentData.Cells.Length];
        }
        for (int i = 0; i < CurrentData.Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)CurrentData.Cells[i];
        }
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;

        Move(Vector2Int.down);

        if(lockTime >= lockDelay)
        {
            Lock();
        }
    }

    private void Lock()
    {
        board.Set(this);
        board.SpawnRandomFigure();
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }

        Lock();
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = new Vector3Int(Position.x + translation.x, Position.y + translation.y, Position.z);

        bool isValidPosition = board.IsValidPosition(this, newPosition);
        if (isValidPosition)
        {
            Position = newPosition;
            lockTime = 0;
        }
        return isValidPosition;
    }

    private void Rotate(int direction)
    {
        int originalRotation = rotationIndex;
        rotationIndex = Wrap(rotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);

        if (!TestWallKicks(rotationIndex, direction))
        {
            rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            Vector3 cell = Cells[i];

            int x;
            int y;

            switch (CurrentData.Type)
            {
                case TetrominoTypes.I:
                case TetrominoTypes.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;

                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }

            Cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKicksIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < CurrentData.WallKicks.GetLength(1); i++)
        {
            Vector2Int translation = CurrentData.WallKicks[wallKickIndex, i];

            if (Move(translation))
            {
                return true;
            }
        }
        return false;
    }

    private int GetWallKicksIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationIndex < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, CurrentData.WallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}
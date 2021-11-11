using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using UnityEngine;

public class PuzzlePiece
{
    private Transform _transform = null;
    public int Index { get; private set; }

    public PuzzlePiece(Transform transform, int index)
    {
        Index = index;
        _transform = transform;
    }

    public PuzzlePiece(PuzzlePiece piece)
    {
        _transform = piece._transform;
        Index = piece.Index;
    }

    public static bool operator ==(PuzzlePiece first, Transform second) => first._transform == second;
    public static bool operator !=(PuzzlePiece first, Transform second) => first._transform != second;

    //Swaps puzzle pieces
    public void Swap(PuzzlePiece obj)
    {
        Vector3 tempCoordinates = new Vector3(_transform.position.x, _transform.position.y);
        int tempIndex = Index;

        _transform.position = obj._transform.position;
        Index = obj.Index;

        obj._transform.position = tempCoordinates;
        obj.Index = tempIndex;
    }
}


public class FirstPuzzle : MonoBehaviour
{
    private List<PuzzlePiece> puzzle; //Collets all pieces

    [HideInInspector] public Transform TempPiece1; //Collets the tapped piece 
    [HideInInspector] public Transform TempPiece2; //Collets the tapped piece

    public TextMeshProUGUI VictoryText;
    private bool _won = false;
    private BoxCollider2D _collider;

    //Sets all pieces in one collection and randomize the puzzle
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

        int index = 0;
        puzzle = new List<PuzzlePiece>();
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.transform != transform)
            puzzle.Add(new PuzzlePiece(child, index++));
        }

        foreach (var piece in puzzle)
        {
            piece.Swap(puzzle[Random.Range(0, puzzle.Count)]);
        }
    }

    void Update()
    {
        //Check if can swap pieces
        if (TempPiece1 != null && TempPiece2 != null)
        {
            int index1 = -1, index2 = -1;
            for (int i = 0; i < puzzle.Count; ++i)
            {
                if (puzzle[i] == TempPiece1)
                    index1 = i;

                if (puzzle[i] == TempPiece2)
                    index2 = i;
            }

            if (index1 != -1 && index2 != -1)
                puzzle[index1].Swap(puzzle[index2]);

            TempPiece1 = TempPiece2 = null;
        }

        //Check did the player win
        for (int i = 1; i < puzzle.Count; ++i)
        {
            if (puzzle[i].Index < puzzle[i - 1].Index)
                return;
        }

        if (!_won)
        {
            VictoryText.text = WinnerPhrases.Phrase;
            _collider.enabled = true;
            _won = true;

            int openedLevels = PlayerPrefs.GetInt("openedLevels");
            if(openedLevels < SceneManager.GetActiveScene().buildIndex + 1)
                PlayerPrefs.SetInt("openedLevels", SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 13);
    }
}

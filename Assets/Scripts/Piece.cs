using UnityEngine;

public class Piece : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(GetComponentInParent<FirstPuzzle>().TempPiece1 == null)
            GetComponentInParent<FirstPuzzle>().TempPiece1 = gameObject.transform;
        else
            GetComponentInParent<FirstPuzzle>().TempPiece2 = gameObject.transform;
    }
}

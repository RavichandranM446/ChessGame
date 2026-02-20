using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Piece : MonoBehaviourPun
{
    public bool isWhite;
    public Vector2Int boardPos;

    [Header("Materials")]
    public Material normalMat;     // Original color (white/black)
    public Material selectedMat;   // Blue
    public Material movedMat;      // Green

    private Renderer rend;

    // Default piece value (Pawn = 1)
    public virtual int GetValue()
    {
        return 1;
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        // Store prefab material if missing
        if (normalMat == null)
            normalMat = rend.material;
    }

    // ==========================================
    //      SELECTION MATERIAL LOGIC
    // ==========================================

    // Highlight piece (blue)
    public void SetSelected(bool value)
    {
        if (selectedMat == null || normalMat == null)
        {
            Debug.LogWarning("Material missing on piece: " + name);
            return;
        }

        rend.material = value ? selectedMat : normalMat;
    }

    // Green highlight (moved)
    public void SetMoved()
    {
        if (movedMat != null)
            rend.material = movedMat;
    }

    // Reset back to normal after move
    public void ResetToNormal()
    {
        if (normalMat != null)
            rend.material = normalMat;
    }

    // ==========================================
    //          MOVEMENT LOGIC
    // ==========================================

    public virtual List<Vector2Int> GetAvailableMoves(Piece[,] board)
    {
        return new List<Vector2Int>();
    }

    protected bool IsInside(int x, int y)
    {
        return (x >= 0 && x < 8 && y >= 0 && y < 8);
    }

    protected bool IsEnemy(Piece[,] board, int x, int y)
    {
        return board[x, y] != null && board[x, y].isWhite != isWhite;
    }
}

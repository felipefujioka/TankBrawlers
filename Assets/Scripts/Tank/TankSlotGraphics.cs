using UnityEngine;

public class TankSlotGraphics : MonoBehaviour
{
    private TankSlot tankSlot;

    public void SetupTankSlot(TankSlot slot)
    {
        tankSlot = slot;
    }

    public void AddSlotPiece(TankPiece piece)
    {
        tankSlot.Piece = piece;
        
        piece.CancelGravity();
        piece.colliderInProps.enabled = false;
        
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;
        piece.transform.localRotation = Quaternion.identity;
        tankSlot.IsFilled = true;
    }

    public void DestroySlot()
    {
        if (tankSlot.Piece != null)
        {
            TankPiece piece = tankSlot.Piece;
            piece.EnableGravity();
            piece.colliderInProps.enabled = true;

            Vector3 centerPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 throwDirection = (piece.transform.position - centerPosition).normalized;
            Vector3 variatingDirection = new Vector3(throwDirection.x + Random.Range(-0.2f, 0.2f),
                throwDirection.y + Random.Range(-0.2f, 0.2f));
            piece.ThrowProp(variatingDirection, null, 20);
            
            tankSlot.Piece = null;
        }
    }
}

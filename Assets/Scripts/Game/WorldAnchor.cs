using UnityEngine;

[ExecuteInEditMode]
public class WorldAnchor : MonoBehaviour
{
    public enum AnchorSide { Left, Right, Top, Bottom }
    public AnchorSide side;
    public float margin = 0f;

    private void OnValidate()
    {
        Correct();
    }

    void Update()
    {
        Correct();
    }

    private void Correct()
    {
        if (Camera.main == null) return;

        float x = 0;
        float y = 0.5f;

        if (side == AnchorSide.Left) x = 0;
        if (side == AnchorSide.Right) x = 1;

        Vector3 targetPos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 10f));

        float finalX = targetPos.x;

        if (side == AnchorSide.Left) finalX += margin;
        if (side == AnchorSide.Right) finalX -= margin;

        transform.position = new Vector3(finalX, transform.position.y, transform.position.z);
    }
}
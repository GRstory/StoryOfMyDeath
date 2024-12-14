using UnityEngine;
using System.Linq;

public class DetectorForward : Detector
{
    LayerMask layerMask = LayerMask.NameToLayer("Ground");

    public override void Update(DetectorData data)
    {
        if (data.CharacterDirection == 0)
        {
            data.IsBlockedForward = false;
            return;
        }

        Vector2 boxCenter = new Vector2(data.Bound.center.x + Physics2D.defaultContactOffset / 2f, data.Bound.center.y);
        Vector2 boxSize = new Vector2(Physics2D.defaultContactOffset, data.Bound.size.y / 2);
        Vector3 direction = data.CharacterDirection > 0 ? Vector3.right : Vector3.left;

        RaycastHit2D raycastHit2D = Physics2D.BoxCastAll(boxCenter, boxSize, 0, direction, 0f, layerMask)
            .Where<RaycastHit2D>((x) => !x.collider.isTrigger)
            .OrderBy<RaycastHit2D, float>((h) => Vector2.Angle(Vector2.up, h.normal))
            .FirstOrDefault<RaycastHit2D>();

        data.IsBlockedForward = (bool)raycastHit2D;



    }
}

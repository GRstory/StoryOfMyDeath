using System.Linq;
using UnityEngine;

public class DetectorGround : Detector
{
    LayerMask layerMask = LayerMask.NameToLayer("Ground");

    public override void Update(DetectorData data)
    {
        /*Bounds bound = data.Bound;
        Vector2 vector2 = new Vector2(bound.size.x - Physics2D.defaultContactOffset, Physics2D.defaultContactOffset);

        float num1 = vector2.y / 2f;
        Vector2 pivot = new Vector2(bound.center.x, bound.center.y);
        float num2 = (num1 * 2 + Physics2D.defaultContactOffset * 2);

        Vector2 size = vector2;
        Vector2 down = Vector2.down;

        float distance = num2;

        RaycastHit2D raycastHit2D = Physics2D.BoxCastAll(pivot, size, 0f, down, distance, 1)
            .Where<RaycastHit2D>((x) => !x.collider.isTrigger && Vector2.Dot(Vector2.up, x.normal) > 0)
            .OrderBy<RaycastHit2D, float>((h)=>Vector2.Angle(Vector2.up, h.normal))
            .FirstOrDefault<RaycastHit2D>();
        data.IsGround = (bool)raycastHit2D;

        //Debug.DrawRay()

        if(data.IsGround)
        {
            data.GroundCollider = raycastHit2D.collider;
            data.GroundRigidbody = raycastHit2D.rigidbody;

            if (data.GroundRigidbody != null)
            {
                data.GroundVelocity = raycastHit2D.rigidbody.linearVelocity;
            }

            //data.PlatformEffector2D = raycastHit2D.collider.GetComponent<PlatformEffector2D>();

        }*/

        Vector2 boxCenter = new Vector2(data.Bound.center.x, data.Bound.min.y - Physics2D.defaultContactOffset / 2f);
        Vector2 boxSize = new Vector2(data.Bound.size.x, Physics2D.defaultContactOffset);

        RaycastHit2D raycastHit2D = Physics2D.BoxCastAll(boxCenter, boxSize, 0, Vector2.down, 0f, layerMask)
            .Where<RaycastHit2D>((x) => !x.collider.isTrigger && Vector2.Dot(Vector2.up, x.normal) > 0)
            .OrderBy<RaycastHit2D, float>((h) => Vector2.Angle(Vector2.up, h.normal))
            .FirstOrDefault<RaycastHit2D>();

        data.IsGround = (bool)raycastHit2D;

        //Debug.DrawRay()

        if (data.IsGround)
        {
            data.GroundCollider = raycastHit2D.collider;
            data.GroundRigidbody = raycastHit2D.rigidbody;
            data.GroundNoraml = raycastHit2D.normal;
            if (data.GroundRigidbody != null)
            {
                data.GroundVelocity = raycastHit2D.rigidbody.linearVelocity;
                //Debug.Log("GV : "+ raycastHit2D.rigidbody.name + " : " + raycastHit2D.rigidbody.linearVelocityX);
            }

            //data.PlatformEffector2D = raycastHit.collider.GetComponent<PlatformEffector2D>();
        }
    }
}

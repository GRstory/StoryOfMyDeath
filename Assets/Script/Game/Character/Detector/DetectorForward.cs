using UnityEngine;

public class DetectorForward : Detector
{
    public override void Update(DetectorData data)
    {
        if (data.CharacterDirection == 0)
        {
            data.IsObstacleForward = false;
            return;
        }

        Bounds bounds = data.Bound;
        float y = bounds.min.y;
        if (data.IsGround) y += 0.1f;
        //bounds.size -= Vector3.up * 

    }
}

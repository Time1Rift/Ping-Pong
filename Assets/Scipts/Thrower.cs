using UnityEngine;

public class Thrower
{
    public Vector3 CalculateVelosityByHeight(Vector3 start, Vector3 end, float height)
    {
        float timeToRise = CalculatTimeByHeight(height);
        float timeToFall = CalculatTimeByHeight(height + (start - end).y);

        Vector3 horizontalVelosity = end - start;
        horizontalVelosity.y = 0;
        horizontalVelosity /= (timeToRise + timeToFall);

        Vector3 verticalVelosity = -Physics.gravity * timeToRise;

        return horizontalVelosity + verticalVelosity;
    }

    private float CalculatTimeByHeight(float height)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);
        return Mathf.Sqrt((height * 2f) / gravity);
    }
}
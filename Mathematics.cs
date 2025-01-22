using Godot;
using System;

public static class Mathematics
{
    public static float GetAngleBetween(Vector3 A, Vector3 B)
    {
        float DotProd = A.Dot(B);
        float Length = A.Length() * B.Length();
        return Mathf.RadToDeg((float)Mathf.Acos(DotProd / Length));
    }
}

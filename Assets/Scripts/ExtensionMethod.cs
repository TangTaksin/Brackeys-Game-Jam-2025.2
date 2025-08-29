using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{

    public static float Remap(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

}

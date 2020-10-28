using System;
#if UNITY
using UnityEngine;
using Math = UnityEngine.Mathf;
#endif


/// <summary>
/// Easingsfs is a library of easing functions and tools.
/// <para>This is the main class of the library.</para>
/// </summary>
public static class Easingsfs {

    private const float pi = 3.14F;

    //Ease in -> Smooth start    Ease out -> Smooth stop

    #region Default easings

    public enum InterpolationMode { sinusoidal, quadratic, exponential, circular }
    public static InterpolationMode defaultMode { get; set; }

    /// <summary>
    /// Deafult EaseIn (Smooth start) function that uses the default easing method.
    /// <para>Use Easingsfs.defaultMode to change the default ease method.</para>
    /// </summary>
    /// <param name="a">Start point for the easing (0 by default).</param>
    /// <param name="b">End point for the easing (1 by default).</param>
    /// <param name="t">Percentage of the easing completed from 0 to 1.</param>
    public static float EaseIn(float a, float b, float t)
    {
        switch (defaultMode)
        {
            case InterpolationMode.quadratic:
                return Quad_EaseIn(a, b, t);
            case InterpolationMode.sinusoidal:
                return Sin_EaseIn(a, b, t);
            case InterpolationMode.exponential:
                return Expo_EaseIn(a, b, t);
            case InterpolationMode.circular:
                return Circ_EaseIn(a, b, t);
            default:
                return Quad_EaseIn(a, b, t);
        }
    }

    /// <summary>
    /// Deafult EaseOut (Smooth stop) function that uses the default easing method.
    /// <para>Use Easingsfs.defaultMode to change the default ease method.</para>
    /// </summary>
    /// <param name="a">Start point for the easing (0 by default).</param>
    /// <param name="b">End point for the easing (1 by default).</param>
    /// <param name="t">Percentage of the easing completed from 0 to 1.</param>
    public static float EaseOut(float a, float b, float t)
    {
        switch (defaultMode) {
            case InterpolationMode.quadratic:
                return Quad_EaseOut(a, b, t);
            case InterpolationMode.sinusoidal:
                return Sin_EaseOut(a, b, t);
            case InterpolationMode.exponential:
                return Expo_EaseOut(a, b, t);
            case InterpolationMode.circular:
                return Circ_EaseOut(a, b, t);
            default:
                return Quad_EaseOut(a, b, t);
        }
    }

    /// <summary>
    /// Deafult EaseInOut (Smooth start & stop) function that uses the default easing method.
    /// <para>Use Easingsfs.defaultMode to change the default ease method.</para>
    /// </summary>
    /// <param name="a">Start point for the easing (0 by default).</param>
    /// <param name="b">End point for the easing (1 by default).</param>
    /// <param name="t">Percentage of the easing completed from 0 to 1.</param>
    public static float EaseInOut(float a, float b, float t)
    {
        switch (defaultMode)
        {
            case InterpolationMode.quadratic:
                return Quad_EaseInOut(a, b, t);
            case InterpolationMode.sinusoidal:
                return Sin_EaseInOut(a, b, t);
            case InterpolationMode.exponential:
                return Expo_EaseInOut(a, b, t);
            case InterpolationMode.circular:
                return Circ_EaseInOut(a, b, t);
            default:
                return Quad_EaseInOut(a, b, t);
        }
    }

    #endregion

    #region Easing methods

    /// <summary>
    /// Linear interpolation between two points (a, b).
    /// </summary>
    /// <param name="a">Start point for the interpolation (0 by default).</param>
    /// <param name="b">End point for the interpolation (1 by default).</param>
    /// <param name="t">Percentage of the interpolation completed from 0 to 1.</param>
    public static float Linear(float a, float b, float t) {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        return a + t*(b - a);   // F(x) =  a + x(b-a) for x in (0,1)
    }

    /// <summary>
    /// Sinusoidal easing in (smooth start) between two points (a, b).
    /// </summary>
    /// <param name="a">Start point for the easing (0 by default).</param>
    /// <param name="b">End point for the easing (1 by default).</param>
    /// <param name="t">Percentage of the easing completed from 0 to 1.</param>
    public static float Sin_EaseIn(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = (float)-Math.Cos(t * pi / 2.0F) + 1.0F; // F(x) =  -cos(x*pi/2)+1 for x in (0,1)
        return (a + x * (b - a));
    }

    /// <summary>
    /// Sinusoidal easing in (smooth start) between two points (a, b).
    /// </summary>
    /// <param name="a">Start point for the easing (0 by default).</param>
    /// <param name="b">End point for the easing (1 by default).</param>
    /// <param name="t">Percentage of the easing completed from 0 to 1.</param>
    public static float Sin_EaseOut(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = (float)Math.Sin(t * pi / 2.0F); // F(x) =  (sin(x*pi/2)) for x in (0,1)
        return (a + x * (b - a));
    }


    public static float Sin_EaseInOut(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = (float)(Math.Cos(pi * (t + 1F)) + 1F) / 2F; // F(x) = (cos(pi*(x + 1)) + 1) / 2 for x in(0,1)
        return (a + x * (b - a));
    }

    //NOTE: Another function for EaseInOut using a bezier curve => plot t^2(3-2t) for x in (0,1)

    public static float Quad_EaseIn(float a, float b, float t, int power = 1)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = t;
        if (power <= 1)
            x *= t;    // F(x) = x^2 for x in (0,1)
        else 
            for (int i = 0; i < power; i++) 
                x *= t; // F(x) = x^n for x in (0,1)

        return (a + x * (b - a));
    }

    public static float Quad_EaseOut(float a, float b, float t, int power = 1)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = .0F;
        if (power <= 1)
            x = 1.0F - (1.0F + t * t - 2.0F * t); // F(x) = 1-(1-x)^2 for x in (0,1)
        else
            x =  1.0F - (float)(Math.Pow(1.0F - t, power));
        return (a + x * (b - a));
    }

    public static float Quad_EaseInOut(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = t;
        if (t <= 0.5f)
            x = 2.0f * t * t; // x <= 0,5: F(x) = 2 * x^2 in (0, 0.5)
        else
        {
            t -= 0.5f;
            x = 2.0f * t * (1.0f - t) + 0.5f; // x > 0,5: F(x) = 2x(1-x)+0,5 in (0, 0,5)
        }

        return (a + x * (b - a));
    }

    public static float Expo_EaseIn(float a, float b, float t, int power = 1)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = (float) Math.Pow (2, power * 10 * (t-1.0F)); // F(x) =  2^(10 * (x-1)) for x in (0,1)
        return (a + x * (b - a));
    }

    public static float Expo_EaseOut(float a, float b, float t, int power = 1)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = -(float)Math.Pow(2, power * -10 * t) + 1.0F; // F(x) =  -2^(-10x) + 1 for x in (0,1)
        return (a + x * (b - a));
    }

    public static float Expo_EaseInOut(float a, float b, float t, int power = 1)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = t;
        if (t <= 0.5f)
            x = 0.5F * (float)Math.Pow(2, power * 10 * (t * 2 - 1.0F)); // x <= 0,5: F(x) = 0.5*(2^(10 * (x*2-1)) in (0, 0.5)
        else
            x = 0.5F * ((float)-Math.Pow(2, power * -10 * (t * 2 - 1.0F)) + 2.0F); // x > 0,5: F(x) = 0.5*(-2^(-10 * (x*2-1))+2) in (0.5, 1)

        return (a + x * (b - a));
    }

    public static float Circ_EaseIn(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = -((float)Math.Sqrt(1 - t * t) - 1.0F); // F(x) =  -(sqrt(1 - x*x) - 1) for x in (0,1)
        return (a + x * (b - a));
    }

    public static float Circ_EaseOut(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = (float)Math.Sqrt(1 - (t-1f) * (t-1f)); // F(x) =  sqrt(1 - (x-1)*(x-1)) for x in (0,1)
        return (a + x * (b - a));
    }

    public static float Circ_EaseInOut(float a, float b, float t)
    {
        if (t > 1.0F) t = 1.0F;
        if (t < 0.0F) t = 0.0F;

        float x = t;
        if (t <= 0.5f)
            x = -0.5F * ((float)Math.Sqrt (1.0F - t * t * 4) - 1.0F); // x <= 0,5: F(x) = -0.5*(sqrt(1 - x*x*4) - 1) in (0, 0.5)
        else
            x = (float)Math.Sqrt (-(t - 0.5F) * (t - 1.5F)) + 0.5F; // x > 0,5: F(x) = 0.5*(sqrt(1 - (x-1)*(x-1)*4) +1 ) === (sqrt(-(x-0.5)*(x-1.5)) +0.5) in (0.5, 1)
        return (a + x * (b - a));
    }

    #endregion

    #region Easefx methods

    public static class Easefx {

        public static float Flip(float value, float distanceAB = 1.0F) {
            return distanceAB - value;
        }

        public static float Power(float value, float power)
        {
            return (float)Math.Pow(value, power);
        }

        public static float Mix(float a, float b, float weightB) {
            return a + weightB * (b - a);
        }

        public static float Scale(float value, float scale) {
            return value * scale;
        }

        public static float InverseScale(float value, float scale)
        {
            return value * (1 - scale);
        }

        public static float Arch(float value) {
            return value * (1 - value);
        }

        public static float Mirror(float value, float t = 1.0F)
        {
            if (value <= t / 2)
                return value * t * 2;
            else
                return (1 - value) * t * 2; 
        }

    }

    #endregion

}

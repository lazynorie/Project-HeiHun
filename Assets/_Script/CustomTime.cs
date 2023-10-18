using UnityEngine;

public static class CustomTime
{
  public static float LocalTimeScale = 1f;

  public static float DeltaTime
  {
    get
    {
      return Time.deltaTime * LocalTimeScale;
    }
  }

  public static bool isPaused {
    get
    {
      return LocalTimeScale == 0.0f;
    } 
  }

  public static float TimeScale
  {
    get { return Time.timeScale * LocalTimeScale; }
  }
}

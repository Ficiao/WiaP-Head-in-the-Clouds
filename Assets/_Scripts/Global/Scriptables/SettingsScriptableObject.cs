using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings levels")]
public class SettingsScriptableObject : ScriptableObject
{
    public float SoundLevels;

    private void OnEnable()
    {
        SoundLevels = 0.3f;
    }
}

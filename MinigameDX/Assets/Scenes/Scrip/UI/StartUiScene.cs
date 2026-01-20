using UnityEngine;
using System.Collections;

public class StartUiScene : MonoBehaviour
{
    [SerializeField] private ShrinkAndRestoreDOTween settingPanelAnimation; // group trong panel

    private IEnumerator Start()
    {
        yield return null;
        if (settingPanelAnimation != null)
            settingPanelAnimation.RestoreAll();
    }
}

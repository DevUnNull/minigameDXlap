using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ShrinkAndRestoreDOTween : MonoBehaviour
{  
    [Header("Objects")]
    [SerializeField] private List<Transform> objects = new List<Transform>();

    [Header("Tween Settings")]
    [SerializeField] private float shrinkDuration = 0.25f;
    [SerializeField] private float restoreDuration = 0.25f;
    [SerializeField] private Ease shrinkEase = Ease.InBack;
    [SerializeField] private Ease restoreEase = Ease.OutBack;

    // Lưu scale ban đầu
    private Dictionary<Transform, Vector3> originalScales = new Dictionary<Transform, Vector3>();

    private void Awake()
    {
        CacheOriginalScales();
    }

    private void CacheOriginalScales()
    {
        originalScales.Clear();

        foreach (var obj in objects)
        {
            if (obj != null && !originalScales.ContainsKey(obj))
            {
                originalScales.Add(obj, obj.localScale);
            }
        }
    }

    // 🔽 Thu nhỏ – biến mất
    public void ShrinkAll()
    {
        foreach (var obj in objects)
        {
            if (obj == null) continue;

            obj.gameObject.SetActive(true);
            obj.DOKill();

            obj.DOScale(Vector3.zero, shrinkDuration)
               .SetEase(shrinkEase);
        }
    }

    // 🔼 Trở lại bình thường
    public void RestoreAll()
    {
        foreach (var obj in objects)
        {
            if (obj == null) continue;
            if (!originalScales.ContainsKey(obj)) continue;

            obj.gameObject.SetActive(true);
            obj.DOKill();

            obj.DOScale(originalScales[obj], restoreDuration)
               .SetEase(restoreEase);
        }
    }
}

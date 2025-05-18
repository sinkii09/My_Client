using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nara.Test
{
    public class Test_Tween : NaraBehaviour
    {
        Vector2 targetPos = new Vector2(0, 0);

        [SerializeField] float duration = 1f;
        [SerializeField] bool snap = false;
        [SerializeField] Ease ease = Ease.Linear;

        [ButtonGroup("Test")]
        public void Test()
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);
            targetPos = new Vector2(x, y);
            transform.DOMove(targetPos, duration, snap).SetEase(ease);
        }

        [ButtonGroup("Test")]
        public void TestSequence()
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-5, 5);
            targetPos = new Vector2(x, y);
            Sequence sequence = DOTween.Sequence().SetLoops(-1);
            sequence.Append(transform.DOMove(targetPos, duration, snap).SetEase(ease));
            sequence.Append(transform.DOMove(Vector2.zero, duration, snap).SetEase(ease));
        }
    }
}
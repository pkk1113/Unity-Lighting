using DG.Tweening;
using UnityEngine;

/// <summary>
/// DoTween Sequene를 Inspector에서 생성해서 적용
/// </summary>
public class DotweenHelper : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public enum StepType
        {
            Move,
            LocalMove,
            Rotate,
            LocalRotate,
            Scale,
        }
        public StepType stepType;
        public Vector3 targetValue;
        public float duration;
        public Ease ease = Ease.Linear;
    }

    [SerializeField] private Step[] m_steps;
    [SerializeField] private bool m_loop;
    [SerializeField] private LoopType m_loopType = LoopType.Restart;

    private Sequence m_sequence = null;

    private void OnEnable()
    {
        m_sequence = DOTween.Sequence();
        foreach (var step in m_steps)
        {
            switch (step.stepType)
            {
                case Step.StepType.Move:
                    m_sequence.Append(transform.DOMove(step.targetValue, step.duration).SetEase(step.ease));
                    break;
                case Step.StepType.LocalMove:
                    m_sequence.Append(transform.DOLocalMove(step.targetValue, step.duration).SetEase(step.ease));
                    break;
                case Step.StepType.Rotate:
                    m_sequence.Append(transform.DORotate(step.targetValue, step.duration, RotateMode.FastBeyond360).SetEase(step.ease));
                    break;
                case Step.StepType.LocalRotate:
                    m_sequence.Append(transform.DOLocalRotate(step.targetValue, step.duration, RotateMode.FastBeyond360).SetEase(step.ease));
                    break;
                case Step.StepType.Scale:
                    m_sequence.Append(transform.DOScale(step.targetValue, step.duration).SetEase(step.ease));
                    break;
            }
        }

        m_sequence.SetLoops(m_loop ? -1 : 0, m_loopType);
        m_sequence.Play();
    }

    private void OnDisable()
    {
        m_sequence?.Rewind();
        m_sequence?.Kill();
        m_sequence = null;
    }
}

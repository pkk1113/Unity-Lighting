using UnityEngine;
using UnityEngine.UI;

public class ExplainUI : MonoBehaviour
{
    [SerializeField] private Button m_buttonClose;
    [SerializeField] private Button m_buttonOpen;

    private void Awake()
    {
        m_buttonClose.onClick.AddListener(() =>
        {
            m_buttonClose.gameObject.SetActive(false);
            m_buttonOpen.gameObject.SetActive(true);
        });

        m_buttonOpen.onClick.AddListener(() =>
        {
            m_buttonClose.gameObject.SetActive(true);
            m_buttonOpen.gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        // 기본은 열린 상태
        m_buttonClose.gameObject.SetActive(true);
        m_buttonOpen.gameObject.SetActive(false);
    }
}

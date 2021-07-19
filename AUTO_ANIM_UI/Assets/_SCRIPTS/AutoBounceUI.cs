using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
 
public class AutoBounceUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private RectTransform m_uiElement;
    private Vector3 m_initScaleValue;
    private Vector3 m_ChangeScaleValue;
    private Vector3 m_GrowScaleValue;
    
    [SerializeField]
    private float m_smooth = 20f;
    [SerializeField]
    private float m_multiply = 1.2f;


    void Awake()
    {
        m_uiElement = gameObject.GetComponent<RectTransform>();
        m_initScaleValue = m_uiElement.transform.localScale;
        m_ChangeScaleValue = m_uiElement.transform.localScale;
        m_GrowScaleValue = m_ChangeScaleValue * 1.2f;
    }

    void Update() 
    {
        m_uiElement.transform.localScale = Vector3.Lerp( m_uiElement.transform.localScale, m_ChangeScaleValue, Time.deltaTime * m_smooth);  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {      
        StartCoroutine(C_OnPointEnter());
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(C_OnPointerExit());
    }

    IEnumerator C_OnPointEnter()
    {
        m_ChangeScaleValue = m_GrowScaleValue;
        yield return new WaitForSeconds(0.1f);
        m_ChangeScaleValue = m_initScaleValue;
        yield return new WaitForSeconds(0.1f);
        m_ChangeScaleValue = m_GrowScaleValue;
        yield return new WaitForSeconds(0.1f);
        yield return null;
    }

     IEnumerator C_OnPointerExit()
    {
        m_ChangeScaleValue = m_initScaleValue;   
        yield return null;
    }
}
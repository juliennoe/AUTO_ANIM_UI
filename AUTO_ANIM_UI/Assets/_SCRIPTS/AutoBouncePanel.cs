using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

 
public class AutoBouncePanel : MonoBehaviour /*, IPointerEnterHandler, IPointerExitHandler */
{
    [Header("Element UI à modifier")]
    public RectTransform m_uiElement;
    private bool m_switchOnOffButton;

    [Header("Sens de l'animation du panel")]
    public bool m_LeftRight;
    public bool m_RightLeft;
    public bool m_UpDown;
    public bool m_DownUp;

    [Header("Option de rebond et de fondu à l'apparition de l'élément")]
    public bool m_bounceOption;
    [SerializeField]
    private float m_smooth = 20f;
    public AnimationCurve m_smoothCurve;

    [Header("Taille de l'ecran")]
    public Vector2 m_ScreenSize;

    [Header("Position initial du Panel")]
    public Vector2 m_initialPosition;
    [Header("Position finale du Panel")]
    public Vector2 m_finalPosition;
    public Vector2 m_bouncePosition;
    [Header("Position évolutive du Panel (LERP POSITION)")]
    public Vector2 m_ChangePosition;

    [Header("Position positive et negative X & Y du Panel")]
    public float m_negativePosition_x;
    public float m_PositivePosition_x;

    public float m_PositivePosition_y;
    public float m_negativePosition_y;
    private float m_bounce;


    void Awake()
    {
        // ACTIVATION DE L'OPTION BOUNCE
        if (m_bounceOption)
        {
            m_bounce = 100f;
        }
        else
        {
            m_bounce = 0f;
        }

        // LUI DEFINIR A LA TAILLE DE L'ECRAN
        m_uiElement.sizeDelta = new Vector2(Screen.width, Screen.height);

        // SAUVEGARDER LA VARIABLE DE LA TAILLE DE L'ECRAN
        m_ScreenSize = m_uiElement.sizeDelta;

        // POSITIONNER LES ANCRES
        m_uiElement.offsetMin = new Vector2(0, 0);
        m_uiElement.offsetMax = new Vector2(0, 0);
        m_uiElement.anchorMax = new Vector2(1, 1);
        m_uiElement.anchorMin = new Vector2(0, 0);

        // DIVISER LA VALEUR X & Y POUR POSITIONNER SUR LE COTE DU SCREEN ET DIVISER PAR DEUX CAR RETINA
        m_negativePosition_x = -(Screen.width / 2);
        m_PositivePosition_x = (Screen.width / 2);

        m_PositivePosition_y = (Screen.height / 2);
        m_negativePosition_y = -(Screen.height / 2);

        // CHANGEMENT D'ORIENTATION DU PANEL
        if (m_LeftRight)
        {
            m_uiElement.position = new Vector2 (m_negativePosition_x, m_PositivePosition_y);
            m_bouncePosition = new Vector2(m_PositivePosition_x + m_bounce, m_PositivePosition_y);
        }

        if (m_RightLeft)
        {
            m_uiElement.position = new Vector2(m_PositivePosition_x * 3, m_PositivePosition_y);
            m_bouncePosition = new Vector2(m_PositivePosition_x - m_bounce, m_PositivePosition_y);
        }

        if (m_UpDown)
        {
            m_uiElement.position = new Vector2(m_PositivePosition_x, m_PositivePosition_y * 3);
            m_bouncePosition = new Vector2(m_PositivePosition_x, m_PositivePosition_y - m_bounce);
        }

        if (m_DownUp)
        {
            m_uiElement.position = new Vector2(m_PositivePosition_x,  m_negativePosition_y);
            m_bouncePosition = new Vector2(m_PositivePosition_x, m_PositivePosition_y + m_bounce);
        }

        // SAUVEGARDER LES POSITIONS
        m_initialPosition = m_uiElement.position;

        // INITALISE LE CHANGEMENT DE POSITION AU MEME ENDROIT QUE L'INIT POSITION
        m_ChangePosition = m_initialPosition;

        // INITIALISER LA POSITION FINAL
        m_finalPosition = new Vector2(m_PositivePosition_x, m_PositivePosition_y);

        Debug.Log(m_uiElement.sizeDelta);
        Debug.Log(m_finalPosition.x);
        Debug.Log(m_smoothCurve.keys);
    }

    void Update() 
    {
       m_uiElement.transform.position = Vector2.Lerp( m_uiElement.transform.position, m_ChangePosition, Time.deltaTime * m_smooth);  
    }

    public void OnClickButton()
    {
        m_switchOnOffButton = !m_switchOnOffButton;

        if (m_switchOnOffButton)
        {
            StartCoroutine(C_OnPointEnter());
        }
        else
        {
            StartCoroutine(C_OnPointerExit());
        }
      
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {      
        StartCoroutine(C_OnPointEnter());
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(C_OnPointerExit());
    } */

    IEnumerator C_OnPointEnter()
    {
        m_ChangePosition = m_finalPosition;
        yield return new WaitForSeconds(0.1f);
        m_ChangePosition = m_bouncePosition;
        yield return new WaitForSeconds(0.1f);
        m_ChangePosition = m_finalPosition;
        yield return null;
    }

     IEnumerator C_OnPointerExit()
     {
        m_ChangePosition = m_bouncePosition;
        yield return new WaitForSeconds(0.1f);
        m_ChangePosition = m_initialPosition;   
        yield return null;
     }
}
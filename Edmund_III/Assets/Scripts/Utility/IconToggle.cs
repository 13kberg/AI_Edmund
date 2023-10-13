using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IconToggle : MonoBehaviour
{
    
    public Sprite m_iconTrue;
    public Sprite m_iconFalse;

    public bool m_defultIconState = true;

    Image m_image;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        m_image.sprite = (m_defultIconState) ? m_iconTrue : m_iconTrue;

    }

    public void ToggleIcon(bool state)
    {
        if (!m_image || !m_iconTrue || !m_iconFalse)
        {
            Debug.Log("WARNING: ICONTOGGLE MISSING ICONTRUE OR ICONFALSE");
            return;
        }

        m_image.sprite = (state) ? m_iconTrue : m_iconFalse;
    }
}

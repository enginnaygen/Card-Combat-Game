using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDamageIndicator : MonoBehaviour
{
    [SerializeField] TMP_Text damageIndicator;
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;

    RectTransform myRect;

    public TMP_Text DamageIndicator { get { return damageIndicator; } set { damageIndicator = value; } }

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        myRect.anchoredPosition += new Vector2(0f, -moveSpeed * Time.deltaTime);
    }
}

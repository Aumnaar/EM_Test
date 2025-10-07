using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    public bool _verticalParallax = true;

    public Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float _parallaxY;
    private float length, startpos;
    private float textureUnitSizeX;

    void Start()
    {
        _parallaxY = parallaxEffectMultiplier.y;
        lastCameraPosition = cameraTransform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startpos = transform.position.x;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }


    void Update()
    {
       //float temp = (cameraTransform.position.x * (1 - parallaxEffectMultiplier.x)) ;
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        if (_verticalParallax == false)
        {
            deltaMovement.y = 0;
        }
        else
        {
          
        }

        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;

        if (Mathf.Abs(transform.position.x - cameraTransform.position.x) > textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }

    }
}

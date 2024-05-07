using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallexEffectMultiplier;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;
    private float textureUnitSizeX;
    private BoxCollider2D boxCollider2D;
    private bool cloudsandRock = false;

    private void Start()
    {
        lastPosition = cameraTransform.position;
        if (spriteRenderer == null)
        {
            
            boxCollider2D = GetComponent<BoxCollider2D>();
            textureUnitSizeX = boxCollider2D.bounds.size.x * 0.5f + boxCollider2D.offset.x;
            cloudsandRock = true;
        }
        else 
        {
            Debug.Log("Got sprite rendere");
            Texture2D texture2D = spriteRenderer.sprite.texture;
            textureUnitSizeX = texture2D.width / spriteRenderer.sprite.pixelsPerUnit;
            cloudsandRock = false;
        } 
        
    }

    private void OnDrawGizmos()
    {
        if (!boxCollider2D)
            return;
        Gizmos.color = Color.red;
        Vector3 spherePos = transform.position;
        spherePos.x += textureUnitSizeX;
        spherePos.y += boxCollider2D.offset.y;
        Gizmos.DrawSphere(spherePos, 1f);
    }
    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastPosition;
        transform.position += new Vector3(deltaMovement.x * parallexEffectMultiplier.x, deltaMovement.y * parallexEffectMultiplier.y);
        lastPosition = cameraTransform.position;

        if (!cloudsandRock && Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPosX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPosX, transform.position.y);                 
        }
        else if (cloudsandRock && Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX ) 
        {        
            float offsetPosX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            Vector3 endPos = new Vector3(cameraTransform.position.x + offsetPosX,transform.position.y,0);
            //Vector3 startPos = new Vector3(transform.position.x, 0 ,0);
            transform.position = Vector3.Lerp(transform.position, endPos, 0.4f);
            //transform.position = new Vector3(cameraTransform.position.x + offsetPosX, transform.position.y);
        }
    }
}

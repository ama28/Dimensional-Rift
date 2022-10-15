using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Being
{
    public Vector2Int coordinates; //within the grid, the bottom left square
    public Vector2Int size;
    public bool collidable;

    private SpriteRenderer spriteRenderer;
    private const float placeablePulseLength = 2.0f;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSelect() { //when building is chosen for placing
        gameObject.SetActive(true);
        StartCoroutine("PlacementAnim");
        spriteRenderer.sortingOrder = 2;
    }

    public void OnDeselect() { //when a different building is chosen for placing
        gameObject.SetActive(false);
        StopCoroutine("PlacementAnim");
    }

    public void OnPlace() { //when building is placed
        gameObject.SetActive(true);
        StopCoroutine("PlacementAnim");
        spriteRenderer.color = Color.white;
        spriteRenderer.sortingOrder = 1;
    }

    public void SetPlaceable(bool placeable) { //visuals to show is building is valid or not
        if(!placeable) {
            spriteRenderer.color = Color.red;
        } else {
            spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator PlacementAnim() {
        float timeElapsed = 0;
        while(true) {
            timeElapsed += Time.deltaTime;
            float alpha = (Mathf.Sin(timeElapsed * (1/placeablePulseLength) * 2 * Mathf.PI) / 2) + 0.5f;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return null;
        }
    }
}

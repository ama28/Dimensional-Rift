using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Being
{
    public Vector2Int coordinates; //within the grid, the bottom left square
    public Vector2Int size;
    public bool collidable;

    private new List<Collider2D> colliders;
    private List<SpriteRenderer> spriteRenderers;
    private const float placeablePulseLength = 2.0f;
    
    protected void Awake()
    {
        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        colliders = new List<Collider2D>(GetComponentsInChildren<Collider2D>());
    }

    protected virtual void Start() {
        transform.SetParent(GameManager.Instance.BuildingManager.transform);
        //disable colliders while placing
        colliders.ForEach(x => x.enabled = false);
    }

    public virtual void OnSelect() { //when building is chosen for placing
        gameObject.SetActive(true);
        StartCoroutine("PlacementAnim");
        spriteRenderers.ForEach(x => x.sortingOrder = 2);
    }

    public virtual void OnDeselect() { //when a different building is chosen for placing
        gameObject.SetActive(false);
        StopCoroutine("PlacementAnim");
    }

    public virtual void OnPlace() { //when building is placed
        gameObject.SetActive(true);
        //enable colliders after placing
        colliders.ForEach(x => x.enabled = true);
        StopCoroutine("PlacementAnim");
        spriteRenderers.ForEach(x => x.color = Color.white);
        spriteRenderers.ForEach(x => x.sortingOrder = 1);
        UpdateGraph();
    }

    protected virtual void UpdateGraph()
    {
        // https://arongranberg.com/astar/documentation/4_0_9_2cdcee7/graph-updates.php
        AstarPath.active.UpdateGraphs(new Bounds(transform.position,
            new Vector3(size.x + 1, size.y + 1)));
    }

    public void SetPlaceable(bool placeable) { //visuals to show is building is valid or not
        if(!placeable) {
            spriteRenderers.ForEach(x => x.color = Color.red);
        } else {
            spriteRenderers.ForEach(x => x.color = Color.white);
        }
    }

    private IEnumerator PlacementAnim() {
        float timeElapsed = 0;
        while(true) {
            timeElapsed += Time.deltaTime;
            float alpha = (Mathf.Sin(timeElapsed * (1/placeablePulseLength) * 2 * Mathf.PI) / 2) + 0.5f;
            Color newColor = new Color(spriteRenderers[0].color.r, spriteRenderers[0].color.g, spriteRenderers[0].color.b, alpha);
            spriteRenderers.ForEach(x => x.color = newColor);
            yield return null;
        }
    }
}

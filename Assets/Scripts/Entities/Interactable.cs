using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    void OnInteract(Player player);

    void OnRelease(Player player);
}

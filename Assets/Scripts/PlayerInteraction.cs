using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int maxItems;
    [SerializeField] private Image heldItemSlot;

    Interactable closestObj;
    Queue<Interactable> inventory = new Queue<Interactable>();

    void Update()
    {
        if (DialogueManager.IsActive) return;

        // Detecta el objeto mas cercano y muestra/oculta el "[E]"

        Interactable found = FindClosest();
        if (found != closestObj)
        {
            if (closestObj != null)
            {
                closestObj.ShowText(false);
            }

            closestObj = found;

            if (closestObj != null)
            {
                closestObj.ShowText(true);
            }
        }

        // Interactuar o agarrar el objeto

        if (closestObj != null && Input.GetKeyDown(KeyCode.E))
        {
            Use(closestObj);
        }

        // Soltar objeto

        if (Input.GetKeyDown(KeyCode.Q) && inventory.Count > 0)
        {
            Drop();
        }
    }

    // Busca algun objeto interactuable en un radio circular del personaje

    Interactable FindClosest()
    {
        Interactable best = null;
        float bestDist = float.MaxValue;

        foreach (var col in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            var it = col.GetComponent<Interactable>();
            if (it == null) continue;

            float d = Vector2.Distance(transform.position, col.transform.position);
            if (d < bestDist) { best = it; bestDist = d; }
        }
        return best;
    }

    // Interactua con el objeto cercano o lo agarra

    void Use(Interactable obj)
    {
        if (obj.canInteract)
        {
            obj.onInteract.Invoke();
            return;
        }

        if (inventory.Count >= maxItems)
        {
            Debug.Log("Mano ocupada");
            return;
        }

        inventory.Enqueue(obj);
        obj.ShowText(false);
        closestObj = null;
        obj.gameObject.SetActive(false);
        UpdateSlot();
    }

    // Saca el objeto del queue y lo deja en el mundo

    void Drop()
    {
        Interactable item = inventory.Dequeue();
        item.transform.position = transform.position;
        item.gameObject.SetActive(true);
        UpdateSlot();
    }

    // Muestra en el canvas el objeto que sigue en la cola (o lo oculta si no hay nada)

    void UpdateSlot()
    {
        if (inventory.Count > 0)
        {
            heldItemSlot.sprite = inventory.Peek().GetComponent<SpriteRenderer>().sprite;
            heldItemSlot.enabled = true;
        }
        else
        {
            heldItemSlot.enabled = false;
        }
    }
}
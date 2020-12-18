using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PuzzelComponent : MonoBehaviour
{

    public int MaxPos = 1;
    public List<PuzzelEllement> PuzzelElements;
    public List<GameObject> GameObjectElements;
    public bool PuzzelComplet;
    public AudioClip OncompletSound;
    [Range(0, 1)] public float Volume=1;
    public UnityEvent OnPuzzelComplet;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PuzzelElements.Count; i++)
        {
            PuzzelElements[i].Index = i;
            PuzzelElements[i].GameObjectElement = GameObjectElements[i];
            RotateElement(i,PuzzelElements[i].CurrentPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeRotateElement(int index)
    {
        for (int i = 0; i <PuzzelElements[index].Increment.Count; i++)
        {
            RotateElement(i,PuzzelElements[index].Increment[i]);
        }
        CheckIsDone();
    }

    public void RotateElement(int elementIndex, int value)
    {
        PuzzelElements[elementIndex].CurrentPos += value;
        Debug.Log("nouvelle pos est " + PuzzelElements[elementIndex].CurrentPos);
        if (PuzzelElements[elementIndex].CurrentPos < 0)
        {
            PuzzelElements[elementIndex].CurrentPos += MaxPos;
            Debug.Log("Index trop petit , passage a " + PuzzelElements[elementIndex].CurrentPos);
        }

        if (PuzzelElements[elementIndex].CurrentPos > MaxPos - 1)
        {
            PuzzelElements[elementIndex].CurrentPos -= MaxPos;
            Debug.Log("Index trop grand, passage a " + PuzzelElements[elementIndex].CurrentPos);
        }

        PuzzelElements[elementIndex].GameObjectElement.transform.localEulerAngles =
            new Vector3(0, 0,(360 / MaxPos * PuzzelElements[elementIndex].CurrentPos));
    }

    private void CheckIsDone()
    {
        bool isDone = true;
        foreach (PuzzelEllement element in PuzzelElements)
        {
            if (element.CurrentPos != 0) isDone = false;
            
        }

        if (isDone)
        {
            Debug.Log("Enigme Reussi");
            foreach (GameObject element in GameObjectElements) element.GetComponent<ButtonComponent>().SelfDestroy();
            PuzzelComplet = true;
            SoundManager.PlaySound(OncompletSound,Volume);
            OnPuzzelComplet.Invoke();
        }
    }
}

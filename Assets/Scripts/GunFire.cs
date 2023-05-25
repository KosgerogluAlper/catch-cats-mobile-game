using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [Header("RayCastSettings")]
    [SerializeField] private GameObject rayPrefab;
    [SerializeField] private GameObject rayPole;
    GameObject newRay;

    public Transform characterTransform;


    readonly float rayDistance = 250f;
    readonly float poleDistance = 1f;
    
    int poleNumber;
    float gunToObstacleDistance;


    public void Fire()
    {
        // RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance))
        {

            Quaternion rayPrefabForwardEuler = Quaternion.Euler(90, 0, (180 - transform.eulerAngles.y));
            float pipeScale = 0.4f;
            gunToObstacleDistance = Vector3.Distance(transform.position, hit.point) / 2f;
            newRay = Instantiate(rayPrefab, transform.position, rayPrefabForwardEuler);
            Vector3 scale = new(pipeScale, gunToObstacleDistance, pipeScale);
            Vector3 middlePosition = (transform.position + hit.point) / 2f;
            newRay.transform.localScale = scale;
            newRay.transform.position = middlePosition;
            poleNumber = (int)(gunToObstacleDistance / poleDistance);
            PlacePole(hit);
        }
    }

    void PlacePole(RaycastHit hit)
    {
        for (int i = 1; i <= poleNumber; i++)
        {
            Vector3 vecDistance = hit.point - characterTransform.position;
            GameObject pole = Instantiate(rayPole, characterTransform.position, Quaternion.identity);
            pole.transform.position += (vecDistance / (poleNumber + 1)) * i;
            pole.transform.SetParent(newRay.transform, true);
            pole.transform.localScale = new Vector3(0.9f, 1.5f, (gunToObstacleDistance / ((poleNumber + 1) * gunToObstacleDistance * 1.4f)));
            //      pole.transform.localRotation = Quaternion.Euler(90f, 0f, 180f);
            //  pole.transform.localPosition = new Vector3(pole.transform.localPosition.x, pole.transform.localPosition.y, 0.76f);
            pole.transform.SetLocalPositionAndRotation(new Vector3(pole.transform.localPosition.x, pole.transform.localPosition.y, 0.76f), Quaternion.Euler(90f, 0f, 180f));
        }
    }

}

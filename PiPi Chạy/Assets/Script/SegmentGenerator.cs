using System.Collections;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;

    [SerializeField] int zPos = 38;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentCount;
    void Update()
    {
        if (!creatingSegment)
        {
            creatingSegment = true;
            StartCoroutine(GenerateSegments());
        }
    }
    IEnumerator GenerateSegments()
    {
        segmentCount = Random.Range(0, 3);
        Instantiate(segment[segmentCount], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 38;
        yield return new WaitForSeconds(3);
        creatingSegment = false;
    }
}

using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;

public class BasicManager : MonoBehaviour
{
    public float roomSize = 10f;
    float birthDistance = 2f;
    [SerializeField] float width = 1.0f;
    [SerializeField] Food foodPrefab;
    [SerializeField] Agent agentPrefab;
    int numAgents = 0;
    BirthChannel birthChannel;
    AgeChannel ageChannel;
    float ageInterval = 100f;
    float nextAge = 100f;

    private void Awake()
    {
        float d = (roomSize - width) / 2;
        float y = foodPrefab.transform.position.y;
        Food r;
        for (float x = -d; x <= d; x += width) {
            for (float z = -d; z <= d; z += width) {
                if (Mathf.Abs(x) < width && Mathf.Abs(z) < width) continue;
                r = Instantiate(foodPrefab, transform.parent);
                r.transform.localPosition = new Vector3(x, y, z);
            }
        }
        ageChannel = new AgeChannel();
        SideChannelManager.RegisterSideChannel(ageChannel);
        birthChannel = new BirthChannel();
        SideChannelManager.RegisterSideChannel(birthChannel);

        int initSize = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("init_pop_size", 0);
        for (int i = 0; i < initSize; i++) {
            Agent a = AddAgent();
            float x = Random.Range(-roomSize / 2 + width, roomSize / 2 - width);
            float z = Random.Range(-roomSize / 2 + width, roomSize / 2 - width);
            a.transform.Translate(x, 0, z);
        }
        if (initSize <= 0) {
            Conceive();
        }
    }

    private void FixedUpdate()
    {
        if (Time.fixedTime > nextAge)
        {
            ageChannel.Age(Time.fixedTime);
            nextAge = Mathf.Ceil(Time.fixedTime / ageInterval) * ageInterval;
        }
    }

    public void OnDestroy()
    {
        SideChannelManager.UnregisterSideChannel(birthChannel);
        SideChannelManager.UnregisterSideChannel(ageChannel);
    }

    public void Eliminate(Agent a)
    {
        numAgents--;
        a.gameObject.SetActive(false);
        Debug.Log("Destroy " + a.Id);
        Destroy(a.gameObject);
        if (numAgents <= 0)
        {
            Conceive();
        }
    }

    private Agent AddAgent()
    {
        numAgents++;
        Agent newborn = Instantiate(agentPrefab, transform.parent);
        newborn.transform.Rotate(0, Random.Range(0, 360), 0);
        return newborn;
    }

    public void Conceive(Agent parent1 = null)
    {
        Agent newborn = AddAgent();
        if (parent1 is null)
        {
            birthChannel.Conceive(newborn.Id);
        }
        else
        {
            newborn.transform.position = parent1.transform.position
                + Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * birthDistance;
            birthChannel.Conceive(newborn.Id, parent1.Id);
        }
    }
}

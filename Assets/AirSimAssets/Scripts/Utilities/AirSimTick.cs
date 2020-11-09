using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimUnity 
{
    public class AirSimTick : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void FixedUpdate()
        {
            PInvokeWrapper.CallTick(Time.fixedDeltaTime);
        }
    }
}

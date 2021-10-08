using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Save
{
    /// <summary>
    /// Serialized class that stores data about the time
    /// </summary>
    [System.Serializable]
    public class TimeData
    {
        public int TimeTillFinish;

        public TimeData(TimeValues data)
        {
            TimeTillFinish = data.TimeTillFinished;
        }
    }
}
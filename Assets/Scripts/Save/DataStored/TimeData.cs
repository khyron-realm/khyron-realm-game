using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Save
{
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
using System;

namespace HearthStone.Library
{
    public static class WaitingPlayerCounter
    {
        private static int waitingPlayerCount = 0;
        public static int WaitingPlayerCount
        {
            get
            {
                return waitingPlayerCount;
            }
            set
            {
                waitingPlayerCount = value;
                onWaitingPlayerCountUpdated?.Invoke(waitingPlayerCount);
            }
        }

        private static event Action<int> onWaitingPlayerCountUpdated;
        public static event Action<int> OnWaitingPlayerCountUpdated { add { onWaitingPlayerCountUpdated += value; } remove { onWaitingPlayerCountUpdated -= value; } }
    }
}

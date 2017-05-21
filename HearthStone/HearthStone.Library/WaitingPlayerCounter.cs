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
                OnWaitingPlayerCountUpdated?.Invoke(waitingPlayerCount);
            }
        }

        public static event Action<int> OnWaitingPlayerCountUpdated;
    }
}

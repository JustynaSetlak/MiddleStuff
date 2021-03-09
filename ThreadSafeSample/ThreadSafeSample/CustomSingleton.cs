namespace ThreadSafeSample
{
    public class CustomSingleton
    {
        private static CustomSingleton _instance;
        private static readonly object ActionLock = new object();

        private CustomSingleton() { }

        public static CustomSingleton GetInstance()
        {
            if (_instance == null)
            {
                lock (ActionLock)
                {
                    return _instance ??= new CustomSingleton();
                }
            }

            return _instance;
        }
    }
}

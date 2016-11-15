namespace winRTtoolkit.Helpers
{
    /// <summary>
    /// Class becomes Singleton
    /// 
    /// public class NoticiasProvider : SingletonBase<NoticiasProvider>
    /// await NoticiasProvider.instance.Method();
    /// </summary>
    /// <typeparam name="Class Name"></typeparam>
    public abstract class SingletonBase<T> where T : SingletonBase<T>, new()
    {
        private static readonly T _instance = new T();

        public static T Instance
        {
            get { return _instance; }
        }
    }
}

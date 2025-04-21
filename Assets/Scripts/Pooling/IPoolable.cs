namespace Pooling
{
    public interface IPoolable
    {
        void OnReturnToPool();
        void OnGetFromPool();
    }
    
    
    public static class PoolableExtension // just to extend the class
    {
        public static string GetPoolTag(this IPoolable poolable)
        {
            return poolable.GetType().Name;
        }
    }
}
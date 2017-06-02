using System.Linq.Expressions;

namespace System.Functional
{
    public static class Func
    {
        public static Expression<Func<T>> expr<T>(Func<T> f) => () => f();
        public static Expression<Func<T1, T2>> expr<T1, T2>(Func<T1, T2> f) => (first) => f(first);
        public static Expression<Func<T1, T2, T3>> expr<T1, T2, T3>(Func<T1, T2, T3> f) => (first, second) => f(first, second);
        public static Expression<Func<T1, T2, T3, T4>> expr<T1, T2, T3, T4>(Func<T1, T2, T3, T4> f) => (first, second, third) => f(first, second, third);

        public static Func<T> Fun<T>(Func<T> f) => f;
        public static Func<T1, T2> fun<T1, T2>(Func<T1, T2> f) => f;
        public static Func<T1, T2, T3> Fun<T1, T2, T3>(Func<T1, T2, T3> f) => f;
        public static Func<T1, T2, T3, T4> Fun<T1, T2, T3, T4>(Func<T1, T2, T3, T4> f) => f;
    }
}

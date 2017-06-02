using System;
using System.Collections.Generic;
using static System.Collections.Generic.Create;
using static System.Functional.Func;
namespace InputValidation
{
    public static class Contracts
    {
        public static T As<T>(this object value) where T :class => value as T;
        public static bool NotNull<T>(this T value) where T : class => value != null;
        public static bool Null<T>(this T value) where T : class => value == null;

        static Contracts()
        {
           
        }

        static Dictionary<Type, Func<string, Exception>> _exceptions 
            = new Dictionary<Type, Func<string, Exception>>()
        {
             {typeof(ArgumentOutOfRangeException), (s) => new ArgumentOutOfRangeException(s) },
             {typeof(InvalidOperationException), (s) => new InvalidOperationException(s) },
             {typeof(ArgumentException), (s) => new ArgumentException(s) },
             {typeof(ArgumentNullException), (s) => new ArgumentNullException(s) },
             {typeof(NotImplementedException), (s) => new NotImplementedException(s) },
             {typeof(NotSupportedException), (s) => new NotSupportedException(s) }
        };

        public static Func<Exception> ResolveException<T>(string message) where T : Exception
        {
            if (_exceptions.ContainsKey(typeof(T)))
            {
                return () => _exceptions[typeof(T)](message);
            }
            return () => new Exception(message);
        }

        public static int CheckIfNatural(this int i, string message = null)
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<ArgumentOutOfRangeException>(message));
            return i;
        }

        public static int CheckIfNatural<T>(this int i, string message = null) where T : Exception
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<T>(message));
            return i;
        }

        public static int CheckIfNonNegative(this int i, string message = null)
        {
            CheckOnCondition(i, _ => _ < 0, () => new ArgumentOutOfRangeException(message));
            return i;
        }

        public static long CheckIfNatural(this long i, string message = null)
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<ArgumentOutOfRangeException>(message));
            return i;
        }

        public static long CheckIfNatural<T>(this long i, string message = null) where T : Exception
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<T>(message));
            return i;
        }

        public static long CheckIfNonNegative(this long i, string message = null)
        {
            CheckOnCondition(i, _ => _ < 0, () => new ArgumentOutOfRangeException(message));
            return i;
        }

        public static float CheckIfNatural(this float i, string message = null)
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<ArgumentOutOfRangeException>(message));
            return i;
        }

        public static float CheckIfNatural<T>(this float i, string message = null) where T : Exception
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<T>(message));
            return i;
        }

        public static float CheckIfNonNegative(this float i, string message = null)
        {
            CheckOnCondition(i, _ => _ < 0, () => new ArgumentOutOfRangeException(message));
            return i;
        } 

        public static double CheckIfNatural(this double i, string message = null)
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<ArgumentOutOfRangeException>(message));
            return i;
        }

        public static double CheckIfNatural<T>(this double i, string message = null) where T : Exception
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<T>(message));
            return i;
        }

        public static double CheckIfNonNegative(this double i, string message = null)
        {
            CheckOnCondition(i, _ => _ < 0, () => new ArgumentOutOfRangeException(message));
            return i;
        }

        public static decimal CheckIfNonNegative<T>(this decimal i, string message = null) where T : Exception
        {
            CheckOnCondition(i, _ => _ <= 0, ResolveException<T>(message));
            return i;
        }

        public static decimal CheckIfNatural(this decimal i, string message = null)
        {
            CheckOnCondition(i, _ => _ < 0, () => new ArgumentOutOfRangeException(message));
            return i;
        }

        public static IList<T> CheckIfHasNoItems<T>(this IList<T> source, string message = null)
        {
            CheckOnCondition(source, s => s.Count == 0, () => new ArgumentException(message));
            return source;
        }

        public static IList<T> CheckIfAny<T>(this IList<T> source, int count, string message = null)
        {
            CheckIfPredicateTrue(source, count, remainder => remainder == 0);
            return source;
        }

        public static IList<T> CheckIfLessOrMoreThan<T>(this IList<T> source, int count, string message = null)
        {
            CheckIfPredicateTrue(source, count, remainder => remainder != 0);
            return source;
        }

        public static IList<T> CheckIfLessThan<T>(this IList<T> source, int count, string message = null, string caller = null)
        {
            CheckIfPredicateTrue(source, count, remainder => remainder < 0);
            return source;
        }

        public static IList<T> CheckIfMoreThan<T>(this IList<T> source, int count, string message = null, string caller = null)
        {
            CheckIfPredicateTrue(source, count, remainder => remainder > 0);
            return source;
        }

        private static IList<T> CheckIfPredicateTrue<T>(this IList<T> source, int count, Func<int, bool> predicate, string message = null)
        {
            CheckOnCondition(source, s => predicate(s.Count - count), () => new ArgumentException(message));
            return source;
        }

        public static void CheckOnCondition<T>(this T obj, Func<T, bool> selector, Func<Exception> exception)
        {
            if (selector(obj))
                throw exception();
        }

        public static T CheckNotNull<T>(this T obj, string message) where T : class
        {
            if (obj.Null())
                throw new ArgumentNullException($"Provided argument '{message}' is null");
            return obj;
        }
    }
}
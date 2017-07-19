using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;

using static System.Collections.Generic.Create;
namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to create objects without direct constructor invocation.
    /// </summary>
    public struct LinqExpressionCtor
    {
        private Type _type;
        private object[] _args;

        /// <summary>
        /// Intializes a new instance of <see cref="LinqExpressionCtor"/>
        /// </summary>
        /// <param name="args">Arguments to pass to target type constructor</param>
        public LinqExpressionCtor(Type type, object[] args)
        {
            _type = type;
            _args = args;
        }

        public LinqExpressionCtor(Type type) : this(type, array<object>())
        {

        }

        public delegate object ObjectActivator(params object[] args);

        /// <summary>
        /// Initializes a new instance of required type.
        /// </summary>
        /// <returns>New instance of required type</returns>
        public object Value()
        {
            ConstructorInfo ctor = _type.GetConstructors().First();
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof(ObjectActivator), newExp, param);

            //compile it
            ObjectActivator compiled = (ObjectActivator)lambda.Compile();
            return compiled(_args);
        }
    }
}

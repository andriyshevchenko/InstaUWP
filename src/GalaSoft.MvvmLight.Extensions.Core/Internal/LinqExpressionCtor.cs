using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using InputValidation;


using static System.Functional.Func;
using static System.Functional.FlowControl;
using static System.Collections.Generic.Create;
using System.IO;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to create objects without direct constructor invocation, 
    /// only knowing its type and constructor arguments.
    /// Warning! To use <see cref="LinqExpressionCtor"/> you have to know 
    /// exact position of required type constructor in Type.GetConstructors() array.
    /// For example, <see cref="String"/> has 8 constructors:
    /// 
    ///   Void .ctor(Char*)
    ///   Void .ctor(Char*, Int32, Int32)
    ///   Void .ctor(SByte*)
    ///   Void .ctor(SByte*, Int32, Int32)
    ///   Void .ctor(SByte*, Int32, Int32, System.Text.Encoding)
    ///   Void .ctor(Char[], Int32, Int32)
    ///   Void .ctor(Char[])
    ///   Void .ctor(Char, Int32)
    ///   
    /// And you want to create a <see cref="String"/> from char array,
    /// so the ctorPosition will be 6 (in zero-based index).
    /// Example usage:
    ///
    ///     var ctor = new LinqExpressionCtor(typeof(string), 6, new char[2]{'1', '2'}); 
    ///     var str = ctor.Value(); 
    ///     
    /// str is equal to "12" now.
    /// </summary>
    public struct LinqExpressionCtor
    {
        private int _constructorNumber;
        private Type _type;
        private object[] _args;

        /// <summary>
        /// Intializes a new instance of <see cref="LinqExpressionCtor"/>
        /// </summary>
        /// <param name="type">Type of object to create</param>
        /// <param name="ctorPosition">Position of constructor in Type.GetConstructors() array</param>
        /// <param name="args">Constructor arguments</param>
        public LinqExpressionCtor(Type type, int ctorPosition, params object[] args)
        {
            _type = type;
            _constructorNumber = ctorPosition
                .CheckIfNonNegative("Position of constructor in Type.GetConstructors() array");
            _args = args;
        }

        /// <summary>
        /// Intializes a new instance of <see cref="LinqExpressionCtor"/> running a default constructor.
        /// </summary>
        /// <param name="type">Type of object to create</param>
        /// <param name="ctorPosition">Position of constructor in Type.GetConstructors() array</param>
        public LinqExpressionCtor(Type type, int ctorPosition = 0) : this(type, ctorPosition, array<object>())
        {

        }

        public delegate object ObjectActivator(params object[] args);

        /// <summary>
        /// Initializes a new instance of required type.
        /// </summary>
        /// <returns>New instance of required type</returns>
        public object Value()
        {
            ConstructorInfo[] constructorInfo = _type.GetConstructors();
            
            ConstructorInfo ctor = constructorInfo.ElementAt(_constructorNumber);
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

using System;
using System.Reflection;
using System.Linq.Expressions;
using InputValidation;
using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to create objects without direct constructor invocation,  
    /// only knowing its type and constructor arguments.
    /// Significantly faster than Activator.CreateInstance()
    /// Warning! To use <see cref="FastObjectCreation"/> you have to know 
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
    ///     var ctor = new FastObjectCreation(typeof(string), 6, new char[2]{'1', '2'}); 
    ///     var str = ctor.Value(); 
    ///     
    /// str is equal to "12" now.
    /// </summary>
    public struct FastObjectCreation
    {
        private int _constructorNumber;
        private Type _type;
        private object[] _args;

        /// <summary>
        /// Intializes a new instance of <see cref="FastObjectCreation"/>
        /// </summary>
        /// <param name="type">Type of object to create</param>
        /// <param name="ctorPosition">Position of constructor in Type.GetConstructors() array</param>
        /// <param name="args">Constructor arguments</param>
        public FastObjectCreation(Type type, int ctorPosition, params object[] args)
        {
            _type = type;
            _constructorNumber = ctorPosition
                .CheckIfNonNegative("Position of constructor in Type.GetConstructors() array");
            _args = args;
        }

        /// <summary>
        /// Intializes a new instance of <see cref="FastObjectCreation"/> running a default constructor.
        /// </summary>
        /// <param name="type">Type of object to create</param>
        /// <param name="ctorPosition">Position of constructor in Type.GetConstructors() array</param>
        public FastObjectCreation(Type type, int ctorPosition = 0) : this(type, ctorPosition, array<object>())
        {

        }

        public delegate object ObjectActivator(params object[] args);

        /// <summary>
        /// Initializes a new instance of required type.
        /// </summary>
        /// <returns>New instance of required type</returns>
        public object Value()
        {
            ConstructorInfo constructor = _type.GetConstructors()[_constructorNumber];
            ParameterInfo[] paramsInfo = constructor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                argsExp[i] =
                    Expression.Convert(
                        Expression.ArrayIndex(param, Expression.Constant(i)),
                        paramsInfo[i].ParameterType
                    );
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(constructor, argsExp);

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

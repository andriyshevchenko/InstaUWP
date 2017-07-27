﻿using Cactoos;
using System.Reflection;
using System;

namespace GalaSoft.MvvmLight.Extensions
{
    public struct AssemblyOfType : IScalar<Assembly>
    {
        private Type _type;

        public AssemblyOfType(Type type)
        {
            _type = type;
        }

        public Assembly Value()
        {
            return _type.GetTypeInfo().Assembly;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Functions
{
    public static class Activator
    {
        /// <summary>
        /// Delegate for activating instances of objects.
        /// </summary>
        /// <param name="args">Arguments of the construtor.</param>
        /// <returns>A new instance of the object.</returns>
        public delegate Object InstanceActivator(params object[] args);

        /// <summary>
        /// Compiles an instance activator.
        /// </summary>
        /// <param name="objectType">Type of the object to be created.</param>
        /// <returns>An compiled instance activator to be used for activating new instances of the object.</returns>
        public static InstanceActivator CompileDefaultConstructorToInstanceActivator(Type objectType)
        {
            ConstructorInfo foundConstructor = default(ConstructorInfo);

            foreach (ConstructorInfo constructorInfo in objectType.GetTypeInfo().DeclaredConstructors)
            {
                if(constructorInfo.GetParameters().Length == 0)
                {
                    foundConstructor = constructorInfo;
                }
            }

            ParameterInfo[] paramsInfo = foundConstructor.GetParameters();

            ParameterExpression parameter = Expression.Parameter(typeof(object[]), "args");

            Expression[] argumentExpressions = new Expression[paramsInfo.Length];

            NewExpression newExpression = Expression.New(foundConstructor, argumentExpressions);
            LambdaExpression lambda = Expression.Lambda(typeof(InstanceActivator), newExpression, parameter);

            return (InstanceActivator)lambda.Compile();
        }
    }
}

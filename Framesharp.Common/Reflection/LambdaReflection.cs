using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framesharp.Common.Reflection
{
    public class LambdaReflection
    {
        /// <summary>
        /// Dada uma expressão linq, compilo uma expressão que retorna o valor da propriedade indicada.
        /// </summary>
        /// <typeparam name="T">Tipo da classe de domínio a ser lida</typeparam>
        /// <param name="rootObject">Instância do objeto a ser lido</param>
        /// <param name="targetExpression">Expressão completa de acesso a propriedade</param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(T rootObject, Expression targetExpression)
        {
            if (rootObject == null) return rootObject;

            targetExpression = GetMemberExpression(targetExpression);

            ParameterExpression parameterExpression = GetParameterExpression(targetExpression);

            // Cria uma expressão para obter o valor de uma propriedade
            var read = Expression.Lambda
                (
                    Expression.Property((targetExpression as MemberExpression).Expression, (targetExpression as MemberExpression).Member as PropertyInfo),
                    parameterExpression
                );

            // Retorna o valor da propriedade obtida dinamicamente
            return read.Compile().DynamicInvoke(rootObject);
        }

        /// <summary>
        /// Dada uma expressão linq, compilo uma expressão que atribui o valor da propriedade indicada.
        /// </summary>
        /// <typeparam name="T">Tipo da classe de domínio a ser preenchida</typeparam>
        /// <param name="rootObject">Instância do objeto a ser preenchido</param>
        /// <param name="targetExpression">Expressão completa de acesso a propriedade</param>
        /// <param name="propertyValue">Valor a ser atribuido à propriedade</param>
        public static T SetPropertyValue<T>(T rootObject, Expression targetExpression,
                                                          object propertyValue) where T : class
        {
            return SetPropertyValue(rootObject,
                                     GetParameterExpression(targetExpression),
                                     targetExpression,
                                     propertyValue);
        }

        /// <summary>
        /// Dada uma expressão linq, compilo uma expressão que atribui o valor da propriedade indicada.
        /// </summary>
        /// <typeparam name="T">Tipo da classe de domínio a ser preenchida</typeparam>
        /// <param name="rootObject">Instância do objeto a ser preenchido</param>
        /// <param name="parameterExpression">Parametro principal da expressão lambda</param>
        /// <param name="targetExpression">Expressão completa de acesso a propriedade</param>
        /// <param name="propertyValue">Valor a ser atribuido à propriedade</param>
        public static T SetPropertyValue<T>(T rootObject,
                                               ParameterExpression parameterExpression,
                                               Expression targetExpression,
                                               object propertyValue) where T : class
        {
            rootObject = rootObject ?? Activator.CreateInstance<T>();

            targetExpression = GetMemberExpression(targetExpression);

            ParameterExpression valueParameterExpression = Expression.Parameter(targetExpression.Type);

            if (targetExpression.Type.IsValueType && propertyValue.GetType() == typeof(String))
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(targetExpression.Type);

                propertyValue = typeConverter.ConvertFromInvariantString(propertyValue.ToString());
            }

            var assign = Expression.Lambda
                        (
                            Expression.Assign(targetExpression, Expression.Convert(valueParameterExpression, targetExpression.Type)),
                            parameterExpression,
                            valueParameterExpression
                        );

            try
            {
                assign.Compile().DynamicInvoke(rootObject, propertyValue);
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException is NullReferenceException)
                    SetPropertyValue(rootObject,
                                     parameterExpression,
                                     (targetExpression as MemberExpression).Expression,
                                     Activator.CreateInstance((targetExpression as MemberExpression).Expression.Type));
            }
            finally
            {
                assign.Compile().DynamicInvoke(rootObject, propertyValue);
            }

            return rootObject;
        }

        /// <summary>
        /// Encontra o parametro raiz da operação lambda que representa o objeto ao qual está
        ///  sendo atribuida a expressão
        /// </summary>
        /// <param name="expression">Expressão lambda de acesso a membro</param>
        /// <returns></returns>
        private static ParameterExpression GetParameterExpression(Expression expression)
        {
            Expression result = GetMemberExpression(expression);

            while (result.NodeType == ExpressionType.MemberAccess)
            {
                result = ((MemberExpression)result).Expression;
            }

            return result as ParameterExpression;
        }

        /// <summary>
        /// Converte a expressão lambda para um MemberExpression que possa ser analisado 
        ///  em futuras operações
        /// </summary>
        /// <param name="expression">Instância de uma expressão lambda de acesso a membro de classe</param>
        /// <returns></returns>
        private static Expression GetMemberExpression(Expression expression)
        {
            Expression result = (expression as LambdaExpression) == null ? expression : (expression as LambdaExpression).Body;

            result = result is UnaryExpression
                                   ? ((UnaryExpression)result).Operand
                                   : result;

            return result;
        }
    }
}

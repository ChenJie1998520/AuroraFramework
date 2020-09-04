using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace AuroraFramework.ComponentModel
{
    /// <summary>
    /// 实现类型属性分别编辑的通用类型转换器
    /// 使用注意：使用的时候应该先从这个通用类型转换器中继承一个自己的类型转换器，泛型T应该是类型转换器的目标类型
    /// </summary> 
    /// <typeparam name="T">转换器中元素的类型</typeparam> 
    public class AuroraTypeConverter<T> : TypeConverter where T : new()
    {
        /// <summary>
        /// 确定此转换器是否可以将指定的源类型的对象转换为该转换器的本机类型。
        /// </summary>
        /// <param name="context">
        ///   一个格式化程序的上下文。
        ///    此对象可以用于获取有关从中调用此转换器的环境的其他信息。
        ///    这可能是 <see langword="null" />, ，因此应始终对其进行检查。
        ///    同样，上下文对象上的属性也可能返回 <see langword="null" />。
        /// </param>
        /// <param name="sourceType">您想要将从转换的类型。</param>
        /// <returns>此方法返回 <see langword="true" /> 如果此对象可以执行转换。</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        /// <summary>
        /// 使用指定的上下文和区域性信息将给定对象转换为此转换器的类型
        /// </summary>
        /// <param name="context">一个提供格式上下文的 <see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
        /// <param name="culture">CultureInfo 要用作当前区域性</param>
        /// <param name="value">要转换的 <see cref="System.Object"/></param>
        /// <returns>转换后的值</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //字符串类似：ClassName { A=a, B=b, C=c } 
            string strValue = value as string;
            if (strValue == null)
            {
                return base.ConvertFrom(context, culture, value);
            }

            strValue = strValue.Trim();
            if (strValue.Length == 0)
            {
                return null;
            }

            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }

            //char sepChar = culture.TextInfo.ListSeparator[0]; 
            char sepChar = '|';//分隔符
            Type type = typeof(T);

            //1、去掉"ClassName { "和" }"两部分 
            string withStart = type.Name + " { "; string withEnd = " }";
            if (strValue.StartsWith(withStart) && strValue.EndsWith(withEnd))
            {
                strValue = strValue.Substring(withStart.Length, strValue.Length - withStart.Length - withEnd.Length);
            }

            //2、分割属性值 
            string[] strArray = strValue.Split(new char[] { sepChar });

            //3、做成属性集合表 
            Hashtable properties = new Hashtable();
            for (int i = 0; i < strArray.Length; i++)
            {
                string str = strArray[i].Trim();
                int index = str.IndexOf('=');
                if (index != -1)
                {
                    string propName = str.Substring(0, index);
                    string propValue = str.Substring(index + 1, str.Length - index - 1);
                    PropertyInfo pi = type.GetProperty(propName);
                    if (pi != null)
                    {
                        //该属性对应类型的类型转换器 
                        TypeConverter converter = TypeDescriptor.GetConverter(pi.PropertyType);
                        //转换器判断
                        if (converter.GetType() == typeof(ArrayConverter) || converter.GetType() == typeof(CollectionConverter))
                        {
                            continue;
                        }

                        properties.Add(propName, converter.ConvertFromString(propValue));
                    }
                }
            }
            return this.CreateInstance(context, properties);
        }

        /// <summary>
        /// 获取一个值，该值指示此转换器是否可以将对象转换为给定的目标类型使用的上下文。
        /// </summary>
        /// <param name="context"><see langword="ITypeDescriptorContext" /> 提供格式上下文的对象。</param>
        /// <param name="destinationType">一个 <see cref="T:System.Type" /> 对象，表示要转换的类型。</param>
        /// <returns>此方法返回 <see langword="true" /> 如果该转换器能够执行转换; 否则为 <see langword="false" />。
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        /// <summary>
        /// 使用指定的上下文和区域性信息将给定的值对象转换为指定的类型
        /// </summary>
        /// <param name="context">一个提供格式上下文的 <see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
        /// <param name="culture">用作当前区域性的 <see cref="System.Globalization.CultureInfo"/></param>
        /// <param name="value">要转换的 <see cref="System.Object"/></param>
        /// <param name="destinationType"><paramref name="value"/> 参数要转换到的 <see cref="System.Type"/></param>
        /// <returns>表示转换的 value 的 <see cref="System.Object"/></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }
            if (value is T)
            {
                if (destinationType == typeof(string))
                {
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    //string separator = culture.TextInfo.ListSeparator + " "; 
                    string separator = " | ";//分隔符
                    StringBuilder sb = new StringBuilder();
                    Type type = value.GetType();
                    sb.Append(type.Name + " { ");
                    PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    for (int i = 0; i < pis.Length; i++)
                    {
                        if (!pis[i].CanRead)
                            continue;
                        Type typeProp = pis[i].PropertyType;
                        string nameProp = pis[i].Name;
                        object valueProp = pis[i].GetValue(value, null);
                        TypeConverter converter = TypeDescriptor.GetConverter(typeProp);

                        //转换器判断
                        if(converter.GetType() == typeof(ArrayConverter) || converter.GetType() == typeof(CollectionConverter))
                        {
                            continue;
                        }
                        sb.AppendFormat("{0}={1}" + separator, nameProp, converter.ConvertToString(context, valueProp));
                    }
                    string strContent = sb.ToString();
                    if (strContent.EndsWith(separator))
                        strContent = strContent.Substring(0, strContent.Length - separator.Length); strContent += " }";
                    return strContent;
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    ConstructorInfo constructor = typeof(T).GetConstructor(new Type[0]);
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[0], false);
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// 改变数值时重新生成对象
        /// </summary>
        /// <param name="context">一个提供格式上下文的 <see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
        /// <param name="propertyValues">表示键/值对的非通用集合</param>
        /// <returns></returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException(nameof(propertyValues));
            }

            Type type = typeof(T);
            ConstructorInfo ci = type.GetConstructor(new Type[0]);
            if (ci == null) return null;

            //调用默认的构造函数构造实例 
            object obj = ci.Invoke(new object[0]);
            //设置属性
            PropertyInfo[] pis = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            object propValue = null;
            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].CanWrite)
                    continue;
                propValue = propertyValues[pis[i].Name];

                if (propValue != null)
                    pis[i].SetValue(obj, propValue, null);
            }
            return obj;
        }


        /// <summary>
        /// 获取支持的创建实例
        /// </summary>
        /// <param name="context">一个提供格式上下文的 <see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
        /// <returns>返回更改此对象的值是否要求调用 CreateInstance 方法来创建新值</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;//返回更改此对象的值是否要求调用 CreateInstance 方法来创建新值。 
        }

        /// <summary>
        /// 使用指定的上下文和特性返回由 <paramref name="value"/> 参数指定的数组类型的属性的集合
        /// </summary>
        /// <param name="context">一个提供格式上下文的 <see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
        /// <param name="value">一个 <see cref="System.Object"/> 指定要为其获取属性的数组类型</param>
        /// <param name="attributes"> 用作筛选器的 <see cref="System.Attribute"/> 类型数组</param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            //属性依照在类型中声明的顺序显示 
            Type type = value.GetType();
            PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            string[] names = new string[pis.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = pis[i].Name;
            }
            return TypeDescriptor.GetProperties(typeof(T), attributes).Sort(names);
        }

        /// <summary>
        /// 获取支持的属性
        /// </summary>
        /// <param name="context">一个提供格式上下文的 <see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}


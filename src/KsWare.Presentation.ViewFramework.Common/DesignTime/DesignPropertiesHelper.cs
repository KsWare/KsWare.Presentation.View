using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace KsWare.Presentation.ViewFramework.DesignTime {

	internal class DesignPropertiesHelper {

		public static DependencyProperty GetDependencyProperty(DependencyObject obj, string propertyName) 
			=> TryGetDependencyProperty(obj, propertyName, out var dt, out _) ? dt : null;

		public static bool TryGetDependencyProperty(object obj, string propertyName, out DependencyProperty dp) 
			=> TryGetDependencyProperty(obj, propertyName, out dp, out _);


		public static bool TryGetDependencyProperty(object obj, string propertyName, out DependencyProperty dp, out DependencyObject d) {
			if (!(obj is DependencyObject d0)) {
				dp = null;
				d = null;
				return false;
			}
			{
				if (!propertyName.EndsWith("Property")) propertyName += "Property";
				var fi = obj.GetType().GetField(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				var value = fi?.GetValue(obj);
				dp = (DependencyProperty)value;
				d = d0;
				return dp != null;
			}
		}

		public static TypeConverter GetTypeConverter(Type type) => TryGetTypeConverter(type, out var tc) ? tc : null;

		public static bool TryGetTypeConverter(Type type, out TypeConverter typeConverter) {
			// typeof(System.Windows.Thickness);
			// typeof(System.Windows.ThicknessConverter);

			var a = type.GetCustomAttribute<TypeConverterAttribute>();
			if (a != null) {
				var converterType = Type.GetType(a.ConverterTypeName,true);
				typeConverter = (TypeConverter)Activator.CreateInstance(converterType);
				return true;
			}

			{
				typeConverter = null;
				var converterName = type.FullName + "Converter";
				var converterType = type.Assembly.GetType(converterName);
				if (converterType == null) return false;
				typeConverter = (TypeConverter)Activator.CreateInstance(converterType);
				return true;
			}
		}

		public static object Convert(object value, Type type, ITypeDescriptorContext context, TypeConverter typeConverter = null) {
			if (value == null) {
				return null;
			}

			if (type.IsEnum) {
				var v = Enum.Parse(type, (string)value);
				return v;
			}

			typeConverter = typeConverter ?? context?.PropertyDescriptor?.Converter ?? GetTypeConverter(type); //TODO GetTypeConverter still required?
			if (typeConverter != null) {
				object v;
				if (value is string s) v = typeConverter.ConvertFromString(context, CultureInfo.InvariantCulture, s);
				else v = typeConverter.ConvertFrom(value);
				return v;
			}

			{
				var v = System.Convert.ChangeType(value, type,CultureInfo.InvariantCulture);
				return v;
			}
		}

		public static bool TryGetPropertyInfo(Type type, string propertyName, out PropertyInfo propertyInfo) {
			propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return propertyInfo != null;
		}

		public static bool TryGetFieldInfo(Type type, string propertyName, out FieldInfo fieldInfo) {
			fieldInfo = type.GetField(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return fieldInfo != null;
		}

		public static void SetValue(object o, string propName, object value) {
			var tdc=new TypeDescriptorContext(o, propName);
			{
				try {
					var v = Convert(value, tdc.PropertyDescriptor.PropertyType, tdc);
					tdc.PropertyDescriptor.SetValue(o, v);
				}
				catch (Exception ex) {
					
				}

				return;
			}


			if (TryGetDependencyProperty(o, propName, out var dp)) {
				var v = Convert(value, dp.PropertyType, tdc);
				((DependencyObject)o).SetValue(dp, v);
				return;
			}
			if(TryGetPropertyInfo(o.GetType(),propName, out var pi))
			{
				var v = Convert(value, pi.PropertyType, tdc);
				pi.SetValue(o, v);
				return;
			}
			if (TryGetFieldInfo(o.GetType(), propName, out var fi)) {
				var v = Convert(value, fi.FieldType, tdc);
				fi.SetValue(o, v);
				return;
			}
			throw new NotSupportedException();
		}

		public static TypeConverter GetTypeConverter(MemberInfo propertyInfo) {
			var converterTypeName = propertyInfo.GetCustomAttributes(typeof(TypeConverterAttribute)).OfType<TypeConverterAttribute>()
				.FirstOrDefault()?.ConverterTypeName;
			if (converterTypeName == null) return null;
			return (TypeConverter) Activator.CreateInstance(Type.GetType(converterTypeName, true));
		}

		public static void ResetValue(object o, string propName) {
			if (TryGetDependencyProperty(o, propName, out var dp, out var d)) {
				// var md = dp.GetMetadata(o.GetType());
				// if (d.GetValue(dp).Equals(md.DefaultValue)) {
				// 	var args = new DependencyPropertyChangedEventArgs(dp, DependencyProperty.UnsetValue, md.DefaultValue);
				// 	md.PropertyChangedCallback(d, args);
				// }
				// else
					d.ClearValue(dp);
				
			}
			if (TryGetPropertyInfo(o.GetType(), propName, out var pi)) {
				//TODO use typeof(TypeConverterAttribute)
				var v = GetDefault(pi.PropertyType);
				pi.SetValue(o, v);
			}
			if (TryGetFieldInfo(o.GetType(), propName, out var fi)) {
				var v = GetDefault(fi.FieldType);
				fi.SetValue(o, v);
			}
			throw new NotSupportedException();
		}

		public static object GetDefault(Type type) {
			if (type.GetTypeInfo().IsValueType) {
				return Activator.CreateInstance(type);
			}
			return null;
		}

	}

	internal class TypeDescriptorContext : ITypeDescriptorContext {
		public TypeDescriptorContext(object instance, string propertyName) {
			Instance = instance;
			PropertyDescriptor = TypeDescriptor.GetProperties(instance)[propertyName];
		}

		public object Instance { get; private set; }
		public PropertyDescriptor PropertyDescriptor { get; private set; }
		public IContainer Container { get; private set; }

		public void OnComponentChanged() {
		}

		public bool OnComponentChanging() {
			return true;
		}

		public object GetService(Type serviceType) {
			return null;
		}
	}

}

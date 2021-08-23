
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using KsWare.Presentation.ViewFramework.DesignTime;
using NUnit.Framework;

namespace KsWare.Presentation.ViewFramework.Tests.DesignTime {

	[TestFixture]
	public class DesignPropertiesHelperTests {

		[TestCase(typeof(TextBox), "BorderThickness")]
		[Apartment(ApartmentState.STA)]
		public void GetDependencyProperty(Type type, string name) {
			var o = (DependencyObject)Activator.CreateInstance(type);
			var dp = DesignPropertiesHelper.GetDependencyProperty(o, name);

			Assert.That(dp, Is.Not.Null);
		}

		[TestCase(typeof(Thickness))]
		[Apartment(ApartmentState.STA)]
		public void GetTypeConverter(Type type) {
			var converter=DesignPropertiesHelper.GetTypeConverter(type);
			Assert.That(converter, Is.Not.Null);
		}

		[TestCase(typeof(TextBox),"Width", typeof(LengthConverter))]
		[Apartment(ApartmentState.STA)]
		public void GetTypeConverter(Type type, string propertyName, Type converterType) {
			DesignPropertiesHelper.TryGetPropertyInfo(type, propertyName, out var pi);
			var converter = DesignPropertiesHelper.GetTypeConverter(pi);
			Assert.That(converter, Is.TypeOf(converterType));
		}

		[TestCase(typeof(Thickness), "1,2,3,4")]
		[TestCase(typeof(Visibility), "Collapsed")]
		[TestCase(typeof(int), "2")]
		[TestCase(typeof(double), "2")]
		[TestCase(typeof(double), "2.4")]
		[TestCase(typeof(double), "NaN")]
		[TestCase(typeof(Color), "#FF102030")]
		[TestCase(typeof(Brush), "#FF102030")]
		[Apartment(ApartmentState.STA)]
		public void Convert(Type type, object value) {
			var v=DesignPropertiesHelper.Convert(value, type,null);
			Assert.That(string.Format(CultureInfo.InvariantCulture, "{0}", v), Is.EqualTo(value));
		}


		[TestCase(typeof(TextBox), "BorderThickness","1,2,3,4")] // Thickness from String
		[TestCase(typeof(TextBox), "Visibility", "Collapsed")] // Enum from String
		[TestCase(typeof(TextBox), "Text", "Text")] // String from String
		[TestCase(typeof(TextBox), "MaxLength", "5")] // Int from String
		[TestCase(typeof(TextBox), "Width", "25")] // double from String
		[TestCase(typeof(TextBox), "Width", "25.2")] // double from String
		[TestCase(typeof(TextBox), "Width", "NaN")] // double from String
		[Apartment(ApartmentState.STA)]
		public void SetValue(Type type, string propertyName, object value) {
			//new TextBox().Width
			var o = (DependencyObject)Activator.CreateInstance(type);
			DesignPropertiesHelper.SetValue(o, propertyName, value);

			object actualValue;
			switch (propertyName) {
				case "BorderThickness": actualValue = ((TextBox)o).BorderThickness.ToString(); break;
				case "Visibility": actualValue = ((TextBox)o).Visibility.ToString(); break;
				case "Text": actualValue = ((TextBox)o).Text; break;
				case "MaxLength": actualValue = ((TextBox)o).MaxLength.ToString(); break;
				case "Width": actualValue = ((TextBox)o).Width.ToString(CultureInfo.InvariantCulture); break;
				default: actualValue = null; break;
			}
			Assert.That(actualValue, Is.EqualTo(value));
		}

	}

}



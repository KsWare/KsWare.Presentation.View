using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KsWare.Presentation.ViewFramework.DesignTime {

	// xmlns:dt="http://ksware.de/Presentation/ViewFramework/DesignTime"
	// dt:Properties.Visibility="Collapsed"
	public static class Properties {

		#region Designtime.IsChecked Property

		public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached(
			"IsChecked", typeof (bool?), typeof (Properties), new PropertyMetadata(default(bool?),AtIsCheckedChanged));

		public static void SetIsChecked(DependencyObject element, bool? value) { element.SetValue(IsCheckedProperty, value); }

		public static bool? GetIsChecked(DependencyObject element) { return (bool?) element.GetValue(IsCheckedProperty); }

		private static void AtIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
			var propertyInfo = d.GetType().GetProperty("IsChecked");
			if(propertyInfo==null) return;
			propertyInfo.SetValue(d, e.NewValue,null);
		}

		#endregion

		#region Designtime.Content Property

		public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached(
			"Content", typeof (object), typeof (Properties), new PropertyMetadata(default(object),AtContentChanged));

		public static void SetContent(DependencyObject element, object value) { element.SetValue(ContentProperty, value); }

		public static object GetContent(DependencyObject element) { return element.GetValue(ContentProperty); }

		private static void AtContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
			var propertyInfo = d.GetType().GetProperty("Content");
			if(propertyInfo==null) return;
			propertyInfo.SetValue(d, e.NewValue,null);
		}

		#endregion

		#region Designtime.Source Property

		public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
			"Source", typeof (ImageSource), typeof (Properties), new PropertyMetadata(default(ImageSource),AtSourceChanged));

		public static void SetSource(Image element, Image value) { element.SetValue(SourceProperty, value); }

		public static ImageSource GetSource(Image element) { return (ImageSource) element.GetValue(SourceProperty); }

		private static void AtSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
			((Image)d).Source = (ImageSource) e.NewValue;
		}

		#endregion

		#region Designtime.Visibility Property

		public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached(
			"Visibility", typeof (Visibility), typeof (Properties), new PropertyMetadata(default(Visibility),AtVisibilityChanged));

		public static void SetVisibility(UIElement element, Visibility value) { element.SetValue(VisibilityProperty, value); }

		public static Visibility GetVisibility(UIElement element) { return (Visibility) element.GetValue(VisibilityProperty); }

		private static void AtVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
			((UIElement)d).Visibility = (Visibility) e.NewValue;
		}

		#endregion

		#region Background

		public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush),AtBackgroundChanged));

		private static void AtBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
			((Control)d).Background = (Brush) e.NewValue;
		}

		public static void SetBackground(DependencyObject element, Brush value) { element.SetValue(BackgroundProperty, value); }

		public static Brush GetBackground(DependencyObject element) { return (Brush) element.GetValue(BackgroundProperty); }

		#endregion

		#region Style

		public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached("Style", typeof(Style), typeof(Properties), new PropertyMetadata(default(Style),AtStyleChanged));

		private static void AtStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
			((FrameworkElement) d).Style = (Style) e.NewValue;
		}

		public static void SetStyle(DependencyObject element, Style value) { element.SetValue(StyleProperty, value); }

		public static Style GetStyle(DependencyObject element) { return (Style) element.GetValue(StyleProperty); }

		#endregion

		#region Properties

// TODO: WIP
//		private static readonly DependencyPropertyKey PropertiesPropertyKey = DependencyProperty.RegisterAttachedReadOnly("Properties", typeof(DesignerPropertyCollection), typeof(Designtime), new PropertyMetadata(default(DesignerPropertyCollection)));
//
//		public static DependencyProperty PropertiesProperty => PropertiesPropertyKey.DependencyProperty;
//
//		public static void SetProperties(DependencyObject element, DesignerPropertyCollection value) { element.SetValue(PropertiesProperty, value); }
//
//		public static DesignerPropertyCollection GetProperties(DependencyObject element) { return (DesignerPropertyCollection) element.GetValue(PropertiesProperty); }

		private static readonly DependencyPropertyKey PropertiesPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly("PropertiesInternal", typeof(DesignerPropertyCollection), typeof(Properties),
				new UIPropertyMetadata(null));

		public static readonly DependencyProperty PropertiesProperty = PropertiesPropertyKey.DependencyProperty;

		private static void SetProperties(DependencyObject obj, DesignerPropertyCollection value) {
			obj.SetValue(PropertiesPropertyKey, value);
		}

		public static DesignerPropertyCollection GetProperties(DependencyObject obj) {
			if (obj == null) throw new ArgumentNullException(nameof(obj));
			if (obj.GetValue(PropertiesProperty) is DesignerPropertyCollection col) return col;
			col = new DesignerPropertyCollection(obj);
			SetProperties(obj, col);
			return col;
		}

		#endregion
	}

// TODO: WIP 
// not working because exception and/or value/name are null
	public class DesignerPropertyCollection : ObservableCollection<object> {
		// TODO exception with specific type "DesignerProperty"

		public DesignerPropertyCollection(DependencyObject associatedObject) {
			if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(associatedObject)) return; // do nothing at runtime
			AssociatedObject = associatedObject;
		}
		public DependencyObject AssociatedObject { get; }

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
			base.OnCollectionChanged(e);
			if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(AssociatedObject)) return; // do nothing at runtime
			switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					foreach (var p in e.NewItems.Cast<DesignerProperty>()) {
						//throw new Exception($"Add {p} {p.Name}");
						p.AssociatedObject = AssociatedObject;
						OnPropertyAdded(p);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var p in e.OldItems.Cast<DesignerProperty>()) {
						//throw new Exception($"Remove {p} {p.Name}");
						OnPropertyRemoved(p);
					}
					break;
			}
		}

		private void OnPropertyRemoved(DesignerProperty designerProperty) {
			//TODO reset dependency value or set defaut value
		}

		private void OnPropertyAdded(DesignerProperty designerProperty) {
			if(string.IsNullOrEmpty(designerProperty.Name)) return;

			var pi=AssociatedObject.GetType().GetProperty(designerProperty.Name,
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			var value = System.Convert.ChangeType(designerProperty.Value, pi.PropertyType);
			AssociatedObject.GetType().InvokeMember(designerProperty.Name,
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, null,
				AssociatedObject, new object[] {value});
		}

		public static void ResetValue(DependencyObject d, DependencyProperty dp) {
			var md = dp.GetMetadata(d.GetType());
			if (d.GetValue(dp).Equals(md.DefaultValue)) {
				var args = new DependencyPropertyChangedEventArgs(dp, DependencyProperty.UnsetValue, md.DefaultValue);
				md.PropertyChangedCallback(d, args);
			}
			else
				d.ClearValue(dp);
		}
	}

	public class DesignerProperty : DependencyObject {

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DesignerProperty), new FrameworkPropertyMetadata(default(string), (d, e) => ((DesignerProperty)d).PropertyChangedCallback(e)));
		public string Name { get => (string) GetValue(NameProperty); set => SetValue(NameProperty, value); }

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DesignerProperty), new FrameworkPropertyMetadata(default(object), (d,e)=>((DesignerProperty)d).PropertyChangedCallback(e)));
		public object Value { get => (object) GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
		
		public static readonly DependencyProperty AssociatedObjectProperty = DependencyProperty.Register("AssociatedObject", typeof(DependencyObject), typeof(DesignerProperty), new FrameworkPropertyMetadata(default(DependencyObject), (d, e) => ((DesignerProperty)d).PropertyChangedCallback(e)));
		public DependencyObject AssociatedObject { get => (DependencyObject)GetValue(AssociatedObjectProperty); set => SetValue(AssociatedObjectProperty, value); }



		private void PropertyChangedCallback(DependencyPropertyChangedEventArgs e) {

			if (string.IsNullOrEmpty(Name)) return;
			if (AssociatedObject==null) return;

			var pi = AssociatedObject.GetType().GetProperty(Name,
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			var value = System.Convert.ChangeType(Value, pi.PropertyType);
			AssociatedObject.GetType().InvokeMember(Name,
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, null,
				AssociatedObject, new object[] { value });
		}


	}

	namespace ShortName {

		[SuppressMessage("ReSharper", "InconsistentNaming")]
		public static class Design {

			#region Designtime.Visibility Property

			public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached(
				"Visibility", typeof(Visibility), typeof(Design), new PropertyMetadata(default(Visibility), AtVisibilityChanged));

			public static void SetVisibility(UIElement element, Visibility value) { element.SetValue(VisibilityProperty, value); }

			public static Visibility GetVisibility(UIElement element) { return (Visibility)element.GetValue(VisibilityProperty); }

			private static void AtVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
				if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
				((UIElement)d).Visibility = (Visibility)e.NewValue;
			}

			#endregion
			#region Designtime.IsChecked Property

			public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached(
				"IsChecked", typeof(bool?), typeof(Design), new PropertyMetadata(default(bool?), AtIsCheckedChanged));

			public static void SetIsChecked(DependencyObject element, bool? value) { element.SetValue(IsCheckedProperty, value); }

			public static bool? GetIsChecked(DependencyObject element) { return (bool?)element.GetValue(IsCheckedProperty); }

			private static void AtIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
				if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
				var propertyInfo = d.GetType().GetProperty("IsChecked");
				if (propertyInfo == null) return;
				propertyInfo.SetValue(d, e.NewValue, null);
			}

			#endregion

			#region Designtime.Content Property

			public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached(
				"Content", typeof(object), typeof(Design), new PropertyMetadata(default(object), AtContentChanged));

			public static void SetContent(DependencyObject element, object value) { element.SetValue(ContentProperty, value); }

			public static object GetContent(DependencyObject element) { return element.GetValue(ContentProperty); }

			private static void AtContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
				if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
				var propertyInfo = d.GetType().GetProperty("Content");
				if (propertyInfo == null) return;
				propertyInfo.SetValue(d, e.NewValue, null);
			}

			#endregion

			#region Designtime.Source Property

			public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
				"Source", typeof(ImageSource), typeof(Design), new PropertyMetadata(default(ImageSource), AtSourceChanged));

			public static void SetSource(Image element, Image value) { element.SetValue(SourceProperty, value); }

			public static ImageSource GetSource(Image element) { return (ImageSource)element.GetValue(SourceProperty); }

			private static void AtSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
				if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
				((Image)d).Source = (ImageSource)e.NewValue;
			}

			#endregion
		
			#region Background

			public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(Design), new PropertyMetadata(default(Brush), AtBackgroundChanged));

			private static void AtBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
				if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
				((Control)d).Background = (Brush)e.NewValue;
			}

			public static void SetBackground(DependencyObject element, Brush value) { element.SetValue(BackgroundProperty, value); }

			public static Brush GetBackground(DependencyObject element) { return (Brush)element.GetValue(BackgroundProperty); }

			#endregion

			#region Style

			public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached("Style", typeof(Style), typeof(Design), new PropertyMetadata(default(Style), AtStyleChanged));

			private static void AtStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
				if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(d)) return;
				((FrameworkElement)d).Style = (Style)e.NewValue;
			}

			public static void SetStyle(DependencyObject element, Style value) { element.SetValue(StyleProperty, value); }

			public static Style GetStyle(DependencyObject element) { return (Style)element.GetValue(StyleProperty); }

			#endregion

			#region Properties

			// TODO: WIP

			private static readonly DependencyPropertyKey PropertiesPropertyKey =
				DependencyProperty.RegisterAttachedReadOnly("PropertiesInternal", typeof(DesignerPropertyCollection), typeof(Design),
					new UIPropertyMetadata(null));

			public static readonly DependencyProperty PropertiesProperty = PropertiesPropertyKey.DependencyProperty;

			private static void SetProperties(DependencyObject obj, DesignerPropertyCollection value) {
				obj.SetValue(PropertiesPropertyKey, value);
			}

			public static DesignerPropertyCollection GetProperties(DependencyObject obj) {
				if (obj == null) throw new ArgumentNullException(nameof(obj));
				if (obj.GetValue(PropertiesProperty) is DesignerPropertyCollection col) return col;
				col = new DesignerPropertyCollection(obj);
				SetProperties(obj, col);
				return col;
			}

				#endregion
		}
	}
}

// Attached Read-only Collection Dependency Properties
// https://digitaltapestry.wordpress.com/2008/07/28/attached-read-only-collection-dependency-properties/
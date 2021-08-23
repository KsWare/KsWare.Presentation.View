﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KsWare.Presentation.ViewFramework.DesignTime {

	// <TextBox Design.Visibility="Collapsed"/>

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

		// Attached Read-only Collection Dependency Properties
		// https://digitaltapestry.wordpress.com/2008/07/28/attached-read-only-collection-dependency-properties/

		private static readonly DependencyPropertyKey PropertiesPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly("PropertiesInternal", typeof(DesignPropertyCollection), typeof(Design),
				new UIPropertyMetadata(null));

		public static readonly DependencyProperty PropertiesProperty = PropertiesPropertyKey.DependencyProperty;

		private static void SetProperties(DependencyObject obj, DesignPropertyCollection value) {
			obj.SetValue(PropertiesPropertyKey, value);
		}

		public static DesignPropertyCollection GetProperties(DependencyObject obj) {
			if (obj == null) throw new ArgumentNullException(nameof(obj));
			if (obj.GetValue(PropertiesProperty) is DesignPropertyCollection col) return col;
			col = new DesignPropertyCollection(obj);
			SetProperties(obj, col);
			return col;
		}

		#endregion
	}

}

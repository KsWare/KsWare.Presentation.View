using System;
using System.ComponentModel;
using System.Windows;


namespace KsWare.Presentation.ViewFramework {

	/// <summary>
	/// Provides a proxy to bind a value.
	/// </summary>
	/// <example>
	/// <code language="xaml">
	///	&lt;UserControl.Resources&gt;
	///		&lt;BindingProxy x:Key="DataContextProxy" Value="{Binding}"/&gt;
	/// &lt;/UserControl.Resources&gt;
	/// &lt;Button 
	///		Command="{Binding Source={StaticResource DataContextProxy},
	///		Path=Value.DoAnythingCommand}" /&gt;
	/// </code>
	/// </example>
	public class BindingProxy : Freezable {

		/// <summary>
		/// The Value dependency property.
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null,PropertyChangedCallback));

		private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var o = (BindingProxy)d;
			o.ValueChanged.Invoke(o, e);
		}
		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value {
			get => GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Raised on <see cref="Value"/> changes.
		/// </summary>
		public event EventHandler<DependencyPropertyChangedEventArgs> ValueChanged;

		/// <inheritdoc />
		protected override Freezable CreateInstanceCore() {
			return new BindingProxy();
		}
	}

}

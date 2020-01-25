using System.Windows;

namespace KsWare.Presentation.ViewFramework.DesignTime {

	public class DesignProperty : DependencyObject {

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DesignProperty), new FrameworkPropertyMetadata(default(string), (d, e) => ((DesignProperty)d).PropertyChangedCallback(e)));
		public string Name { get => (string) GetValue(NameProperty); set => SetValue(NameProperty, value); }

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DesignProperty), new FrameworkPropertyMetadata(default(object), (d,e)=>((DesignProperty)d).PropertyChangedCallback(e)));
		public object Value { get => (object) GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
		
		public static readonly DependencyProperty AssociatedObjectProperty = DependencyProperty.Register("AssociatedObject", typeof(DependencyObject), typeof(DesignProperty), new FrameworkPropertyMetadata(default(DependencyObject), (d, e) => ((DesignProperty)d).PropertyChangedCallback(e)));
		public DependencyObject AssociatedObject { get => (DependencyObject)GetValue(AssociatedObjectProperty); set => SetValue(AssociatedObjectProperty, value); }



		private void PropertyChangedCallback(DependencyPropertyChangedEventArgs e) {

			if (string.IsNullOrEmpty(Name)) return;
			if (AssociatedObject==null) return;
			DesignPropertiesHelper.SetValue(AssociatedObject,Name,Value);
		}

		

	}

}

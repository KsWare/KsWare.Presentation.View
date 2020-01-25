using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace KsWare.Presentation.ViewFramework.DesignTime {

	public class DesignPropertyCollection : ObservableCollection<object> {
		// TODO exception with specific type "DesignProperty"

		public DesignPropertyCollection(DependencyObject associatedObject) {
			if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(associatedObject)) return; // do nothing at runtime

			AssociatedObject = associatedObject;
		}
		public DependencyObject AssociatedObject { get; }

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
			base.OnCollectionChanged(e);
			if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(AssociatedObject)) return; // do nothing at runtime

			switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					foreach (var p in e.NewItems.Cast<DesignProperty>()) {
						//throw new Exception($"Add {p} {p.Name}");
						p.AssociatedObject = AssociatedObject;
						OnPropertyAdded(p);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var p in e.OldItems.Cast<DesignProperty>()) {
						//throw new Exception($"Remove {p} {p.Name}");
						OnPropertyRemoved(p);
					}
					break;
			}
		}


		private void OnPropertyAdded(DesignProperty designProperty) {
			if(string.IsNullOrEmpty(designProperty.Name)) return;

			DesignPropertiesHelper.SetValue(AssociatedObject, designProperty.Name, designProperty.Value);
		}
		private void OnPropertyRemoved(DesignProperty designProperty) {
			if (string.IsNullOrEmpty(designProperty.Name)) return;
			DesignPropertiesHelper.ResetValue(AssociatedObject, designProperty.Name);
		}

	}

}

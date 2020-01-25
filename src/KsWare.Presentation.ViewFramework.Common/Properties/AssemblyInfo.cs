using System.Reflection;
using System.Windows;
using System.Windows.Markup;


[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

// [assembly: XmlnsDefinition(KsWare.Presentation.ViewFramework.Common.AssemblyInfo.XmlNamespace, "KsWare.Presentation.ViewFramework")]
// [assembly: XmlnsPrefix(KsWare.Presentation.ViewFramework.Common.AssemblyInfo.XmlNamespace, "ksv")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "KsWare.Presentation.ViewFramework")]


// The namespace MUST correspond to the assembly name.!
// ReSharper disable once CheckNamespace
namespace KsWare.Presentation.ViewFramework.Common {

	/// <summary>
	/// Provides information about this assembly.
	/// </summary>
	public static class AssemblyInfo {

		/// <summary>
		/// Gets the assembly.
		/// </summary>
		/// <value>The assembly.</value>
		public static Assembly Assembly => Assembly.GetExecutingAssembly();

		/// <summary>
		/// The XML namespace. const http://ksware.de/Presentation/ViewFramework
		/// </summary>
		public const string XmlNamespace = "http://ksware.de/Presentation/ViewFramework";

		/// <summary>
		/// The root namespace. const KsWare.Presentation.ViewFramework
		/// </summary>
		public const string RootNamespace = "KsWare.Presentation.ViewFramework";

	}
}

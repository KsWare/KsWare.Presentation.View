using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;


[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

// [assembly: XmlnsDefinition(KsWare.Presentation.ViewFramework.Common.AssemblyInfo.XmlNamespace, "KsWare.Presentation.ViewFramework")]
// [assembly: XmlnsPrefix(KsWare.Presentation.ViewFramework.Common.AssemblyInfo.XmlNamespace, "ksv")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "KsWare.Presentation.ViewFramework")]

[assembly: InternalsVisibleTo("KsWare.Presentation.ViewFramework.Common.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010069C998BC60C2134424673D3D2F2C081BE5A433F2187D1D65A5C6467ABF11FA2844B737A2436BEDBA99E063A0F792BFCDD9B52DD3FA8ADDB394D97B8523A48C91C3366A0571ABAAA9C051327A7CCBBBEA5488968639B27929CC3699A6E9738FBF47814720A0ED13CDD971E808484D781C53EDA81C4579DFF5087A34D8F1383C9B")]


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

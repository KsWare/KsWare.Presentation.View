using System.Windows.Markup;

// with prefix
// [assembly: XmlnsDefinition(KsWare.Presentation.ViewFramework.Common.AssemblyInfo.XmlNamespace+ "/DesignTime", "KsWare.Presentation.ViewFramework.DesignTime")]
// [assembly: XmlnsPrefix(KsWare.Presentation.ViewFramework.Common.AssemblyInfo.XmlNamespace + "/DesignTime", "dt")]
// xmlns:dt="http://ksware.de/Presentation/ViewFramework/DesignTime"
// dt:Properties.Visibility="Collapsed"

// without prefix
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "KsWare.Presentation.ViewFramework.DesignTime")]
// Design.Visibility="Collapsed"
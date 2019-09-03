using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace KsWare.Presentation.ViewFramework.Tests {

	[TestFixture]
	public class LibTests {

		[Test]
		public void NamespaceMustMatchAssemblyName() {
			var t = typeof (KsWare.Presentation.ViewFramework.Common.AssemblyInfo);
			var assemblyName = KsWare.Presentation.ViewFramework.Common.AssemblyInfo.Assembly.GetName(false).Name;
			Assert.That(t.Namespace, Is.EqualTo(assemblyName));
		}
	}
}

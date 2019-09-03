using KsWare.Presentation.Converters;
using NUnit.Framework;

namespace KsWare.Presentation.ViewFramework.Tests
{
	[TestFixture]
	public class VisibilityBindingTests
	{
		[Test]
		public void Test()
		{
			var sut = new VisibilityBinding("Test", VisibilityConverter.Expression.TrueVisibleElseCollapsed);
			
			Assert.That(sut.Converter, Is.SameAs(VisibilityConverter.TrueVisibleElseCollapsed));
		}
	}
}
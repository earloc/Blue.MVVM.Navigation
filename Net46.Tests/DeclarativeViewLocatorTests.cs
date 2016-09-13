using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blue.MVVM.Navigation.ViewLocators;
using System.Threading.Tasks;

namespace Net46.Tests {
    [TestClass]
    public class DeclarativeViewLocatorTests {
        public class ViewModel {

        }

        public class ViewModel<T> {

        }

        public class SpecializeViewModel<T> : ViewModel<T> {

        }

        public class StringViewModel : ViewModel<string> {

        }


        [DefaultViewFor(typeof(ViewModel<>))]
        [DefaultViewFor(typeof(ViewModel))]
        public class View {

        }

        [DefaultViewFor(typeof(ViewModel<string>))]
        public class StringView {

        }
      

        [TestMethod]
        public async Task SearchingConcreteClosedGenericResolvesMatchingView() {
            var viewModelType = typeof(ViewModel);
            var expected = typeof(View);
            
            var unit = new DeclarativeViewLocator();
            var actual = await unit.ResolveViewTypeForAsync(viewModelType);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task SearchingClosedGenericResolvesMatchingOpenGenericView() {
            var viewModelType = typeof(ViewModel<string>);
            var expected = typeof(StringView);

            var unit = new DeclarativeViewLocator();
            var actual = await unit.ResolveViewTypeForAsync(viewModelType);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task SearchingForSpecializedOpenGenericResolvesMatchingView() {
            var viewModelType = typeof(SpecializeViewModel<>);
            var expected = typeof(View);

            var unit = new DeclarativeViewLocator();
            var actual = await unit.ResolveViewTypeForAsync(viewModelType);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task SearchingViewForSpecializedClosedGenericResolvesViewWithMappingForBaseType() {
            var viewModelType = typeof(StringViewModel);
            var expected = typeof(StringView);

            var unit = new DeclarativeViewLocator();
            var actual = await unit.ResolveViewTypeForAsync(viewModelType);

            Assert.AreEqual(expected, actual);
        }
    }
}

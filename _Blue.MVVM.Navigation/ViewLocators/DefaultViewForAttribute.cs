using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation.ViewLocators {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DefaultViewForAttribute : Attribute {

        public DefaultViewForAttribute(Type viewmodelType) {
            if (viewmodelType == null)
                throw new ArgumentNullException(nameof(viewmodelType), "must not be null");

            ViewModelType = viewmodelType;
        }

        public Type ViewModelType { get; }
    }
}

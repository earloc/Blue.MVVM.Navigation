using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    public class ResolvingViewEventArgs : EventArgs {

        public ResolvingViewEventArgs(Type viewModelType, string suggestedViewTypeName) {
            if (viewModelType == null)
                throw new ArgumentNullException(nameof(viewModelType), "must not be null");

            ViewModelType = viewModelType;
            SuggestedViewTypeName = suggestedViewTypeName;
        }

        public Type ViewModelType { get; }

        public string SuggestedViewTypeName { get;  }

    }
}

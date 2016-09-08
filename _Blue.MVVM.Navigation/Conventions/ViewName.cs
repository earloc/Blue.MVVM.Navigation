using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation.Conventions {
    public class ViewName {

        public ViewName(string @namespace, string name) {
            if (string.IsNullOrEmpty(@namespace))
                throw new ArgumentNullException(nameof(@namespace), "must not be null or empty");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "must not be null or empty");

            Namespace = @namespace;
            Name = name;
        }

        public string Name { get; set; }
        public string Namespace { get; set; }

        public string FullName {
            get {
                return $"{Namespace}.{Name}";
            }
        }

    }
}

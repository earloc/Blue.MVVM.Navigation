﻿using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.ViewLocators {
    public class DefaultViewLocator : IViewLocator {

        public DefaultViewLocator() {
            Map = new MappingViewLocator();
            Convention = new ConventionalViewLocator(true);
            Declarative = new DeclarativeViewLocator();
            Composite = new CompositeViewLocator(Map, Convention, Declarative);
            Cache = new CachingViewLocator(Composite);
        }

        public CompositeViewLocator Composite { get; }
        public DeclarativeViewLocator Declarative { get; }
        public ConventionalViewLocator Convention { get; }
        public MappingViewLocator Map { get; }
        public CachingViewLocator Cache { get; }

        public Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false) {
            return Cache.ResolveViewTypeForAsync(viewModelType, throwOnError);
        }
    }
}

﻿#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos (Brazil)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion Copyright

using System.Linq;
using System.Web.Http.ModelBinding;
using Xunit;

namespace DataTables.AspNet.WebApi2.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.Mvc5 'Configuration' class.
    /// </summary>
    public class ConfigurationTests
    {
        /// <summary>
        /// This test must be executed alone.
        /// Validates registration with default settings.
        /// </summary>
        [Fact]
        public void DefaultRegistration()
        {
			// Arrange
			//var httpConfiguration = TestHelper.MockHttpConfiguration();
			var httpConfig = new System.Web.Http.HttpConfiguration();
            DataTables.AspNet.WebApi2.Configuration.RegisterDataTables(httpConfig);
            var modelBinder = System.Web.Http.GlobalConfiguration.Configuration.Services
				.GetServices(typeof(IModelBinder))
				.Cast<IModelBinder>()
				.FirstOrDefault();

            // Assert
            Assert.NotNull(modelBinder);
            Assert.NotNull(modelBinder as IModelBinder);
            Assert.Null((modelBinder as ModelBinder).ParseAdditionalParameters);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom options.
        /// </summary>
        [Fact]
        public void RegistrationWithCustomOptions()
        {
            // Arrange
            var options = TestHelper.MockOptions().UseHungarianNotation();
			var httpConfig = new System.Web.Http.HttpConfiguration();
			DataTables.AspNet.WebApi2.Configuration.RegisterDataTables(httpConfig, options);

            // Assert
            Assert.Equal(options, DataTables.AspNet.WebApi2.Configuration.Options);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom model binder.
        /// </summary>
        [Fact]
        public void RegistrationWithCustomBinder()
        {
            // Arrange
            var requestBinder = TestHelper.MockModelBinder();
			var httpConfig = new System.Web.Http.HttpConfiguration();
			DataTables.AspNet.WebApi2.Configuration.RegisterDataTables(httpConfig, requestBinder);

            var modelBinder = System.Web.Http.GlobalConfiguration.Configuration.Services.GetServices(typeof(IModelBinder)).Cast<IModelBinder>().FirstOrDefault();

            // Assert
            Assert.Equal(requestBinder, modelBinder);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom parser function for aditional parameters.
        /// </summary>
        [Fact]
        public void RegistrationWithParseAditionalParameters()
        {
			// Arrange
			var httpConfig = new System.Web.Http.HttpConfiguration();
			DataTables.AspNet.WebApi2.Configuration.RegisterDataTables(httpConfig, TestHelper.ParseAdditionalParameters, true);
			var modelBinder = System.Web.Http.GlobalConfiguration.Configuration.Services.GetServices(typeof(IModelBinder)).Cast<IModelBinder>().FirstOrDefault() as ModelBinder;

			// Assert
			Assert.Equal(true, DataTables.AspNet.WebApi2.Configuration.Options.IsRequestAdditionalParametersEnabled);
            Assert.Equal(true, DataTables.AspNet.WebApi2.Configuration.Options.IsResponseAdditionalParametersEnabled);
            Assert.NotNull(modelBinder.ParseAdditionalParameters);
        }
    }
}

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Routing;

namespace NSB17WebApiInjection
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			var container = new WindsorContainer();
			container.Register( Component.For<Controllers.SampleController>() );

			var busConfig = new BusConfiguration();
			busConfig.UseContainer<NServiceBus.WindsorBuilder>( c => c.ExistingContainer( container ) );
			busConfig.UsePersistence<InMemoryPersistence>();

			var bus = Bus.Create( busConfig ).Start();

			GlobalConfiguration.Configure( httpConfig =>
			{
				WebApiConfig.Register( httpConfig );
				httpConfig.DependencyResolver = new Resolver( container );
			} );
		}
	}

	class Resolver : IDependencyResolver
	{
		/*
		 * Very trivial resolver for the purpose of the sample, a better implementation here 
		 * https://github.com/RadicalFx/Radical-Boostrappers/blob/develop/src/net45/Radical.Bootstrapper.Windsor.AspNet/Infrastructure/WindsorDependencyResolver.cs
		 */

		IWindsorContainer container;

		public Resolver( IWindsorContainer container )
		{
			this.container = container;
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public object GetService( Type serviceType )
		{
			if( this.container.Kernel.HasComponent( serviceType ) )
			{
				return this.container.Resolve( serviceType );
			}

			return null;
		}

		public IEnumerable<object> GetServices( Type serviceType )
		{
			if( this.container.Kernel.HasComponent( serviceType ) )
			{
				var all = this.container.ResolveAll( serviceType );
				return all.Cast<Object>();
			}

			return new List<Object>();
		}


		public void Dispose()
		{

		}
	}

}
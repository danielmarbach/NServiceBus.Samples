using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using NServiceBus;
using NServiceBus.ObjectBuilder;

namespace NSB10AspNetInjection
{
	public class Global : HttpApplication
	{
		void Application_Start( object sender, EventArgs e )
		{
			// Code that runs on application startup
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure( WebApiConfig.Register );
			RouteConfig.RegisterRoutes( RouteTable.Routes );

			var configuration = new BusConfiguration();
			configuration.UsePersistence<InMemoryPersistence>();

			var busInstance = Bus.Create( configuration ).Start();
			var builder = ( ( NServiceBus.Unicast.UnicastBus )busInstance ).Builder;

			IConfigureComponents configurer = null;
			configuration.RegisterComponents( r =>
			{
				configurer = r;
				r.RegisterSingleton<IControllerFactory>( new BuilderControllerFactory( builder ) );
			} );

			System.Web.Mvc.DependencyResolver.SetResolver( t =>
			{
				if( configurer.HasComponent( t ) )
				{
					return builder.Build( t );
				}

				//default value expected by MVC to signal that we have no components of type "t"
				return null;
			},
			t =>
			{
				if( configurer.HasComponent( t ) )
				{
					return builder.BuildAll( t );
				}

				//default value expected by MVC to signal that we have no components of type "t"
				return new List<Object>();
			} );
		}

		public class BuilderControllerFactory : DefaultControllerFactory
		{
			readonly IBuilder builder;

			public BuilderControllerFactory( IBuilder builder )
			{
				this.builder = builder;
			}

			public override void ReleaseController( IController controller )
			{
				builder.Release( controller );
			}

			protected override IController GetControllerInstance( RequestContext requestContext, Type controllerType )
			{
				if( controllerType == null )
				{
					throw new HttpException( 404, string.Format( "The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path ) );
				}

				var iController = ( IController )builder.Build( controllerType );

				return iController;
			}
		}

		public class BuilderDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
		{
			readonly IBuilder builder;
			readonly IConfigureComponents configurer;

			public BuilderDependencyResolver( IBuilder builder, IConfigureComponents configurer )
			{
				this.builder = builder;
				this.configurer = configurer;
			}

			public System.Web.Http.Dependencies.IDependencyScope BeginScope()
			{
				var scope = this.builder.CreateChildBuilder();
				return new BuilderDependencyScope( scope, this.configurer);
			}

			public object GetService( Type serviceType )
			{
				if( this.configurer.HasComponent( serviceType ) )
				{
					return this.builder.Build( serviceType );
				}

				return null;
			}

			public IEnumerable<object> GetServices( Type serviceType )
			{
				if( this.configurer.HasComponent( serviceType ) )
				{
					return this.builder.BuildAll( serviceType );
				}

				return new Object[ 0 ];
			}

			public void Dispose()
			{

			}
		}

		class BuilderDependencyScope : System.Web.Http.Dependencies.IDependencyScope
		{
			readonly IBuilder scopedBuilder;
			readonly IConfigureComponents configurer;

			public BuilderDependencyScope( IBuilder scopedBuilder, IConfigureComponents configurer )
			{
				this.scopedBuilder = scopedBuilder;
				this.configurer = configurer;
			}
			public object GetService( Type serviceType )
			{
				if( this.configurer.HasComponent( serviceType ) )
				{
					return this.scopedBuilder.Build( serviceType );
				}

				return null;
			}

			public IEnumerable<object> GetServices( Type serviceType )
			{
				if( this.configurer.HasComponent( serviceType ) )
				{
					return this.scopedBuilder.BuildAll( serviceType );
				}

				return new Object[ 0 ];
			}

			public void Dispose()
			{
				this.scopedBuilder.Dispose();
			}
		}
	}
}
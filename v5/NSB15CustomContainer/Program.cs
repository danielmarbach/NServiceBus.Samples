using Castle.MicroKernel.Registration;
using NServiceBus;
using Radical.Bootstrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB15CustomContainer
{
	class Program
	{
		static void Main( string[] args )
		{
			var bootstrapper = new WindsorBootstrapper
			(
				directory: AppDomain.CurrentDomain.BaseDirectory, //the directory where to look for assemblies
				filter: "*.*" //the default filer is *.dll, but this is and exe so we need to include it to
			);

			//the bootstrap process will look for any class 
			//the implements the IWindsorInstaller inetrface
			//and is exported via MEF
			var container = bootstrapper.Boot();

			var config = new BusConfiguration();
			config.UsePersistence<InMemoryPersistence>();
			config.UseContainer<NServiceBus.WindsorBuilder>( c =>
			{
				c.ExistingContainer( container );
			} );
		}
	}

	[Export(typeof(IWindsorInstaller))]
	class MyInstaller : IWindsorInstaller
	{
		public void Install( Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store )
		{
			//configure the container here with custom configuration
		}
	}

}

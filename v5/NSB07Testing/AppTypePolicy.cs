using NServiceBus.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB07Testing.Sagas
{
	public class AppTypePolicy : Saga<AppTypePolicyData>,
		IAmStartedByMessages<Messages.AppTypeRequest>,
		IAmStartedByMessages<Messages.SetAppType>
	{

		protected override void ConfigureHowToFindSaga( SagaPropertyMapper<AppTypePolicyData> mapper )
		{
			mapper.ConfigureMapping<Messages.SetAppType>( r => r.AppId ).ToSaga( r => r.AppId );
			mapper.ConfigureMapping<Messages.AppTypeRequest>( r => r.AppId ).ToSaga( r => r.AppId );
		}

		public void Handle( Messages.AppTypeRequest message )
		{
			this.Data.AppId = message.AppId;

			var response = new Messages.AppTypeResponse();
			response.AppId = this.Data.AppId;
			response.AppType = this.Data.AppType;
			this.Bus.Reply( response );

		}

		public void Handle( Messages.SetAppType message )
		{
			this.Data.AppId = message.AppId;
			this.Data.AppType = Messages.AppType.Known;
		}
	}


	public class AppTypePolicyData : ContainSagaData
	{
		[Unique]
		public int AppId { get; set; }
		public Messages.AppType AppType { get; set; }

		public AppTypePolicyData()
		{
			this.AppType = Messages.AppType.Unknown;
		}
	}

}

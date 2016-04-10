using Microsoft.PowerBI.Api.Beta;
using Microsoft.PowerBI.Api.Beta.Models;
using Microsoft.PowerBI.Security;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace powerbi_sample_app.Reporting
{
    public class PowerBIReportClient : IPowerBIClient, IDisposable
    {
        public const string StatePublishing     = "Publishing";
        public const string StateSucceeded      = "Succeeded";
        public const string StateFailed         = "Failed";

        private static string _signingKey;
        private static string _apiUrl;

        private string _workspaceCollection;
        private string _workspaceId;
        private IPowerBIClient _client;

        static PowerBIReportClient()
        {
            _signingKey = ConfigurationManager.AppSettings["powerbi:SigningKey"];
            _apiUrl = ConfigurationManager.AppSettings["powerbi:ApiUrl"];
        }

        public PowerBIReportClient(string workspaceCollection, string workspaceId)
            : this(PowerBIToken.CreateDevToken(workspaceCollection, workspaceId), workspaceCollection, workspaceId)
        {
        }

        public PowerBIReportClient(PowerBIToken token, string workspaceCollection, string workspaceId)
        {
            this._workspaceCollection = workspaceCollection;
            this._workspaceId = workspaceId;
            this._client = CreatePowerBIClient(token);
        }

        public void Dispose()
        {
            if (this._client != null)
            {
                this._client.Dispose();
            }
        }

        public static string SigningKey
        {
            get { return _signingKey; }
        }

        public async Task<Import> WaitForImportToComplete(Import import)
        {
            while (import.ImportState != StateSucceeded && import.ImportState != StateFailed)
            {
                import = await this._client.Imports.GetImportByIdAsync(this._workspaceCollection, this._workspaceId, import.Id);
                await Task.Delay(1000);
            }
            return import;
        }

        public async Task UpdateConnectionInformation(IEnumerable<Dataset> datasets, string serverName, string databaseName, string username, string password)
        {
            foreach (var dataset in datasets)
            {
                // Optionally udpate the connectionstring details if preent
                if (!string.IsNullOrWhiteSpace(serverName) && !string.IsNullOrWhiteSpace(databaseName))
                {
                    string connectionString = String.Format("Server=tcp:{0}.database.windows.net;Database={1};Trusted_Connection=False;",
                        serverName,
                        databaseName);
                    var connectionParameters = new Dictionary<string, object>
                    {
                        { "connectionString", connectionString }
                    };
                    await this._client.Datasets.SetAllConnectionsAsync(this._workspaceCollection, this._workspaceId, dataset.Id, connectionParameters);
                }
                // Reset your connection credentials
                var delta = new GatewayDatasource
                {
                    CredentialType = "Basic",
                    BasicCredentials = new BasicCredentials
                    {
                        Username = username,
                        Password = password
                    }
                };
                // Get the datasources from the dataset
                var datasources = await this._client.Datasets.GetGatewayDatasourcesAsync(this._workspaceCollection, this._workspaceId, dataset.Id);
                foreach (var datasource in datasources.Value)
                {
                    // Update the datasource with the specified credentials
                    await this._client.Gateways.PatchDatasourceAsync(this._workspaceCollection, this._workspaceId, datasource.GatewayId, datasource.Id, delta);
                }
            }
        }

        public Uri BaseUri
        {
            get { return this._client.BaseUri;  }
            set { this._client.BaseUri = value; }
        }

        public JsonSerializerSettings SerializationSettings
        {
            get { return this._client.SerializationSettings; }
        }

        public JsonSerializerSettings DeserializationSettings
        {
            get { return this._client.DeserializationSettings; }
        }

        public ServiceClientCredentials Credentials
        {
            get { return this._client.Credentials; }
        }

        public IDatasets Datasets
        {
            get { return this._client.Datasets; }
        }

        public IGateways Gateways
        {
            get { return this._client.Gateways; }
        }

        public IImports Imports
        {
            get { return this._client.Imports; }
        }

        public IWorkspaces Workspaces
        {
            get { return this._client.Workspaces; }
        }

        public IReports Reports
        {
            get { return this._client.Reports; }
        }

        private IPowerBIClient CreatePowerBIClient(PowerBIToken token)
        {
            var jwt = token.Generate(_signingKey);
            var credentials = new TokenCredentials(jwt, "AppToken");
            var client = new PowerBIClient(credentials)
            {
                BaseUri = new Uri(_apiUrl)
            };

            return client;
        }
    }
}
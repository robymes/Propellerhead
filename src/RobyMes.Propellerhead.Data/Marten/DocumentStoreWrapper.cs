using Marten;
using Marten.Events;
using Marten.Events.Projections.Async;
using Marten.Schema;
using Marten.Services;
using Marten.Storage;
using Marten.Transforms;
using RobyMes.Propellerhead.Common.Configuration;
using RobyMes.Propellerhead.Common.Data;
using RobyMes.Propellerhead.Common.Data.Events;
using RobyMes.Propellerhead.Common.Data.Projections;
using System;
using System.Collections.Generic;
using System.Data;

namespace RobyMes.Propellerhead.Data.Marten
{
    public sealed class DocumentStoreWrapper : IDocumentStore
    {
        private IDocumentStore documentStore;

        public IDocumentSchema Schema
        {
            get
            {
                return this.documentStore.Schema;
            }
        }

        public AdvancedOptions Advanced
        {
            get
            {
                return this.documentStore.Advanced;
            }
        }

        public IDiagnostics Diagnostics
        {
            get
            {
                return this.documentStore.Diagnostics;
            }
        }

        public IDocumentTransforms Transform
        {
            get
            {
                return this.documentStore.Transform;
            }
        }

        public EventGraph Events => throw new NotImplementedException();

        public ITenancy Tenancy => throw new NotImplementedException();

        public DocumentStoreWrapper(IConfigurationProvider configurationProvider)
        {
            this.documentStore = DocumentStore.For(ds =>
            {
                ds.Connection(configurationProvider.DocumentStoreConnectionString);
                if (string.IsNullOrEmpty(configurationProvider.DocumentStoreSchemaName) == false)
                {
                    ds.DatabaseSchemaName = configurationProvider.DocumentStoreSchemaName;
                    ds.Events.DatabaseSchemaName = $"{configurationProvider.DocumentStoreSchemaName}_events";
                }
                ds.Events.AddEventTypes(new List<Type>()
                    {
                        typeof(CustomerCreatedEvent)
                    });
                ds.Events.AggregateFor<CustomerStream>();
                ds.Events.InlineProjections.AggregateStreamsWith<CustomerProjection>();
            });
        }

        public IDaemon BuildProjectionDaemon(Type[] viewTypes = null, IDaemonLogger logger = null, DaemonSettings settings = null, global::Marten.Events.Projections.IProjection[] projections = null)
        {
            return this.documentStore.BuildProjectionDaemon(viewTypes, logger, settings, projections);
        }

        public void BulkInsert<T>(T[] documents, BulkInsertMode mode = BulkInsertMode.InsertsOnly, int batchSize = 1000)
        {
            this.documentStore.BulkInsert(documents, mode, batchSize);
        }

        public void BulkInsertDocuments(IEnumerable<object> documents, BulkInsertMode mode = BulkInsertMode.InsertsOnly, int batchSize = 1000)
        {
            this.documentStore.BulkInsertDocuments(documents, mode, batchSize);
        }

        public IDocumentSession DirtyTrackedSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return this.documentStore.DirtyTrackedSession(isolationLevel);
        }

        public void Dispose()
        {
            if (this.documentStore != null)
            {
                this.documentStore.Dispose();
            }
        }

        public IDocumentSession LightweightSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return this.documentStore.LightweightSession(isolationLevel);
        }

        public IDocumentSession OpenSession(DocumentTracking tracking = DocumentTracking.IdentityOnly, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return this.documentStore.OpenSession(tracking, isolationLevel);
        }

        public IDocumentSession OpenSession(SessionOptions options)
        {
            return this.documentStore.OpenSession(options);
        }

        public IQuerySession QuerySession()
        {
            return this.documentStore.QuerySession();
        }

        public IQuerySession QuerySession(SessionOptions options)
        {
            return this.documentStore.QuerySession(options);
        }

        public void BulkInsert<T>(string tenantId, T[] documents, BulkInsertMode mode = BulkInsertMode.InsertsOnly, int batchSize = 1000)
        {
            this.documentStore.BulkInsert(tenantId, documents, mode, batchSize);
        }

        public IDocumentSession OpenSession(string tenantId, DocumentTracking tracking = DocumentTracking.IdentityOnly, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return this.documentStore.OpenSession(tenantId, tracking, isolationLevel);
        }

        public IDocumentSession LightweightSession(string tenantId, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return this.documentStore.LightweightSession(tenantId, isolationLevel);
        }

        public IDocumentSession DirtyTrackedSession(string tenantId, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return this.documentStore.DirtyTrackedSession(tenantId, isolationLevel);
        }

        public IQuerySession QuerySession(string tenantId)
        {
            return this.documentStore.QuerySession(tenantId);
        }

        public void BulkInsertDocuments(string tenantId, IEnumerable<object> documents, BulkInsertMode mode = BulkInsertMode.InsertsOnly, int batchSize = 1000)
        {
            this.documentStore.BulkInsertDocuments(tenantId, documents, mode, batchSize);
        }
    }
}
